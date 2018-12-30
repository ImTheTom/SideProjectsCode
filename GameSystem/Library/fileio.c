#include <stdio.h> 
#include <stdlib.h>
#include <string.h>
#include <pthread.h>
#include <stdint.h>
#include "fileio.h"
#include "senddata.h"

pthread_mutex_t read_write_mutex;
pthread_mutex_t read_count_mutex;
int read_count=0;

//Used to get the number of lines in a text file
int GetNumberOfLines(char * file_name){
    int number_of_lines = 0;
    char buffer[500];//Used to store lines
    FILE *file_ptr=fopen(file_name,"r");
    if(file_ptr){//If the file open correctly
        while(fgets(buffer,sizeof(buffer),file_ptr)!=NULL){//While there are lines being read in
            number_of_lines++;
        }
    }else{
        printf("Failed to open file");
        exit(1);
    }
    fclose(file_ptr);
    return number_of_lines;
}

//Used to get the number of words in a text file
int GetNumberOfWords(char * file_name){
    int number_of_words = 0;
    char buffer[MAX_WORD_SIZE];//Used to store words of the Max Word size specified
    FILE *file_ptr=fopen(file_name,"r");
    if(file_ptr){//If file opened correctly
        while(fgets(buffer,sizeof(buffer),file_ptr)!=NULL){//While words are being read in
            number_of_words++;
        }
    }else{
        printf("Failed to open file");
        exit(1);
    }
    fclose(file_ptr);
    return number_of_words;
}

//Used to select a random word on any line up to the passed integer value words
char * SelectRandomWord(char * file_name, int words){
    int random_selection = rand() % words;//Randomly select a value
    char *buffer = (char *) malloc(sizeof(char)*MAX_WORD_SIZE);//Used to store a word of the max word size specififed
    FILE *file_ptr=fopen(file_name,"r");
    if(file_ptr){//if file opened correctly
        while(random_selection>0){//while not at random value
            fgets(buffer,sizeof(buffer),file_ptr);//increment the file pointer by reading in and temporary storing the word
            random_selection--;
        }
    }else{
        printf("Failed to open file");
        exit(1);
    }
    strtok(buffer,"\n");//Remove the new line from the word
    fclose(file_ptr);
    return buffer;
}

//Used to write to the records.txt file This function is thread safe.
void WriteRecord(char * username, int won, char * game, int seconds){
    pthread_mutex_lock(&read_write_mutex);//Ensure no other thread writes while other threads are writing

    FILE *ptr_file;    
    ptr_file = fopen("TextFiles/records.txt","a");
    if(!ptr_file){//If fail to open file then exit
        printf("File failed to open during authenticate function\n");
        exit(1);
    }
    if(won==1){//if the passed value is 1 then the person won otherwise the person lost
        fprintf(ptr_file,"%s won a game of %s in %d seconds.\n", username, game, seconds);
    }else{
        fprintf(ptr_file,"%s lost a game of %s in %d seconds.\n", username, game, seconds);
    }

    fclose(ptr_file);

    pthread_mutex_unlock(&read_write_mutex); //Unlock mutex so other threads can write

    free(ptr_file);
}

//Used to read 10 lines from the records.txt file. This function is thread safe
void SendRecords(int socket_id){
    pthread_mutex_lock(&read_count_mutex);//Ensure the read_count variable isn't changed by other threads
    read_count++;

    if(read_count>=1)//There are files reading therefore ensure no thread writes while reading
        pthread_mutex_lock(&read_write_mutex);
    
    pthread_mutex_unlock(&read_count_mutex); //Allow other threads to read

    int number_of_lines = GetNumberOfLines("TextFiles/records.txt");
    int start = number_of_lines-LINES_TO_SEND;//start at the appropariate line
    if(start<0){//if there isn't more than LINES_TO_SEND values start at 0
        start = 0;
    }
    int end = start+LINES_TO_SEND;//end at the appropariate line

    FILE *ptr_file;
    char temp_buffer[200];//Used to store lines
    
    ptr_file = fopen("TextFiles/records.txt","r");
    if(!ptr_file){//If fail to open file return FAIL_TO_AUTHENTICATE
        printf("File failed to open during authenticate function\n");
        exit(1);
    }
    for(int i=0; i<start; i++){//Move file pointer to the line
        fgets(temp_buffer,sizeof(temp_buffer),ptr_file);
    }
    while(start<end && fgets(temp_buffer,sizeof(temp_buffer),ptr_file)!=NULL){//Send over the lines while start<end and there is actual lines to send
        SendString(socket_id,temp_buffer,200);
        start++;
    }
    strcpy(temp_buffer,NO_INFORMATION);//Set buffer to known value
    for(int start2=start; start2<end;start2++){//Send over a known value for the remainder of start and end
        SendString(socket_id,temp_buffer,200);
    }

    fclose(ptr_file);

    pthread_mutex_lock(&read_count_mutex);//Ensure the read_count variable isn't changed by other threads
    read_count--;

    if(read_count==0)//No thread is reading
        pthread_mutex_unlock(&read_write_mutex);//Can unlock the write mutex

    pthread_mutex_unlock(&read_count_mutex);

    free(ptr_file);
}

//Used to print records that is reciveing from the SendRecords function
void PrintRecords(int socket_id){
    printf("\n");
    char buffer[200];
    for(int i=0;i<LINES_TO_SEND; i++){
        strcpy(buffer,RecieveString(socket_id,200));
        if(strcmp(buffer,NO_INFORMATION)!=0){//Check if there is information to print
            printf("%s",buffer);
        }
    }
    printf("\n");
}
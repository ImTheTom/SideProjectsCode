#include <stdio.h> 
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <pthread.h>  
#include <stdlib.h>
#include <time.h>
#include <stdint.h>
#include "hangman.h"
#include "fileio.h"
#include "senddata.h"
#include "printmessages.h"

void InitialiseHangman(HangmanGame * hmg){
    int number_of_words = GetNumberOfWords("TextFiles/words.txt");
    char *word_selected = SelectRandomWord("TextFiles/words.txt", number_of_words);
    strcpy(hmg->word,word_selected);
    hmg->game_over=false;
    hmg->lives = 4;
    hmg->word_length=strlen(hmg->word);
    hmg->unkown_letters=hmg->word_length;

    char *buffer = (char *) malloc(sizeof(char)*MAX_WORD_SIZE);
    for(int i=0; i<hmg->word_length;i++){
        buffer[i]='_';
    }
    buffer[hmg->word_length]='\0';
    strcpy(hmg->guessed_word,buffer);
}

void ShowGallows(int lives){
    switch(lives){
        case 4:
            PrintGallows(0);
            break;
        case 3:
            PrintGallows(1);
            break;
        case 2:
            PrintGallows(2);
            break;
        case 1:
            PrintGallows(3);
            break;
        case 0:
            PrintGallows(4);
            break;
    }
}

void PrintGallows(int number_wrong){
    if(number_wrong==0){
        printf("\n________\n");
        printf("|      \n");
        printf("|      \n");
        printf("|      \n");
        printf("|      \n");
        printf("|      \n");
    }else{
        printf("\n________\n");
        printf("|      |\n");
    if(number_wrong==1){
        printf("|      \n");
    }else{
        printf("|      0\n");
    }
    if(number_wrong<=2){
        printf("|      \n");
        printf("|      \n");
        printf("|      \n");
    }
    if(number_wrong==3){
        printf("|     /|\\\n");
        printf("|      |\n");
        printf("|      \n");
        printf("|      \n");
    }
    if(number_wrong==4){
        printf("|     /|\\\n");
        printf("|      |\n");
        printf("|     / \\\n");
        printf("|      \n");
    }
    }
}

int ProcessHangmanResponse(HangmanGame * hmg, int character){
    int found_letter = 0;
    character = Lower(character);
    for(int i=0;i<hmg->word_length;i++){
        if(hmg->word[i]==character){
            found_letter=1;
            if(hmg->guessed_word[i]!=character){
                hmg->guessed_word[i]=character;
                hmg->unkown_letters--;
            }
        }
    }
    if(found_letter==0){
        hmg->lives--;
    }
    return found_letter;
}

int Lower(int character){
    if((character>=65) && (character <=90)){
        character = character+32;
    }
    return character;
}

void PlayHangman(int new_fd,char * username, pthread_mutex_t random_mutex){
    time_t start, end;
    int recieved_integer = 1;
    HangmanGame * hangman_game;
    hangman_game = malloc(sizeof(HangmanGame));
    int found_letter=0;
    int selection_choice = 0;

    pthread_mutex_lock(&random_mutex);
    InitialiseHangman(hangman_game);
    pthread_mutex_unlock(&random_mutex);

    start=time(NULL);

    while(!hangman_game->game_over){
        SendString(new_fd,hangman_game->guessed_word,MAX_WORD_SIZE);
        selection_choice = RecieveInteger(new_fd);
        if(selection_choice==1){
        recieved_integer = RecieveInteger(new_fd);
        found_letter = ProcessHangmanResponse(hangman_game, (int)recieved_integer);
        SendInteger(new_fd,found_letter);
        SendInteger(new_fd, hangman_game->lives);
        if(hangman_game->lives==0){
            hangman_game->game_over=true;
            end=time(NULL);
            SendInteger(new_fd, 1);
            SendString(new_fd,hangman_game->word, MAX_WORD_SIZE);
            WriteRecord(username,0,"Hangman",(int) difftime(end,start));
        }else if(hangman_game->unkown_letters==0){
            hangman_game->game_over=true;
            end=time(NULL);
            SendInteger(new_fd, 2);
            SendString(new_fd,hangman_game->word, MAX_WORD_SIZE);
            WriteRecord(username,1,"Hangman",(int) difftime(end,start));
        }else{
            SendInteger(new_fd, 0);
        }
        }else if(selection_choice==2){
            hangman_game->game_over=true;
            end=time(NULL);
            WriteRecord(username,0,"Hangman",(int) difftime(end,start));
        }
    }

    free(hangman_game);
}

void PlayHangmanClient(int socket_id){
    char hangman_word[20];
    int game_over = 0;
    char letter;
    int found_letter=0;
    int lives=0;
    int round_response=0;
    int selection_choice=0;
    while(game_over==0){
        strcpy(hangman_word,RecieveString(socket_id,20));
        PrintHangmanWord(hangman_word);
        PrintHangmanSelection();
        scanf("%d", &selection_choice);
        if(selection_choice==1){
            SendInteger(socket_id, selection_choice);
            PrintAskForHangmanLetter();
            scanf(" %c",&letter);
            SendInteger(socket_id,(int)letter);
            found_letter = RecieveInteger(socket_id);
            lives = RecieveInteger(socket_id);
            round_response=RecieveInteger(socket_id);
            if(found_letter==1){
                printf("\nCorrect Guess!\n");
            }else{
                printf("\nIncorrect Guess!\n");
                printf("%d Lives remaining\n",lives);
            }
            ShowGallows(lives);
            if(round_response==1){
                strcpy(hangman_word,RecieveString(socket_id,20));
                printf("Out of lives!\nWord was %s\n\n",hangman_word);
                game_over=1;
            }else if(round_response==2){
                strcpy(hangman_word,RecieveString(socket_id,20));
                printf("Game won!\nWord was %s\n\n", hangman_word);
                game_over=1;
            }
        }else if(selection_choice==2){
            SendInteger(socket_id,selection_choice);
            game_over=1;
        }
    }
}
#define _GNU_SOURCE
#include <stdio.h> 
#include <pthread.h>  
#include <stdlib.h>
#include <time.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <errno.h> 
#include <string.h> 
#include <sys/types.h> 
#include <netinet/in.h> 
#include <sys/socket.h> 
#include <sys/wait.h> 
#include <netdb.h>
#include <stdbool.h>
#include <signal.h>
#include "Library/senddata.h"
#include "Library/hangman.h"
#include "Library/authenticate.h"
#include "Library/tictactoe.h"
#include "Library/fileio.h"
#include "Library/tictactoe3d.h"

//Define Magic Numbers
#define DEFAULTPORT 12345
#define NUM_HANDLER_THREADS 10
#define PLAY_HANGMAN 1
#define PLAY_TIC_TAC_TOE 2
#define PLAY_TIC_TAC_TOE_ADVANCE 3
#define PLAY_3D_TIC_TAC_TOE 4
#define RECORDS 8
#define QUIT 9

pthread_mutex_t random_mutex;

pthread_mutex_t request_mutex = PTHREAD_RECURSIVE_MUTEX_INITIALIZER_NP;//Thread pool matrix
pthread_cond_t got_request = PTHREAD_COND_INITIALIZER;//Thread pool condition variable

int number_of_requests = 0;

struct Request {
    int socket_id;
    struct Request* next; 
};

struct Request* requests = NULL;//Head of linked list of requests
struct Request* last_request = NULL;//Last Request

void BeginGames(int new_fd){//Main function a thread will when request has been made
    char * username;
    char * password;
    int recieved_integer = 1;

    username = RecieveString(new_fd,10);// Recive the username and password from the client
    password = RecieveString(new_fd,10);

    int authenticated_index = Authenticate(username,password,"TextFiles/Authentication.txt");// Authenticate the user and send the integer over
    SendInteger(new_fd,authenticated_index);

    if(authenticated_index != FAIL_TO_AUTHENTICATE){
        while(recieved_integer != QUIT){
            recieved_integer = RecieveInteger(new_fd);// Recive the response from the main menu
            printf("Recieved data %d\n", recieved_integer);

            if(recieved_integer==PLAY_HANGMAN){
                PlayHangman(new_fd,username,random_mutex);
            }else if(recieved_integer==PLAY_TIC_TAC_TOE){
                PlayTicTacToe(new_fd,username, random_mutex, 0);
            }else if(recieved_integer == PLAY_TIC_TAC_TOE_ADVANCE){
                PlayTicTacToe(new_fd,username, random_mutex, 1);
            }else if(recieved_integer==PLAY_3D_TIC_TAC_TOE){
                Play3DTicTacToe(new_fd,username,random_mutex);
            } 
            else if(recieved_integer==RECORDS){
                SendRecords(new_fd);
            }
        }
    }
}

//Used to add a Request that needs to be completed by a thread from the pool
void AddRequest(int request_socket, pthread_mutex_t* pool_mutex, pthread_cond_t*  pool_mutex_condition){
    struct Request* new_request;//Initialise a new Request

    new_request = (struct Request*)malloc(sizeof(struct Request));//set aside memory and allocate variables
    new_request->socket_id = request_socket;
    new_request->next = NULL;

    pthread_mutex_lock(pool_mutex);//Lock thread pool mutex
    
    if (number_of_requests == 0) { //If no requests needs to set both requests and last requests to new requests
        requests = new_request;
        last_request = new_request;
    } else {//If there are requets then needs to update the last_request only
        last_request->next = new_request;
        last_request = new_request;
    }

    number_of_requests++;//Increment the number of requests before unlocking mutex

    pthread_mutex_unlock(pool_mutex);

    pthread_cond_signal(pool_mutex_condition);//Signal the condition mutex
}

//Used to get the head of requests linked list
struct Request* get_request(pthread_mutex_t* pool_mutex){
    struct Request* new_request; //Initialise a new Request

    pthread_mutex_lock(pool_mutex);//Lock pool mutex

    if (number_of_requests > 0) {//If number of requests is greater than zero
        new_request = requests;//Set the new Request created to the head of requests
        requests = new_request->next;//Update the head of requests
        if (requests == NULL) {//If requests is null then last requests is null
            last_request = NULL;
        }
        number_of_requests--;//Decrement number of requests
    }
    else { 
        new_request = NULL;
    }

    pthread_mutex_unlock(pool_mutex);

    return new_request;
}

//Used to call the BeginGames function
void HandleRequest(struct Request* new_request){
    if (new_request) {
        BeginGames(new_request->socket_id);
    }
}

//Used for the threads from the pool to be able access requests when needed
void* HandleRequestsLoop(){
    struct Request* new_request;//Initialise a Request variable

    pthread_mutex_lock(&request_mutex);//Lock mutex

    while (1) {
        if (number_of_requests > 0) {//If there is a Request needed to be done
            new_request = get_request(&request_mutex);//Get the Request
            if (new_request) {
                pthread_mutex_unlock(&request_mutex);//Got the Request therefore can let other threads access
                HandleRequest(new_request);//Go and handle the Request
                free(new_request);//Free the Request memory
                pthread_mutex_lock(&request_mutex);//Lock the mutex
            }
        } else {
            pthread_cond_wait(&got_request, &request_mutex);//Unlock the mutex
        }
    }
}

int main(int argc, char* argv[]) {
    socklen_t sin_size;
    int sock_fd, new_fd;
    struct sockaddr_in my_address, their_address;
    int requested_port = DEFAULTPORT;
    pthread_t client_threads[NUM_HANDLER_THREADS]; 

    if(argc==2){
		requested_port = atoi(argv[1]);
    }
    
    //Set up server

    sock_fd = socket(AF_INET, SOCK_STREAM, 0);
    if(sock_fd == -1){
        perror("Socket error");
        exit(1);
    }

    my_address.sin_family = AF_INET;
    my_address.sin_port = htons(requested_port);
    my_address.sin_addr.s_addr = INADDR_ANY;

    if(bind(sock_fd, (struct sockaddr *) &my_address, sizeof(struct sockaddr)) == -1){
        perror("Bind error");
        exit(1);
    }

    if(listen(sock_fd, 10) == -1){
        perror("listen");
        exit(1);
    }

    printf("Server initiated\n");

    // Server set up and running

    for (int i=0; i<NUM_HANDLER_THREADS; i++) {//Create the threads for the pools
        pthread_create(&client_threads[i], NULL, HandleRequestsLoop, NULL);
    }

	while(1) {
        sin_size = sizeof(struct sockaddr_in);
        
		if ((new_fd = accept(sock_fd, (struct sockaddr *)&their_address, &sin_size)) == -1) {
			perror("accept");
			continue;
		}
		printf("server: got connection from %s\n", inet_ntoa(their_address.sin_addr));
            
        AddRequest(new_fd, &request_mutex, &got_request);//Add the Request for the threads
    }
    close(new_fd);
    free(requests);
    free(last_request);
    return 0;
}
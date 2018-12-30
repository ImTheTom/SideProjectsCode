#include <stdio.h> 
#include <stdlib.h> 
#include <errno.h> 
#include <string.h> 
#include <netdb.h> 
#include <sys/types.h> 
#include <netinet/in.h> 
#include <sys/socket.h> 
#include <unistd.h>
#include <stdbool.h>
#include <time.h>
#include <signal.h>
#include "Library/printmessages.h"
#include "Library/senddata.h"
#include "Library/hangman.h"
#include "Library/authenticate.h"
#include "Library/tictactoe.h"
#include "Library/fileio.h"
#include "Library/tictactoe3d.h"

#define FAIL_AUTHENTICATE 65535
#define PLAY_HANGMAN 1
#define PLAY_TIC_TAC_TOE 2
#define PLAY_TIC_TAC_TOE_ADVANCE 3
#define PLAY_3D_TIC_TAC_TOE 4
#define RECORDS 8
#define QUIT 9
#define REVEAL_MINE "R"
#define PLACE_FLAG "P"
#define QUIT_GAME "Q"

int sock_fd;

//Used to handle ctrl+c action
void HandleSigint(int sig){
    pid_t id = getpid();
    close(sock_fd);
    kill(id,SIGTERM);
}

int main(int argc, char *argv[]){
    struct hostent *he;
    struct sockaddr_in their_address;
    int response_integer;
    char username[10];
    char password[10];

    if(argc != 3){//Client needs 2 commands server address and port number
        printf("No enough input commands - ./client {server address} {port number}\n");
        return 0;
    }

    signal(SIGINT, HandleSigint);//Handle Ctrl+c command

    //Start setting up connection

    he = gethostbyname(argv[1]);

    sock_fd = socket(AF_INET, SOCK_STREAM, 0);
    if(sock_fd == -1){
        perror("Socket error");
        exit(1);
    }

    their_address.sin_family = AF_INET;
    their_address.sin_port = htons(atoi(argv[2]));
    their_address.sin_addr = *((struct in_addr *)he->h_addr);
    bzero(&(their_address.sin_zero), 8);

    if(connect(sock_fd, (struct sockaddr *) &their_address, sizeof(struct sockaddr)) == -1){
        perror("Connect Error");
        exit(1);
    }

    //Connection made

    PrintLogIn();//Print log in and get the username and password
    GetUsername(username);
    GetPassword(password);

    SendString(sock_fd, username, 10);//Send information to server
    SendString(sock_fd, password, 10);    

    response_integer = RecieveInteger(sock_fd);//Get the response from the server
    if(response_integer != FAIL_AUTHENTICATE){
        PrintWelcome();
    }else{
        PrintFailAuthenticate();
    }

    while(response_integer != FAIL_AUTHENTICATE && response_integer != QUIT){
        PrintSelectionOptions();//Print the main menu selection
        scanf("%d",&response_integer);
        SendInteger(sock_fd, response_integer);//send selection to server
        if(response_integer==PLAY_HANGMAN){
            PlayHangmanClient(sock_fd);
        }else if(response_integer == PLAY_TIC_TAC_TOE || response_integer == PLAY_TIC_TAC_TOE_ADVANCE){
            PlayTicTacToeClient(sock_fd);
        }else if(response_integer == PLAY_3D_TIC_TAC_TOE){
            Play3DTicTacToeClient(sock_fd);
        }else if(response_integer==RECORDS){
            PrintRecords(sock_fd);
        }
    }
    close(sock_fd);
    return 0;
}
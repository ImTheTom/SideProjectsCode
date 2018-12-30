#include "printmessages.h"
#include "tictactoe.h"
#include <stdio.h>

//Prints the log in message
void PrintLogIn(){
    printf("==================================\n");
    printf("Welcome to the online gaming system\n");
    printf("==================================\n\n");
    printf("You are required to log on with your registered user name and password\n\n");
}

//Prints the welcome message
void PrintWelcome(){
    printf("Welcome to the gaming system\n\n");
}

//Prints the faile authentiction message
void PrintFailAuthenticate(){
    printf("Failed to authenticate\n");
}

//Prints the main menu selection options
void PrintSelectionOptions(){
    printf("Please enter a selection: \n");
    printf("<1> Hangman \n");
    printf("<2> Tic Tac Toe \n");
    printf("<3> Tic Tac Toe Advanced \n");
    printf("<4> 3D Tic Tac Toe\n");
    printf("<8> Records\n");
    printf("<9> Quit \n");
    printf("Selection option: ");
}

//Prints the selection menu for Hangman
void PrintHangmanSelection(){
    printf("What would you like to do\n");
    printf("<1> Guess letter \n");
    printf("<2> Quit\n");
    printf("Selection choice: ");
}

//Prints to ask for a letter in hangman
void PrintAskForHangmanLetter(){
    printf("Guess Letter: ");
}

//Prints the word in readable format for hangman
void PrintHangmanWord(char * word){
    printf("Word is: %s\n",word);
}

//Prints the tic tac toe selection menu
void PrintTicTacToeSelection(){
    printf("What would you like to do\n");
    printf("<1> Select Coordinates \n");
    printf("<2> Quit\n");
    printf("Selection choice: ");
}

//Prints to ask for coordinates for tic tac toe
void PrintAskForTicTacToeCoordinates(){
    printf("Coordinates: ");
}

//Prints the winner of a tic tac toe game
void PrintTicTacToeWinner(int winner){
    if(winner==NAUGHT){
        printf("The winning player is Naughts!\n\n");
    }else{
        printf("The winning player is Crosses!\n\n");
    }
}
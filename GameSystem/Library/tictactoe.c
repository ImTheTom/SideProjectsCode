#include <stdio.h> 
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <time.h>
#include <stdint.h>
#include <pthread.h>  
#include "tictactoe.h"
#include "fileio.h"
#include "senddata.h"
#include "printmessages.h"

struct TicTacToeGame * test_game;

struct Moves* head;
struct Moves* tail;

void InitialiseTicTacToe(TicTacToeGame * tttg){
    test_game = malloc(sizeof(TicTacToeGame));
    tttg->game_over=false;
    tttg->turn=NAUGHT;
    for(int x=0;x<ROWS;x++){
        for(int y=0; y<COLUMNS;y++){
            Tile tile;
            tile.state=EMPTY;
            tttg->board[x][y]=tile;
        }
    }
}

void PrintTicTacToe(TicTacToeGame * tttg){
    for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+64);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                printf("%d ",tttg->board[x-1][y].state);
            }
        }
        printf("\n");
    }
}

void PrintTicTacToeFromArray(int ** game_array){
    printf("\n");
    for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+64);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                if(game_array[x-1][y]==NAUGHT)
                    printf("%c ",'o');
                else if(game_array[x-1][y]==CROSS)
                    printf("%c ",'x');
                else
                    printf("  ");
            }
        }
        printf("\n");
    }
}

void ResetTicTacToeArray(int ** game_array, TicTacToeGame * tttg){
    for(int x=0; x<ROWS; x++){
        for(int y=0; y<COLUMNS; y++){
            game_array[x][y] = tttg->board[x][y].state;
        }
    }
}

int ProcessTicTacToeResponse(char * response, TicTacToeGame * tttg, int random){
    int row = response[0]-65; //A
    int column = response[1]-49; //1
    if(IsValid(row,column,tttg)){
        tttg->board[row][column].state=tttg->turn;
        if(CheckGameWon(tttg)){
            printf("Game is over!\n");
            printf("%d Won!\n",tttg->turn);
            tttg->game_over=true;
            return tttg->turn;
        }
        if(tttg->turn == NAUGHT)
            tttg->turn=CROSS;
        else
            tttg->turn=NAUGHT;
        if(random==1)
            return ComputerTicTacToeChoiceNotRandom(tttg);
        else
            return ComputerTicTacToeChoice(tttg);
    }
    return 0;
}

int ComputerTicTacToeChoice(TicTacToeGame * tttg){
    for(int x=0; x<ROWS;x++){//Columns
        for(int y=0; y<COLUMNS; y++){
            if(tttg->board[x][y].state==EMPTY){
                struct Moves * m;
                m = (struct Moves*) malloc(sizeof(struct Moves));
                m->row = x;
                m->column = y;
                m->next=NULL;
                if(head==NULL){
                    head = m;
                    tail = m;
                }else{
                    tail->next = m;
                    tail = m;
                }
            }
        }
    }
    int number_of_moves = 0;
    struct Moves * temp_move;
    struct Moves * free_move;
    temp_move=head;
    while(temp_move!=NULL){
        temp_move=temp_move->next;
        number_of_moves++;
    }

    int selected_row=0;
    int selected_column=0;
    int random_selection = rand() % number_of_moves;
    temp_move=head;
    for(int i=0; i<random_selection;i++){
        temp_move = temp_move->next;
    }
    selected_row=temp_move->row;
    selected_column=temp_move->column;
    temp_move = head;
    while(temp_move!=NULL){
        free_move = temp_move;
        temp_move=temp_move->next;
        free(free_move);
    }
    head=NULL;
    return ComputerTicTacToeProcess(selected_row,selected_column, tttg);
}

int ComputerTicTacToeChoiceNotRandom(TicTacToeGame * tttg){
    for(int x=0; x<ROWS;x++){//Columns
        for(int y=0; y<COLUMNS; y++){
            if(tttg->board[x][y].state==EMPTY){
                struct Moves * m;
                m = (struct Moves*) malloc(sizeof(struct Moves));
                m->row = x;
                m->column = y;
                m->next=NULL;
                if(head==NULL){
                    head = m;
                    tail = m;
                }else{
                    tail->next = m;
                    tail = m;
                }
            }
        }
    }

    int number_of_moves = 0;
    struct Moves * temp_move;
    struct Moves * free_move;
    int selected_row=0;
    int selected_column=0;
    temp_move=head;
    while(temp_move!=NULL){
        tttg->board[temp_move->row][temp_move->column].state = NAUGHT;
        if(CheckGameWon(tttg)){
            tttg->board[temp_move->row][temp_move->column].state = EMPTY;
            selected_row = temp_move->row;
            selected_column = temp_move->column;
            temp_move = head;
            while(temp_move!=NULL){
                free_move = temp_move;
                temp_move=temp_move->next;
                free(free_move);
            }
            head=NULL;
            return ComputerTicTacToeProcess(selected_row,selected_column, tttg);
        }
        tttg->board[temp_move->row][temp_move->column].state = EMPTY;

        tttg->board[temp_move->row][temp_move->column].state = CROSS;
        if(CheckGameWon(tttg)){
            tttg->board[temp_move->row][temp_move->column].state = EMPTY;
            selected_row = temp_move->row;
            selected_column = temp_move->column;
            temp_move = head;
            while(temp_move!=NULL){
                free_move = temp_move;
                temp_move=temp_move->next;
                free(free_move);
            }
            head=NULL;
            return ComputerTicTacToeProcess(selected_row,selected_column, tttg);
        }
        tttg->board[temp_move->row][temp_move->column].state = EMPTY;
        temp_move=temp_move->next;
        number_of_moves++;
    }

    int random_selection = rand() % number_of_moves;
    temp_move=head;
    for(int i=0; i<random_selection;i++){
        temp_move = temp_move->next;
    }
    selected_row=temp_move->row;
    selected_column=temp_move->column;
    temp_move = head;
    while(temp_move!=NULL){
        free_move = temp_move;
        temp_move=temp_move->next;
        free(free_move);
    }
    head=NULL;
    return ComputerTicTacToeProcess(selected_row,selected_column, tttg);
}

int ComputerTicTacToeProcess(int row, int column, TicTacToeGame * tttg){
    if(IsValid(row,column,tttg)){
        tttg->board[row][column].state=tttg->turn;
        if(CheckGameWon(tttg)){
            printf("Game is over!\n");
            printf("%d Won!\n",tttg->turn);
            tttg->game_over=true;
            return tttg->turn;
        }
        if(tttg->turn == NAUGHT)
            tttg->turn=CROSS;
        else
            tttg->turn=NAUGHT;
    }
    return 0;
}

bool IsValid(int row, int column, TicTacToeGame * tttg){
    if(tttg->board[row][column].state==0){
        return (row>=0) && (row<ROWS) && (column>=0) && (column<COLUMNS);
    }
    return false;
}

bool CheckGameWon(TicTacToeGame * tttg){
    for(int column=0; column<COLUMNS;column++){
        if(CheckColumn(column, tttg)){
            return true;
        }
    }
    for(int row=0; row<ROWS;row++){
        if(CheckRow(row, tttg)){
            return true;
        }
    }
    if(CheckTopLeftBotRight(tttg)){
        return true;
    }
    if(CheckTopRightBotLeft(tttg)){
        return true;
    }
    return false;
}


bool CheckColumn(int column, TicTacToeGame * tttg){
    if(tttg->board[0][column].state!=0){
        if(tttg->board[0][column].state==tttg->board[1][column].state && tttg->board[1][column].state == tttg->board[2][column].state){
            return true;
        }
    }
    return false;
}

bool CheckRow(int row, TicTacToeGame * tttg){
    if(tttg->board[row][0].state!=0){
        if(tttg->board[row][0].state==tttg->board[row][1].state && tttg->board[row][1].state == tttg->board[row][2].state){
            return true;
        }
    }
    return false;
}

bool CheckTopLeftBotRight(TicTacToeGame * tttg){
    if(tttg->board[0][0].state!=0){
        if(tttg->board[0][0].state==tttg->board[1][1].state && tttg->board[1][1].state == tttg->board[2][2].state){
            return true;
        }
    }
    return false;
}

bool CheckTopRightBotLeft(TicTacToeGame * tttg){
    if(tttg->board[0][2].state!=0){
        if(tttg->board[0][2].state==tttg->board[1][1].state && tttg->board[1][1].state == tttg->board[2][0].state){
            return true;
        }
    }
    return false;
}

void PlayTicTacToe(int socket_id,char * username, pthread_mutex_t random_mutex, int random){
    char coordinates[2];
    TicTacToeGame * tttg;
    int selection_choice = 0;
    int winner = 0;
    time_t start, end;

    int ** game_array;
    uint16_t ** byte_array;

    game_array=calloc(COLUMNS, sizeof(int *));
    if(!game_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<ROWS;row++){
        game_array[row]=calloc(ROWS, sizeof(int));
        if(!game_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    byte_array=calloc(COLUMNS, sizeof(uint16_t *));
    if(!byte_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<ROWS;row++){
        byte_array[row]=calloc(ROWS, sizeof(uint16_t));
        if(!byte_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    tttg = malloc(sizeof(TicTacToeGame));

    if(!tttg){
        perror("Tic tac toe game malloc error");
        exit(1);
    }

    pthread_mutex_lock(&random_mutex);
    InitialiseTicTacToe(tttg);
    pthread_mutex_unlock(&random_mutex);

    start=time(NULL);

    while(!tttg->game_over){
        ResetTicTacToeArray(game_array,tttg);
        SendArrayInt(socket_id,game_array,ROWS,COLUMNS);
        selection_choice= RecieveInteger(socket_id);
        if(selection_choice==1){
            strcpy(coordinates,RecieveString(socket_id,2));
            winner = ProcessTicTacToeResponse(coordinates, tttg, random);
            SendInteger(socket_id,winner);
            if(winner!=0){
                end=time(NULL);
                ResetTicTacToeArray(game_array,tttg);
                SendArrayInt(socket_id,game_array,ROWS,COLUMNS);
                if(winner==NAUGHT){
                    WriteRecord(username,1,"Tic Tac Toe",(int) difftime(end,start));
                }else{
                    WriteRecord(username,0,"Tic Tac Toe",(int) difftime(end,start));
                }
            }
        }else if(selection_choice==2){
            tttg->game_over=true;
            end=time(NULL);
            WriteRecord(username,0,"Tic Tac Toe",(int) difftime(end,start));
        }
    }

    free(tttg);
    free(game_array);
    free(byte_array);
}

void PlayTicTacToeClient(int socket_id){
    char coordinates[2];
    int game_over = 0;
    int winner = 0;
    int selection_choice;
    int ** game_array;
    uint16_t ** byte_array;

    game_array=calloc(COLUMNS, sizeof(int *));
    if(!game_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<ROWS;row++){
        game_array[row]=calloc(ROWS, sizeof(int));
        if(!game_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    byte_array=calloc(COLUMNS, sizeof(uint16_t *));
    if(!byte_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<ROWS;row++){
        byte_array[row]=calloc(ROWS, sizeof(uint16_t));
        if(!byte_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }
    
    while(game_over==0){
        RecieveArrayInt(socket_id,byte_array,game_array,ROWS,COLUMNS);
        PrintTicTacToeFromArray(game_array);
        PrintTicTacToeSelection();
        scanf("%d", &selection_choice);
        if(selection_choice==1){
            SendInteger(socket_id, selection_choice);
            PrintAskForTicTacToeCoordinates();
            scanf("%s",coordinates);
            SendString(socket_id,coordinates,2);
            winner = RecieveInteger(socket_id);
        }else if(selection_choice==2){
            SendInteger(socket_id, selection_choice);
            game_over=1;
        }
       if(winner!=0){
            game_over=1;
        }
    }
    if(winner!=0){
        RecieveArrayInt(socket_id,byte_array,game_array,ROWS,COLUMNS);
        PrintTicTacToeFromArray(game_array);
        PrintTicTacToeWinner(winner);
    }

    free(game_array);
    free(byte_array);
}
#include <stdio.h> 
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <time.h>
#include <stdint.h>
#include <pthread.h>  
#include "tictactoe.h"
#include "tictactoe3d.h"
#include "fileio.h"
#include "senddata.h"
#include "printmessages.h"

struct Moves3D* head;
struct Moves3D* tail;

void Initialise3DTicTacToe(TicTacToeGame3D * tttg){
    tttg->game_over=false;
    tttg->turn=NAUGHT;
    for(int x=0;x<ROWS;x++){
        for(int y=0; y<COLUMNS;y++){
            Tile tile;
            tile.state=EMPTY;
            tttg->board[0][x][y]=tile;
            tttg->board[1][x][y]=tile;
            tttg->board[2][x][y]=tile;
        }
    }
}

void Print3DTicTacToe(TicTacToeGame3D * tttg){
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
                printf("%d ",tttg->board[0][x-1][y].state);
            }
        }
        printf("\n");
    }
    for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+67);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                printf("%d ",tttg->board[1][x-1][y].state);
            }
        }
        printf("\n");
    }
    for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+70);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                printf("%d ",tttg->board[2][x-1][y].state);
            }
        }
        printf("\n");
    }
}

void Print3DTicTacToeFromArray(int ** game_array){
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

    for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+67);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                if(game_array[x+2][y]==NAUGHT)
                    printf("%c ",'o');
                else if(game_array[x+2][y]==CROSS)
                    printf("%c ",'x');
                else
                    printf("  ");
            }
        }
        printf("\n");
    }

        for(int x=0; x<4;x++){
        if(x==0){
            printf(" ");
        }else{
            printf("%c ", x+70);
        }
        for(int y=0; y<3;y++){
            if(x==0){
                printf(" %d",y+1);
            }else{
                if(game_array[x+5][y]==NAUGHT)
                    printf("%c ",'o');
                else if(game_array[x+5][y]==CROSS)
                    printf("%c ",'x');
                else
                    printf("  ");
            }
        }
        printf("\n");
    }
}

void Reset3DTicTacToeArray(int ** game_array, TicTacToeGame3D * tttg){
    for(int x=0; x<9; x++){
        for(int y=0; y<COLUMNS; y++){
            int field = x/3;
            int game_row = x-(field*3);
            game_array[x][y] = tttg->board[field][game_row][y].state;
        }
    }
}

int Process3DTicTacToeResponse(char * response, TicTacToeGame3D * tttg){
    int row = response[0]-65; //A
    int field = row/3;
    row = row-(field*3);
    int column = response[1]-49; //1
    printf("Field - %d Row - %d Column - %d\n",field,row,column);
    if(Is3DValid(field,row,column,tttg)){
        tttg->board[field][row][column].state=tttg->turn;
        if(Check3DGameWon(tttg)){
            printf("Game is over!\n");
            printf("%d Won!\n",tttg->turn);
            tttg->game_over=true;
            return tttg->turn;
        }
        if(tttg->turn == NAUGHT)
            tttg->turn=CROSS;
        else
            tttg->turn=NAUGHT;
        return Computer3DTicTacToeChoiceNotRandom(tttg);
    }
    return 0;
}

int Computer3DTicTacToeChoiceNotRandom(TicTacToeGame3D * tttg){
    for(int f=0; f<3;f++){
    for(int x=0; x<ROWS;x++){//Columns
        for(int y=0; y<COLUMNS; y++){
            if(tttg->board[f][x][y].state==EMPTY){
                struct Moves3D * m;
                m = (struct Moves3D*) malloc(sizeof(struct Moves3D));
                m->field=f;
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
    }

    int number_of_moves = 0;
    struct Moves3D * temp_move;
    struct Moves3D * free_move;
    int selected_field=0;
    int selected_row=0;
    int selected_column=0;
    temp_move=head;
    while(temp_move!=NULL){
        tttg->board[temp_move->field][temp_move->row][temp_move->column].state = NAUGHT;
        if(Check3DGameWon(tttg)){
            tttg->board[temp_move->field][temp_move->row][temp_move->column].state = EMPTY;
            selected_field = temp_move->field;
            selected_row = temp_move->row;
            selected_column = temp_move->column;
            temp_move = head;
            while(temp_move!=NULL){
                free_move = temp_move;
                temp_move=temp_move->next;
                free(free_move);
            }
            head=NULL;
            return Computer3DTicTacToeProcess(selected_field, selected_row,selected_column, tttg);
        }
        tttg->board[temp_move->field][temp_move->row][temp_move->column].state = EMPTY;

        tttg->board[temp_move->field][temp_move->row][temp_move->column].state = CROSS;
        if(Check3DGameWon(tttg)){
            tttg->board[temp_move->field][temp_move->row][temp_move->column].state = EMPTY;
            selected_field = temp_move->field;
            selected_row = temp_move->row;
            selected_column = temp_move->column;
            temp_move = head;
            while(temp_move!=NULL){
                free_move = temp_move;
                temp_move=temp_move->next;
                free(free_move);
            }
            head=NULL;
            return Computer3DTicTacToeProcess(selected_field, selected_row,selected_column, tttg);
        }
        tttg->board[temp_move->field][temp_move->row][temp_move->column].state = EMPTY;
        temp_move=temp_move->next;
        number_of_moves++;
    }

    int random_selection = rand() % number_of_moves;
    temp_move=head;
    for(int i=0; i<random_selection;i++){
        temp_move = temp_move->next;
    }
    selected_field=temp_move->field;
    selected_row=temp_move->row;
    selected_column=temp_move->column;
    temp_move = head;
    while(temp_move!=NULL){
        free_move = temp_move;
        temp_move=temp_move->next;
        free(free_move);
    }
    head=NULL;
    return Computer3DTicTacToeProcess(selected_field, selected_row,selected_column, tttg);
}

int Computer3DTicTacToeProcess(int field, int row, int column, TicTacToeGame3D * tttg){
    if(Is3DValid(field,row,column,tttg)){
        tttg->board[field][row][column].state=tttg->turn;
        if(Check3DGameWon(tttg)){
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

bool Is3DValid(int field, int row, int column, TicTacToeGame3D * tttg){
    if(tttg->board[field][row][column].state==0){
        return (row>=0) && (row<ROWS) && (column>=0) && (column<COLUMNS);
    }
    return false;
}

bool Check3DBotLeftTopRightVerticle(TicTacToeGame3D * tttg){
    if(tttg->board[0][2][0].state!=0){
        if(tttg->board[0][2][0].state==tttg->board[1][1][1].state && tttg->board[1][1][1].state == tttg->board[2][0][2].state){
            return true;
        }
    }
    return false;
}

bool Check3DGameWon(TicTacToeGame3D * tttg){
    for(int column=0; column<COLUMNS;column++){
        if(Check3DColumn(column, tttg)){
            return true;
        }
    }

    for(int i=0; i<9;i++){
        int row =0;
        row = i/3;
        int column = i-(row*3);
        if(Check3DColumnVertical(row, column, tttg)){
            return true;
        }
    }

    for(int row=0; row<ROWS;row++){
        if(Check3DRow(row, tttg)){
            return true;
        }
    }

    for(int i=0; i<3; i++){
        if(Check3DRowsLeftToRight(i,tttg)){
            return true;
        }
        if(Check3DRowsRightToLeft(i,tttg)){
            return true;
        }
    }

    if(Check3DTopLeftBotRight(tttg)){
        return true;
    }
    if(Check3DTopRightBotLeft(tttg)){
        return true;
    }

    if(Check3DTopLeftBotRightVerticle(tttg)){
        return true;
    }

    if(Check3DBotRightTopLeftVerticle(tttg)){
        return true;
    }

    if(Check3DTopRightBotLeftVerticle(tttg)){
        return true;
    }

    if(Check3DBotLeftTopRightVerticle(tttg)){
        return true;
    }

    return false;
}


bool Check3DColumn(int column, TicTacToeGame3D * tttg){
    for(int i=0; i<3;i++){
        if(tttg->board[i][0][column].state!=0){
            if(tttg->board[i][0][column].state==tttg->board[i][1][column].state && tttg->board[i][1][column].state == tttg->board[i][2][column].state){
                return true;
            }
        }
    }
    return false;
}

bool Check3DColumnVertical(int row, int column, TicTacToeGame3D * tttg){
    if(tttg->board[0][row][column].state!=0){
        if(tttg->board[0][row][column].state==tttg->board[1][row][column].state && tttg->board[1][row][column].state == tttg->board[2][row][column].state){
            return true;
        }
    }
    return false;
}

bool Check3DRow(int row, TicTacToeGame3D * tttg){
    for(int i=0; i<3;i++){
        if(tttg->board[i][row][0].state!=0){
            if(tttg->board[i][row][0].state==tttg->board[i][row][1].state && tttg->board[i][row][1].state == tttg->board[i][row][2].state){
                return true;
            }
        }
    }
    return false;
}

bool Check3DRowsLeftToRight(int row, TicTacToeGame3D * tttg){
    if(tttg->board[0][row][0].state!=0){
        if(tttg->board[0][row][0].state==tttg->board[1][row][1].state && tttg->board[1][row][1].state == tttg->board[2][row][2].state){
            return true;
        }
    }
    return false;
}

bool Check3DRowsRightToLeft(int row, TicTacToeGame3D * tttg){
    if(tttg->board[0][row][2].state!=0){
        if(tttg->board[0][row][2].state==tttg->board[1][row][1].state && tttg->board[1][row][1].state == tttg->board[2][row][0].state){
            return true;
        }
    }
    return false;
}

bool Check3DTopLeftBotRight(TicTacToeGame3D * tttg){
    for(int i=0; i<3;i++){
        if(tttg->board[i][0][0].state!=0){
            if(tttg->board[i][0][0].state==tttg->board[i][1][1].state && tttg->board[i][1][1].state == tttg->board[i][2][2].state){
                return true;
            }
        }
    }
    return false;
}

bool Check3DTopLeftBotRightVerticle(TicTacToeGame3D * tttg){
    if(tttg->board[0][0][0].state!=0){
        if(tttg->board[0][0][0].state==tttg->board[1][1][1].state && tttg->board[1][1][1].state == tttg->board[2][2][2].state){
            return true;
        }
    }
    return false;
}

bool Check3DBotRightTopLeftVerticle(TicTacToeGame3D * tttg){
    if(tttg->board[0][2][2].state!=0){
        if(tttg->board[0][2][2].state==tttg->board[1][1][1].state && tttg->board[1][1][1].state == tttg->board[2][0][0].state){
            return true;
        }
    }
    return false;
}


bool Check3DTopRightBotLeft(TicTacToeGame3D * tttg){
    for(int i=0; i<3;i++){
        if(tttg->board[i][0][2].state!=0){
            if(tttg->board[i][0][2].state==tttg->board[i][1][1].state && tttg->board[i][1][1].state == tttg->board[i][2][0].state){
                return true;
            }
        }
    }
    return false;
}

bool Check3DTopRightBotLeftVerticle(TicTacToeGame3D * tttg){
    if(tttg->board[0][0][2].state!=0){
        if(tttg->board[0][0][2].state==tttg->board[1][1][1].state && tttg->board[1][1][1].state == tttg->board[2][2][0].state){
            return true;
        }
    }
    return false;
}

void Play3DTicTacToe(int socket_id,char * username, pthread_mutex_t random_mutex){
    char coordinates[2];
    TicTacToeGame3D * tttg;
    int selection_choice = 0;
    int winner = 0;
    time_t start, end;

    int ** game_array;
    uint16_t ** byte_array;

    game_array=calloc(9, sizeof(int *));
    if(!game_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<9;row++){
        game_array[row]=calloc(3, sizeof(int));
        if(!game_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    byte_array=calloc(9, sizeof(uint16_t *));
    if(!byte_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<9;row++){
        byte_array[row]=calloc(3, sizeof(uint16_t));
        if(!byte_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    tttg = malloc(sizeof(TicTacToeGame3D));

    if(!tttg){
        perror("Tic tac toe game malloc error");
        exit(1);
    }

    pthread_mutex_lock(&random_mutex);
    Initialise3DTicTacToe(tttg);
    pthread_mutex_unlock(&random_mutex);

    start=time(NULL);

    while(!tttg->game_over){
        Reset3DTicTacToeArray(game_array,tttg);
        SendArrayInt(socket_id,game_array,9,3);
        selection_choice= RecieveInteger(socket_id);
        if(selection_choice==1){
            strcpy(coordinates,RecieveString(socket_id,2));
            winner = Process3DTicTacToeResponse(coordinates, tttg);
            SendInteger(socket_id,winner);
            if(winner!=0){
                end=time(NULL);
                Reset3DTicTacToeArray(game_array,tttg);
                SendArrayInt(socket_id,game_array,9,3);
                if(winner==NAUGHT){
                    WriteRecord(username,1,"3D Tic Tac Toe",(int) difftime(end,start));
                }else{
                    WriteRecord(username,0,"3D Tic Tac Toe",(int) difftime(end,start));
                }
            }
        }else if(selection_choice==2){
            tttg->game_over=true;
            end=time(NULL);
            WriteRecord(username,0,"3D Tic Tac Toe",(int) difftime(end,start));
        }
    }

    free(tttg);
    free(game_array);
    free(byte_array);
}

void Play3DTicTacToeClient(int socket_id){
    char coordinates[2];
    int game_over = 0;
    int winner = 0;
    int selection_choice;
    int ** game_array;
    uint16_t ** byte_array;

    game_array=calloc(9, sizeof(int *));
    if(!game_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<9;row++){
        game_array[row]=calloc(3, sizeof(int));
        if(!game_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }

    byte_array=calloc(9, sizeof(uint16_t *));
    if(!byte_array){
        perror("Game array columns error");
        exit(1);
    }
    for(int row=0; row<9;row++){
        byte_array[row]=calloc(3, sizeof(uint16_t));
        if(!byte_array[row]){
            perror("Game array rows error");
            exit(1);
        }
    }
    
    while(game_over==0){
        RecieveArrayInt(socket_id,byte_array,game_array,9,3);
        Print3DTicTacToeFromArray(game_array);
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
        RecieveArrayInt(socket_id,byte_array,game_array,9,3);
        Print3DTicTacToeFromArray(game_array);
        PrintTicTacToeWinner(winner);
    }

    free(game_array);
    free(byte_array);
}
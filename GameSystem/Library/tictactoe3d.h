#ifndef __TICTACTOE3D_H__
#define __TICTACTOE3D_H__
#include <stdbool.h>
#include <pthread.h>
#include "tictactoe.h"

typedef struct{
    Tile board[3][3][3];
    bool game_over;
    int turn;
} TicTacToeGame3D;

struct Moves3D{
    int field;
    int row;
    int column;
    struct Moves3D * next;
};

void Initialise3DTicTacToe(TicTacToeGame3D * tttg);

void Print3DTicTacToe(TicTacToeGame3D * tttg);

void Print3DTicTacToeFromArray(int ** game_array);

void Reset3DTicTacToeArray(int ** game_array, TicTacToeGame3D * tttg);

int Process3DTicTacToeResponse(char * response, TicTacToeGame3D * tttg);

int Computer3DTicTacToeChoiceNotRandom(TicTacToeGame3D * tttg);

int Computer3DTicTacToeProcess(int field, int row, int column, TicTacToeGame3D * tttg);

bool Is3DValid(int field, int row, int column, TicTacToeGame3D * tttg);

bool Check3DGameWon(TicTacToeGame3D * tttg);

bool Check3DColumn(int column, TicTacToeGame3D * tttg);

bool Check3DColumnVertical(int row, int column, TicTacToeGame3D * tttg);

bool Check3DRow(int row, TicTacToeGame3D * tttg);

bool Check3DRowsLeftToRight(int row, TicTacToeGame3D * tttg);

bool Check3DRowsRightToLeft(int row, TicTacToeGame3D * tttg);

bool Check3DTopLeftBotRight(TicTacToeGame3D * tttg);

bool Check3DTopLeftBotRightVerticle(TicTacToeGame3D * tttg);

bool Check3DBotRightTopLeftVerticle(TicTacToeGame3D * tttg);

bool Check3DTopRightBotLeft(TicTacToeGame3D * tttg);

bool Check3DTopRightBotLeftVerticle(TicTacToeGame3D * tttg);

void Play3DTicTacToe(int socket_id,char * username, pthread_mutex_t random_mutex);

void Play3DTicTacToeClient(int socket_id);

#endif //__SENDDATA_H__
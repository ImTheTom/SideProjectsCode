#ifndef __TICTACTOE_H__
#define __TICTACTOE_H__
#include <stdbool.h>
#include <pthread.h>

#define EMPTY 0
#define NAUGHT 1
#define CROSS 2
#define ROWS 3
#define COLUMNS 3

typedef struct{
    int state;
} Tile;

typedef struct{
    Tile board[3][3];
    bool game_over;
    int turn;
} TicTacToeGame;

struct Moves{
    int row;
    int column;
    struct Moves * next;
};

void InitialiseTicTacToe(TicTacToeGame * tttg);

void PrintTicTacToe(TicTacToeGame * tttg);

void PrintTicTacToeFromArray(int ** game_array);

void ResetTicTacToeArray(int ** game_array, TicTacToeGame * tttg);

int ProcessTicTacToeResponse(char * response, TicTacToeGame * tttg, int random);

int ComputerTicTacToeChoice(TicTacToeGame * tttg);

int ComputerTicTacToeChoiceNotRandom(TicTacToeGame * tttg);

int ComputerTicTacToeProcess(int row, int column, TicTacToeGame * tttg);

bool IsValid(int row, int column, TicTacToeGame * tttg);

bool CheckGameWon(TicTacToeGame * tttg);

bool CheckColumn(int column, TicTacToeGame * tttg);

bool CheckRow(int row, TicTacToeGame * tttg);

bool CheckTopLeftBotRight(TicTacToeGame * tttg);

bool CheckTopRightBotLeft(TicTacToeGame * tttg);

void PlayTicTacToe(int socket_id,char * username, pthread_mutex_t random_mutex, int random);

void PlayTicTacToeClient(int socket_id);

#endif //__SENDDATA_H__
#ifndef __HANGMAN_H__
#define __HANGMAN_H__

#include <stdbool.h>
#include <pthread.h>

#define MAX_WORD_SIZE 20

typedef struct{
    char word[MAX_WORD_SIZE];
    bool game_over;
    int lives;
    int word_length;
    int unkown_letters;
    char guessed_word[MAX_WORD_SIZE];
} HangmanGame;

void InitialiseHangman(HangmanGame * hmg);

void ShowGallows(int lives);

void PrintGallows(int number_wrong);

int ProcessHangmanResponse(HangmanGame * hmg, int character);

int Lower(int character);

void PlayHangman(int new_fd,char * username, pthread_mutex_t random_mutex);

void PlayHangmanClient(int socket_id);

#endif //__HANGMAN_H__
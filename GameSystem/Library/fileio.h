#ifndef __FILEIO_H__
#define __FILEIO_H__

#define MAX_WORD_SIZE 20
#define LINES_TO_SEND 10
#define NO_INFORMATION "NO INFORMATION"

int GetNumberOfLines(char * file_name);

int GetNumberOfWords(char * file_name);

char * SelectRandomWord(char * file_name, int words);

void WriteRecord(char * username, int won, char * game, int seconds);

void SendRecords(int socket_id);

void PrintRecords(int socket_id);

#endif //__HANGMAN_H__
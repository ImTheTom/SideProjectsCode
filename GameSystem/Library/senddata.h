#ifndef __SENDDATA_H__
#define __SENDDATA_H__

void SendInteger(int socket_id, int integer);

int RecieveInteger(int socket_id);

void SendString(int socket_id, char * string, int length);

char * RecieveString(int socket_id, int length);

void SendArrayInt(int socket_id, int ** send_array, int number_of_rows, int number_of_columns);

void RecieveArrayInt(int socket_id, uint16_t ** byte_array, int ** recieved_array, int number_of_rows, int number_of_columns);

#endif //__SENDDATA_H__
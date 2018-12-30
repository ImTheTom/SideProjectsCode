all: server client

client : client.c Library/*.c
	gcc -Wall -o client client.c Library/*.c

server : serverpool.c Library/*.c
	gcc -Wall -o server serverpool.c Library/*.c -lpthread

clean :
	rm server client
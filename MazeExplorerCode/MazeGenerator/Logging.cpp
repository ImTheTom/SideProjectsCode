#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>
#include "Logging.h"

void LogStartedGame() {
	std::cout << "Maze Game Started!\n" << std::endl;
}

void LogCreatedObjects() {
	std::cout << "The Text, The Timer, The Maze, The Walls, The Start and End Pos, The Player and The AI were created.\n" << std::endl;
}

void LogGameBegin() {
	std::cout << "\nGame has started" << std::endl;
}

void LogGameEnd(std::string time) {
	std::cout << "\nGame has Ended for a total length of " << time << "." << std::endl;
}

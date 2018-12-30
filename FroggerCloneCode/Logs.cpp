#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>
#include <Logs.h>

//Functions for the console logs

void WindowCreated() {
	std::cout << "Window Created" << std::endl;
	std::cout << "Now Loading Sprites...\n" << std::endl;
}

void FinishedSprites() {
	std::cout << "\nLoading Sprites complete!" << std::endl;
	std::cout << "Now Loading Font...\n" << std::endl;
}

void FinishedFont() {
	std::cout << "\nLoading Font complete!" << std::endl;
	std::cout << "Now Showing Splash screen...\n" << std::endl;
}

void PlayerMoved(int num) {
	if (num == 1) {
		std::cout << "Player Moved Right" << std::endl;
	} else if (num == 0) {
		std::cout << "Player Moved Left" << std::endl;

	} else if (num == 2) {
		std::cout << "Player Moved up" << std::endl;

	} else if (num == 3) {
		std::cout << "Player Moved down" << std::endl;

	}
}

void RestartedGame() {
	std::cout << "\nPressed Space Bar!" << std::endl;
	std::cout << "Now Loading New Game...\n" << std::endl;
}

void PlayerDied() {
	std::cout << "\nPlayer was killed.\n" << std::endl;
	std::cout << "Resetting Position.\n" << std::endl;
}
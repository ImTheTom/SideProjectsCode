#include <SFML/Graphics.hpp>
#include <list>
#include <iostream>
#include <string>
#include <math.h>
#include <vector>
#include "Player.h"
#include "Maze.h"
#include "Timer.h"
#include "AIFinder.h"
#include "Loading.h"
#include "Logging.h"
#include "Results.h"
#include "TestCase.h"

int totalCases = 0;

void RunTestCase() {
	RunMazeCases();
	RunLoadingCase();
	RunLoggingCase();
	RunResultsCase();
	RunTimerCase();
	RunPlayerCases();
	RunAiFinderCases();
	std::cout << totalCases << " total cases passed out of 12" << std::endl;
}

void RunMazeCases() {
	std::cout << "\nStarting the maze cases\n" << std::endl;
	try {
		Maze maze = Maze();
		std::vector<sf::RectangleShape> walls = maze.GetWalls();
		if (walls.size() == 19) {
			totalCases += 1;
			std::cout << "Walls correct size" << std::endl;
		} else {
			std::cout << "Too little walls" << std::endl;
		}
		try {
			maze.GetEndPos();
			maze.GetStartPos();
			totalCases += 1;
			std::cout << "Found the start and end points" << std::endl;
		} catch (int e) {
			std::cout << "Couldn't find the start or end point" << std::endl;
		}
	} catch (int e) {
		std::cout << "Couldn't create maze" << std::endl;
	}
}

void RunLoadingCase() {
	std::cout << "\nStarting the loading cases\n" << std::endl;
	try {
		sf::Font font = LoadFont("walkway");
		totalCases += 1;
		std::cout << "Load Successful" << std::endl;
	} catch (int e) {
		std::cout << "Load Unscuessful" << std::endl;
	}
}

void RunLoggingCase() {
	std::cout << "\nStarting the Logging cases\n" << std::endl;
	try {
		LogStartedGame();
		LogCreatedObjects();
		LogGameBegin();
		LogGameEnd("00:01");
		totalCases += 1;
		std::cout << "Logged successful" << std::endl;
	} catch (int e) {
		std::cout << "Logged Unscuessful" << std::endl;
	}
}

void RunResultsCase() {
	std::cout << "\nStarting the Results cases\n" << std::endl;
	try {
		SaveResult(3, "Success");
		totalCases += 1;
		std::cout << "Result case successful" << std::endl;
	} catch (int e) {
		std::cout << "Result case Unscuessful" << std::endl;
	}
}

void RunTimerCase() {
	sf::Clock timer;
	try {
		StringOfTime(timer);
		totalCases += 1;
		std::cout << "Run Timer case successful" << std::endl;
	} catch (int e) {
		std::cout << "Run Timer case successful" << std::endl;
	}
}

void RunPlayerCases() {
	std::cout << "\nStarting the Player cases\n" << std::endl;
	try {
		Maze maze = Maze();
		std::vector<sf::RectangleShape> walls = maze.GetWalls();
		sf::RectangleShape startPos = maze.GetStartPos();
		sf::RectangleShape endPos = maze.GetEndPos();
		Player player = Player(startPos.getOrigin().x, startPos.getOrigin().y, endPos, walls);
		try {
			player.MoveUp();
			player.MoveDown();
			player.MoveRight();
			player.MoveLeft();
			totalCases += 1;
			std::cout << "Player could move" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't move" << std::endl;
		}
		try {
			player.IncreaseMovementSpeed();
			player.ResetMovementSpeed();
			totalCases += 1;
			std::cout << "Player could increase movement" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't increase movement" << std::endl;
		}
		try {
			player.CheckGoal();
			totalCases += 1;
			std::cout << "Player could check goal" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't check goal" << std::endl;
		}
		try {
			sf::CircleShape test = player.GetPlayer();
			player.SetPlayer(test);
			totalCases += 1;
			std::cout << "Player could update circle" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't update circle" << std::endl;
		}
	} catch (int e) {
		std::cout << "Couldn't create maze or player" << std::endl;
	}
}

void RunAiFinderCases() {
	std::cout << "\nStarting the AI cases\n" << std::endl;
	try {
		Maze maze = Maze();
		std::vector<sf::RectangleShape> walls = maze.GetWalls();
		sf::RectangleShape startPos = maze.GetStartPos();
		sf::RectangleShape endPos = maze.GetEndPos();
		Player player = Player(startPos.getOrigin().x, startPos.getOrigin().y, endPos, walls);
		AIFinder aiFinder = AIFinder(player.GetPlayer(), walls);
		try {
			aiFinder.FindGoal();
			totalCases += 1;
			std::cout << "Player could call find goal" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't call find goal" << std::endl;
		}
		try {
			sf::CircleShape test = aiFinder.GetPlayer();
			totalCases += 1;
			std::cout << "Player could call Get Player" << std::endl;
		} catch (int e) {
			std::cout << "Player couldn't call Get Player" << std::endl;
		}
	} catch (int e) {
		std::cout << "Couldn't create maze or player or AIFinder" << std::endl;
	}
}
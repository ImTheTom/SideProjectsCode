#include <SFML/Graphics.hpp>
#include <list>
#include <iostream>
#include <string>
#include <math.h>
#include <vector>
#include <iostream>
#include "Player.h"
#include "Maze.h"
#include "Timer.h"
#include "AIFinder.h"
#include "Loading.h"
#include "Logging.h"
#include "Results.h"
#include "TestCase.h"

#define COMPUTERCONTROLLED 2
#define TESTCASE 3

bool stopGame = false;
bool called = false;

std::string actualTime;

int main(int argc, char *argv[]){

	srand(time(NULL));
	// Get what state to play the game in
	int option;

	std::cout << "Enter 1 to play or 2 for computer or 3 for test cases: ";
	std::cin >> option;
	std::cout << "You entered " << option << std::endl;

	if (option != TESTCASE) {

		LogStartedGame();

		// Creates the text variables
		sf::Font mainFont = LoadFont("walkway");
		sf::Text GameoverText;
		GameoverText.setFont(mainFont);
		GameoverText.setString("Game Over.\nPress space to close.");
		GameoverText.setFillColor(sf::Color::Yellow);
		GameoverText.setOrigin(-200, -200);

		sf::Text timerText;
		timerText.setFont(mainFont);
		timerText.setFillColor(sf::Color::White);

		// Create the variables
		Maze maze = Maze();

		std::vector<sf::RectangleShape> walls = maze.GetWalls();

		sf::RectangleShape startPos = maze.GetStartPos();
		sf::RectangleShape endPos = maze.GetEndPos();

		Player player = Player(startPos.getOrigin().x, startPos.getOrigin().y, endPos, walls);

		AIFinder aiFinder = AIFinder(player.GetPlayer(), walls);

		LogCreatedObjects();

		// Set the antialising settings and create the window and timer
		sf::ContextSettings settings;
		settings.antialiasingLevel = 8;

		sf::RenderWindow window(sf::VideoMode(600, 600), "Maze Generator", sf::Style::Default, settings);

		sf::Clock timer;

		LogGameBegin();

		while (window.isOpen()) {
			sf::Event event;
			while (window.pollEvent(event)) {
				if (event.type == sf::Event::Closed)
					window.close();
				// If the game option is 1 and not in the finish position get the key movements and move accordingly
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up) && !stopGame && option == 1) {
					player.MoveUp();
					player.IncreaseMovementSpeed();
				} else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Down) && !stopGame && option == 1) {
					player.MoveDown();
					player.IncreaseMovementSpeed();
				} else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left) && !stopGame && option == 1) {
					player.MoveLeft();
					player.IncreaseMovementSpeed();
				} else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right) && !stopGame && option == 1) {
					player.MoveRight();
					player.IncreaseMovementSpeed();
				} else {
					player.ResetMovementSpeed();
				}
				// If the game is in the stop state wait for the space key to close the window
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space) && stopGame) {
					window.close();
				}
			}

			// Clear and redraw the objects every screen.
			window.clear(sf::Color::Black);

			for (int i = 0; i < walls.size(); i++) {
				window.draw(walls[i]);
			}

			if (!stopGame && option == COMPUTERCONTROLLED) {
				aiFinder.FindGoal();
				player.SetPlayer(aiFinder.GetPlayer());
			}

			stopGame = player.CheckGoal();

			window.draw(startPos);
			window.draw(endPos);
			window.draw(player.GetPlayer());

			// If not in stop game continue to update the timer string
			if (!stopGame) {
				actualTime = StringOfTime(timer);
				timerText.setString(actualTime);
			}

			window.draw(timerText);

			// If in the stop game state draw the game over text and save the result once
			if (stopGame) {
				window.draw(GameoverText);
				if (!called) {
					LogGameEnd(actualTime);
					SaveResult(option, actualTime);
					called = true;
				}
			}

			window.display();
		}
	} else {
		RunTestCase();
		system("pause");
	}
	return 0;
}
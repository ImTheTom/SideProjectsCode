#include <SFML/Graphics.hpp>
#include "AIFinder.h"
#include <windows.h>

AIFinder::AIFinder(sf::CircleShape circle, std::vector<sf::RectangleShape> Walls) {
	playerCircle = circle;
	walls = Walls;
	movementArray[0] = true; // Right
	movementArray[1] = false; // Left
	movementArray[2] = true; // Down
	movementArray[3] = false; // Up
}

// This function makes the circle travel down and right. If it can't travel righht anymore it
// Travels left. Same with the down movement but in the up direction.
void AIFinder::FindGoal() {
		if (movementArray[0]) {
			playerCircle.move(rand() % 5, 0);
			for (int i = 0; i < walls.size(); i++) {
				if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
					playerCircle.move(-5, 0);
					movementArray[0] = false;
					movementArray[1] = true;
				}
			}
		}
		else if (movementArray[1]) {
			playerCircle.move(-rand() % 5, 0);
			for (int i = 0; i < walls.size(); i++) {
				if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
					playerCircle.move(5, 0);
					movementArray[1] = false;
					movementArray[0] = true;
				}
			}
		}
		if (movementArray[2]) {
			playerCircle.move(0, rand() % 5);
			for (int i = 0; i < walls.size(); i++) {
				if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
					playerCircle.move(0, -5);
					movementArray[2] = false;
					movementArray[3] = true;
				}
			}
		}
		else if(movementArray[3]) {
			playerCircle.move(0, -rand() % 5);
			for (int i = 0; i < walls.size(); i++) {
				if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
					playerCircle.move(0, 5);
					movementArray[3] = false;
					movementArray[2] = true;
				}
			}
		}
		Sleep(10);
}

sf::CircleShape AIFinder::GetPlayer() {
	return playerCircle;
}
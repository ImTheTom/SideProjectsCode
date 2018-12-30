#include <SFML/Graphics.hpp>
#include "Player.h"

Player::Player(int startx, int starty, sf::RectangleShape EndPosition, std::vector<sf::RectangleShape> Walls) {
	startPos[0][0] = startx;
	startPos[0][1] = starty;
	walls = Walls;
	endPos = EndPosition;
	sf::CircleShape circle(12);
	circle.setOrigin(startPos[0][0], startPos[0][1]);
	circle.setFillColor(sf::Color(0, 0, 255, 255)); // Blue
	playerCircle = circle;
}

sf::CircleShape Player::GetPlayer() {
	return playerCircle;
}

void Player::SetPlayer(sf::CircleShape circle) {
	playerCircle = circle;
}

bool Player::CheckGoal() {
	if (playerCircle.getGlobalBounds().intersects(endPos.getGlobalBounds())) {
		return true;
	}
	return false;
}

void Player::MoveUp() {
	playerCircle.move(0, -playerMovement);
	for (int i = 0; i < walls.size(); i++) {
		if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
			playerCircle.move(0, 5);
			ResetMovementSpeed();
		}
	}
}

void Player::MoveDown() {
		playerCircle.move(0, playerMovement);
		for (int i = 0; i < walls.size(); i++) {
			if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
				playerCircle.move(0, -5);
				ResetMovementSpeed();
			}
		}
	}

void Player::MoveRight() {
		playerCircle.move(playerMovement, 0);
		for (int i = 0; i < walls.size(); i++) {
			if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
				playerCircle.move(-5, 0);
				ResetMovementSpeed();
			}
		}
	}

void Player::MoveLeft() {
		playerCircle.move(-playerMovement, 0);
		for (int i = 0; i < walls.size(); i++) {
			if (playerCircle.getGlobalBounds().intersects(walls[i].getGlobalBounds())) {
				playerCircle.move(5, 0);
				ResetMovementSpeed();
			}
		}
}

void Player::IncreaseMovementSpeed() {
	if (playerMovement < 3) {
		playerMovement += 1;
	}
}

void Player::ResetMovementSpeed() {
	playerMovement = 1;
}
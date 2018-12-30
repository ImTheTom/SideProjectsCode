#pragma once
#include <SFML/Graphics.hpp>

// This class is for the player circle and used to move the player
class Player {
private:
	int playerMovement=1;
	sf::RectangleShape endPos;
	int startPos[1][1];
	sf::CircleShape playerCircle;
	std::vector<sf::RectangleShape> walls;
public:
	Player(int startx, int starty, sf::RectangleShape EndPos, std::vector<sf::RectangleShape> Walls);
	sf::CircleShape GetPlayer();
	void SetPlayer(sf::CircleShape circle);
	void MoveUp();
	void MoveDown();
	void MoveRight();
	void MoveLeft();
	bool CheckGoal();
	void IncreaseMovementSpeed();
	void ResetMovementSpeed();
};
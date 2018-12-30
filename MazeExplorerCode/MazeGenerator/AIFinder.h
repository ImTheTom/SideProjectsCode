#pragma once
#include <SFML/Graphics.hpp>

// This class is used to control the circle if it is controlled by the computer
class AIFinder {
private:
	bool movementArray[4];
	sf::CircleShape playerCircle;
	std::vector<sf::RectangleShape> walls;
public:
	AIFinder(sf::CircleShape circle, std::vector<sf::RectangleShape> Walls);
	void FindGoal();
	sf::CircleShape GetPlayer();
};

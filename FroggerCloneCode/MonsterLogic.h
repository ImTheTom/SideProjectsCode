#pragma once
#include <SFML/Graphics.hpp>

std::vector<sf::Sprite> SetUpCars(std::vector<sf::Sprite> cars);
std::vector<sf::Sprite> MoveCars(std::vector<sf::Sprite> cars);

std::vector<sf::Sprite> SetUpBirds(std::vector<sf::Sprite> birds);
std::vector<sf::Sprite> MoveBirds(std::vector<sf::Sprite> birds);

std::vector<sf::Sprite> SetUpLillypads(std::vector<sf::Sprite> lillypads);
std::vector<sf::Sprite> MoveLillypads(std::vector<sf::Sprite> lillypads);

bool GameWon(sf::Sprite person);
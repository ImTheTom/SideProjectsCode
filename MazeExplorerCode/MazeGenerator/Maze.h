#pragma once
#include <SFML/Graphics.hpp>
#include <iostream>
#include <list>
#include <vector>

// This class is used to create the maze and it's walls
class Maze {
private:
	int startAndFinishPos[5][2];
	int outerWallOrigins[4][2];
	int innerWallVerticleOrigins[8][2];
	int innerWallHorizontalOrigins[7][2];
	sf::RectangleShape startPos;
	sf::RectangleShape endPos;
	std::vector<sf::RectangleShape> outerWalls;
	std::vector<sf::RectangleShape> innerVerticleWalls;
	std::vector<sf::RectangleShape> innerHorizontalWalls;
	void CreateInnerVerticleWalls();
	void CreateInnerHorizontalWalls();
	void CreateOuterWalls();
	void CreateWalls();
	void CreateStartAndEndPoints();
public:
	Maze();
	std::vector<sf::RectangleShape> GetWalls();
	sf::RectangleShape GetStartPos();
	sf::RectangleShape GetEndPos();
};
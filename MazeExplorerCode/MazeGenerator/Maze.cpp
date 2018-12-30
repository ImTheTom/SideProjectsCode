#include <SFML/Graphics.hpp>
#include "Maze.h"
#include <vector>
#include <cstdlib> 
#include <ctime> 
#include <iostream>

// Sets arrarys and calls the functions to create the start and finish pos as well as the walls
Maze::Maze() {
	outerWallOrigins[0][0] = -50;
	outerWallOrigins[0][1] = -50;
	outerWallOrigins[1][0] = -550;
	outerWallOrigins[1][1] = -50;
	outerWallOrigins[2][0] = -50;
	outerWallOrigins[2][1] = -50;
	outerWallOrigins[3][0] = -50;
	outerWallOrigins[3][1] = -550;

	innerWallVerticleOrigins[0][0] = -250;
	innerWallVerticleOrigins[0][1] = -100;
	innerWallVerticleOrigins[1][0] = -150;
	innerWallVerticleOrigins[1][1] = -200;
	innerWallVerticleOrigins[2][0] = -150;
	innerWallVerticleOrigins[2][1] = -400;
	innerWallVerticleOrigins[3][0] = -200;
	innerWallVerticleOrigins[3][1] = -450;
	innerWallVerticleOrigins[4][0] = -250;
	innerWallVerticleOrigins[4][1] = -400;
	innerWallVerticleOrigins[5][0] = -350;
	innerWallVerticleOrigins[5][1] = -50;
	innerWallVerticleOrigins[6][0] = -450;
	innerWallVerticleOrigins[6][1] = -300;
	innerWallVerticleOrigins[7][0] = -450;
	innerWallVerticleOrigins[7][1] = -100;


	innerWallHorizontalOrigins[0][0] = -50;
	innerWallHorizontalOrigins[0][1] = -100;
	innerWallHorizontalOrigins[1][0] = -100;
	innerWallHorizontalOrigins[1][1] = -200;
	innerWallHorizontalOrigins[2][0] = -100;
	innerWallHorizontalOrigins[2][1] = -300;
	innerWallHorizontalOrigins[3][0] = -100;
	innerWallHorizontalOrigins[3][1] = -400;
	innerWallHorizontalOrigins[4][0] = -100;
	innerWallHorizontalOrigins[4][1] = -450;
	innerWallHorizontalOrigins[5][0] = -350;
	innerWallHorizontalOrigins[5][1] = -400;
	innerWallHorizontalOrigins[6][0] = -450;
	innerWallHorizontalOrigins[6][1] = -200;

	CreateWalls();

	startAndFinishPos[0][0] = -55;
	startAndFinishPos[0][1] = -62;
	startAndFinishPos[1][0] = -155;
	startAndFinishPos[1][1] = -237;
	startAndFinishPos[2][0] = -174;
	startAndFinishPos[2][1] = -480;
	startAndFinishPos[3][0] = -488;
	startAndFinishPos[3][1] = -173;
	startAndFinishPos[4][0] = -395;
	startAndFinishPos[4][1] = -405;

	CreateStartAndEndPoints();
}

void Maze::CreateWalls() {
	CreateOuterWalls();
	CreateInnerVerticleWalls();
	CreateInnerHorizontalWalls();
}

// Gets two seperate random nums for the start and finish then creates the rectangles
// at the random location
void Maze::CreateStartAndEndPoints() {
	int start = rand() % 5;
	int end = rand() % 5;
	while (start == end) {
		end = rand() % 5;
	}

	sf::RectangleShape startRect(sf::Vector2f(25, 25));
	startRect.setOrigin(startAndFinishPos[start][0], startAndFinishPos[start][1]);
	startRect.setFillColor(sf::Color(0, 255, 0, 255));
	startPos = startRect;

	sf::RectangleShape endRect(sf::Vector2f(25, 25));
	endRect.setOrigin(startAndFinishPos[end][0], startAndFinishPos[end][1]);
	endRect.setFillColor(sf::Color(255, 0, 0, 255));
	endPos = endRect;
}

void Maze::CreateOuterWalls() {
	for (int i = 0; i < 4; i++) {
		if (i == 1 || i == 2) {
			sf::RectangleShape line(sf::Vector2f(5, 500));
			line.setOrigin(outerWallOrigins[i][0], outerWallOrigins[i][1]);
			outerWalls.push_back(line);
		} else {
			sf::RectangleShape line(sf::Vector2f(505, 5));
			line.setOrigin(outerWallOrigins[i][0], outerWallOrigins[i][1]);
			outerWalls.push_back(line);
		}
	}
}

void Maze::CreateInnerHorizontalWalls() {
	for (int i = 0; i < 7; i++) {
		if (i == 0) {
			sf::RectangleShape line(sf::Vector2f(200, 5));
			line.setOrigin(innerWallHorizontalOrigins[0][0], innerWallHorizontalOrigins[0][1]);
			innerHorizontalWalls.push_back(line);
		} else if (i == 1 || i==4 || i==5 || i ==6) {
			sf::RectangleShape line(sf::Vector2f(100, 5));
			line.setOrigin(innerWallHorizontalOrigins[i][0], innerWallHorizontalOrigins[i][1]);
			innerHorizontalWalls.push_back(line);
		} else if (i == 2 || i == 3) {
			sf::RectangleShape line(sf::Vector2f(155, 5));
			line.setOrigin(innerWallHorizontalOrigins[i][0], innerWallHorizontalOrigins[i][1]);
			innerHorizontalWalls.push_back(line);
		} 
	}
}

void Maze::CreateInnerVerticleWalls() {
	for (int i = 0; i < 8; i++) {
		if (i==7) {
			sf::RectangleShape line(sf::Vector2f(5, 150));
			line.setOrigin(innerWallVerticleOrigins[i][0], innerWallVerticleOrigins[i][1]);
			innerVerticleWalls.push_back(line);
		} else if(i==1 || i==3 || i==4) {
			sf::RectangleShape line(sf::Vector2f(5, 100));
			line.setOrigin(innerWallVerticleOrigins[i][0], innerWallVerticleOrigins[i][1]);
			innerVerticleWalls.push_back(line);
		}else if (i == 2) {
			sf::RectangleShape line(sf::Vector2f(5, 50));
			line.setOrigin(innerWallVerticleOrigins[i][0], innerWallVerticleOrigins[i][1]);
			innerVerticleWalls.push_back(line);
		} else if (i == 5) {
			sf::RectangleShape line(sf::Vector2f(5, 450));
			line.setOrigin(innerWallVerticleOrigins[i][0], innerWallVerticleOrigins[i][1]);
			innerVerticleWalls.push_back(line);
		} else if (i == 6 || i ==0) {
			sf::RectangleShape line(sf::Vector2f(5, 200));
			line.setOrigin(innerWallVerticleOrigins[i][0], innerWallVerticleOrigins[i][1]);
			innerVerticleWalls.push_back(line);
		}
	}
}

// Returns all the walls
std::vector<sf::RectangleShape> Maze::GetWalls() {
	std::vector<sf::RectangleShape> allWalls;
	allWalls = outerWalls;
	allWalls.insert(allWalls.end(), innerVerticleWalls.begin(), innerVerticleWalls.end());
	allWalls.insert(allWalls.end(), innerHorizontalWalls.begin(), innerHorizontalWalls.end());
	return allWalls;
}

sf::RectangleShape Maze::GetStartPos() {
	return startPos;
}

sf::RectangleShape Maze::GetEndPos() {
	return endPos;
}
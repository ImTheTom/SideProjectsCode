#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>
#include "Timer.h"

typedef struct {
	int m;
	int s;
} GameTimer;

GameTimer Counter;

std::string StringOfTime(sf::Clock timer) {
	sf::Time elapsedTime = timer.getElapsedTime();
	double roundedTime = round(elapsedTime.asSeconds());
	Counter.m = (int)roundedTime / 60;
	Counter.s = (int)roundedTime % 60;
	std::string actualTimeString;
	if (Counter.m < 10) {
		actualTimeString = "0" + std::to_string(Counter.m);
	} else {
		actualTimeString = std::to_string(Counter.m);
	}
	if (Counter.s < 10) {
		actualTimeString = actualTimeString + ":0" + std::to_string(Counter.s);
	} else {
		actualTimeString = actualTimeString + ":" + std::to_string(Counter.s);
	}
	return actualTimeString;
}
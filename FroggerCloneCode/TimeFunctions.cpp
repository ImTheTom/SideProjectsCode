#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>
#include <TimeFunctions.h>

//Functions for the timer in the top left. To return a nice looking string :)

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
	std::string actualTime;
	if (Counter.m < 10) {
		actualTime = "0" + std::to_string(Counter.m);
	}
	else {
		actualTime = std::to_string(Counter.m);
	}
	if (Counter.s < 10) {
		actualTime = actualTime + ":0" + std::to_string(Counter.s);
	}
	else {
		actualTime = actualTime + ":" + std::to_string(Counter.s);
	}
	return actualTime;
}
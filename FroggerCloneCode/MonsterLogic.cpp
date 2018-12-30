#include <SFML/Graphics.hpp>
#include <MonsterLogic.h>

int leftStart = 250;
int rightStart = -250;
int seperation = 150;
int restartAmount = seperation * 9;

//If the person reaches the top the function returns true
bool GameWon(sf::Sprite person) {
	if (person.getPosition().y<10) {
		return true;
	}
	return false;
}

//Function to set positions of the sprites to the right spot
std::vector<sf::Sprite> SetUpCars(std::vector<sf::Sprite> cars) {
	for (int i = 0; i < 60; i++) {
		if (i == 10 || i == 20 || i == 30 || i == 40 || i == 50) {
			leftStart = 250;
			rightStart = -250;
		}
		if (i < 9) {
			cars[i].move(leftStart, 0);
			leftStart -= seperation+rand()%30;
		} else if (i < 19) {
			cars[i].move(rightStart, 0);
			rightStart += seperation + rand() % 30;
		} else if (i < 29) {
			cars[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;
		} else if (i < 39) {
			cars[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;
		} else if (i < 49) {
			cars[i].move(rightStart, 0);
			rightStart += seperation + rand() % 30;
		} else {
			cars[i].move(rightStart, 0);
			rightStart += seperation + rand() % 30;
		}
	}
	return cars;
}

//Function that handles how fast the cars go from side to side
//If the car is off the screen reset it to the back
std::vector<sf::Sprite> MoveCars(std::vector<sf::Sprite> cars) {
	for (int i = 0; i < 60; i++) {
		if (i < 9) {
			cars[i].move(0.01, 0);
			if (cars[i].getPosition().x > 600) {
				cars[i].move(-restartAmount,0);
			}
		} else if (i < 19) {
			cars[i].move(-0.01, 0);
			if (cars[i].getPosition().x < -650) {
				cars[i].move(restartAmount, 0);
			}
		} else if (i < 29) {
			cars[i].move(0.01, 0);
			if (cars[i].getPosition().x > 600) {
				cars[i].move(-restartAmount, 0);
			}
		} else if (i < 39) {
			cars[i].move(0.01, 0);
			if (cars[i].getPosition().x > 600) {
				cars[i].move(-restartAmount, 0);
			}
		} else if (i < 49) {
			cars[i].move(-0.01, 0);
			if (cars[i].getPosition().x < -650) {
				cars[i].move(restartAmount, 0);
			}
		} else {
			cars[i].move(-0.01, 0);
			if (cars[i].getPosition().x < -650) {
				cars[i].move(restartAmount, 0);
			}
		}
	}
	return cars;
}

//Function to set positions of the sprites to the right spot
std::vector<sf::Sprite> SetUpBirds(std::vector<sf::Sprite> birds) {
	for (int i = 0; i < 30; i++) {
		if (i == 10 || i == 20) {
			leftStart = 250;
			rightStart = -250;
		}
		if (i < 9) {
			birds[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;
		} else if (i < 19) {
			birds[i].move(rightStart, 0);
			rightStart += seperation + rand() % 30;
		} else {
			birds[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;
		}
	}
	return birds;
}

//Function that handles how fast the birds go from side to side
//If the bird is off the screen reset it to the back
std::vector<sf::Sprite> MoveBirds(std::vector<sf::Sprite> birds) {
	for (int i = 0; i < 30; i++) {
		if (i < 9) {
			birds[i].move(0.01, 0);
			if (birds[i].getPosition().x > 600) {
				birds[i].move(-restartAmount, 0);
			}
		} else if (i < 19) {
			birds[i].move(0.01, 0);
			if (birds[i].getPosition().x > 600) {
				birds[i].move(-restartAmount, 0);
			}
		} else {
			birds[i].move(0.01, 0);
			if (birds[i].getPosition().x > 600) {
				birds[i].move(-restartAmount, 0);
			}
		}
	}
	return birds;
}

//Function to set positions of the sprites to the right spot
std::vector<sf::Sprite> SetUpLillypads(std::vector<sf::Sprite> lillypads) {
	for (int i = 0; i < 30; i++) {
		if (i == 10 || i == 20) {
			leftStart = 250;
			rightStart = -250;
		}
		if (i < 9) {
			lillypads[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;;
		} else if (i < 19) {
			lillypads[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;;
		} else {
			lillypads[i].move(leftStart, 0);
			leftStart -= seperation + rand() % 30;;
		}
	}
	return lillypads;
}

//Function that handles how fast the lillypads go from side to side
//If the lillypads is off the screen reset it to the back
std::vector<sf::Sprite> MoveLillypads(std::vector<sf::Sprite> lillypads) {
	for (int i = 0; i < 30; i++) {
		if (i < 9) {
			lillypads[i].move(0.01, 0);
			if (lillypads[i].getPosition().x > 600) {
				lillypads[i].move(-restartAmount, 0);
			}
		} else if (i < 19) {
			lillypads[i].move(0.01, 0);
			if (lillypads[i].getPosition().x > 600) {
				lillypads[i].move(-restartAmount, 0);
			}
		} else {
			lillypads[i].move(0.01, 0);
			if (lillypads[i].getPosition().x > 600) {
				lillypads[i].move(-restartAmount, 0);
			}
		}
	}
	return lillypads;
}
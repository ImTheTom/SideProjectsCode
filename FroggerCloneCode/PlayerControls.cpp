#include <SFML/Graphics.hpp>
#include <PlayerControls.h>
#include <Logs.h>

int lastNum = 4;

//Functions for how to move main sprite
sf::Sprite Move(sf::Sprite character, int num) {
	if (num == 0) {
		sf::Vector2f coords = character.getPosition();
		if (coords.x > 0) {
			character.move(-3, 0);
			if (lastNum != num) {
				PlayerMoved(0);
			}
		}
		lastNum = num;
		return character;
	} else if (num == 1) {
		sf::Vector2f coords = character.getPosition();
		if (coords.x < 550) {
			character.move(3, 0);
			if (lastNum != num) {
				PlayerMoved(1);
			}
		}
		lastNum = num;
		return character;
	} else if (num == 2) {
		sf::Vector2f coords = character.getPosition();
		if (coords.y > 0) {
			character.move(0, -3);
			if (lastNum != num) {
				PlayerMoved(2);
			}
		}
		lastNum = num;
		return character;
	} else if (num == 3) {
		sf::Vector2f coords = character.getPosition();
		if (coords.y < 775) {
			character.move(0, 3);
			if (lastNum != num) {
				PlayerMoved(3);
			}
		}
		lastNum = num;
		return character;
	}
}
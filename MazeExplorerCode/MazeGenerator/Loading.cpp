#include "Loading.h"

sf::Font LoadFont(std::string file) {
	sf::Font font;
	if (!font.loadFromFile(file + ".ttf"))
		std::cout << "Could not load font." << std::endl;
	return font;
}
#include <Loading.h>
#include <SFML/Graphics.hpp>
#include <iostream>
#include <string>

using namespace std;

//Functions to load textures and fonts as well outputing information to the console.

sf::Texture SetTexture(string file) {
	sf::Texture texture;
	if (!texture.loadFromFile("images/"+file+".png")) {
		throw std::runtime_error("Could not load " +file);
	}
	else {
		cout << file +" was loaded." << endl;
	}
	return texture;
}

sf::Font LoadFont(std::string file) {
	sf::Font font;
	if (!font.loadFromFile("images/" + file + ".ttf")) {
		throw std::runtime_error("Could not load " + file);
	}
	else {
		cout << file + " was loaded." << endl;
	}
	return font;
}
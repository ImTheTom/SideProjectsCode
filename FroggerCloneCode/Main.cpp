// Headers
#include <Graphics.hpp>
#include <iostream>
#include <string>
#include <Loading.h>
#include <Logs.h>
#include <math.h> 
#include <TimeFunctions.h>
#include <PlayerControls.h>
#include <vector>
#include <MonsterLogic.h>

//Globals

bool showSplash = true;
bool gameWon = false;

sf::Sprite froggerSplashSprite;
sf::Sprite backgroundSprite;
sf::Sprite mainGuySprite;
sf::Texture car1Texture;
sf::Texture car2Texture;
sf::Texture car3Texture;
sf::Texture lillypadTexture;
sf::Texture birdTexture;

int main(){

	srand(time(NULL));

	sf::RenderWindow window(sf::VideoMode(600, 800), "Frogger");

	WindowCreated();

	//Loading the textures using the created function SetTexture.

	sf::Texture froggerSplashTexture = SetTexture("FroggerSplash");
	froggerSplashSprite.setTexture(froggerSplashTexture);

	sf::Texture backgroundTexture = SetTexture("FroggerBackground");
	backgroundSprite.setTexture(backgroundTexture);

	sf::Texture mainGuyTexture = SetTexture("MainGuy");
	mainGuySprite.setTexture(mainGuyTexture);
	mainGuySprite.move(300, 760);

	car1Texture = SetTexture("car1");
	car2Texture = SetTexture("car2");
	car3Texture = SetTexture("car3");
	lillypadTexture = SetTexture("lillypad");
	birdTexture = SetTexture("bird");

std::string 
	//Creating vectors to hold all the monster sprites

	std::vector<sf::Sprite>cars(60, sf::Sprite(car1Texture));
	std::vector<sf::Sprite>lillypadSprites(30, sf::Sprite(lillypadTexture));
	std::vector<sf::Sprite>birdSprites(30, sf::Sprite(birdTexture));

	//Looping through to set textures and starting positions of all the monsters
	//Also calling SetUp functions for particular monsters to set the positons more accurately.

	for (int i = 0; i < 60; i++) {
		if (i < 19) {
			cars[i].setTexture(car1Texture);
		} else if (i < 39) {
			cars[i].setTexture(car2Texture);
		} else {
			cars[i].setTexture(car3Texture);
		}
		if (i < 9) {
			cars[i].move(23, 720);
		} else if (i < 19) {
			cars[i].move(523, 670);
		} else if (i < 29) {
			cars[i].move(0, 300);
		} else if (i < 39) {
			cars[i].move(15, 250);
		} else if (i < 49) {
			cars[i].move(560, 200);
		} else {
			cars[i].move(580, 150);
		}
	}

	cars = SetUpCars(cars);

	for (int i = 0; i < 30; i++) {
		lillypadSprites[i].setTexture(lillypadTexture);
		if (i < 9) {
			lillypadSprites[i].move(0, 510);
		} else if (i < 19) {
			lillypadSprites[i].move(0, 460);
		} else {
			lillypadSprites[i].move(0, 50);
		}
	}

	lillypadSprites = SetUpLillypads(lillypadSprites);

	for (int i = 0; i < 30; i++) {
		birdSprites[i].setTexture(birdTexture);
		if (i < 9) {
			birdSprites[i].move(0, 600);
		} else if (i < 19) {
			birdSprites[i].move(550, 350);
		} else {
			birdSprites[i].move(0, 100);
		}
	}

	birdSprites = SetUpBirds(birdSprites);


	FinishedSprites();

	//Setting up the font which will be used throughout the game.

	sf::Font mainFont = LoadFont("walkway");
	sf::Text introText;
	introText.setFont(mainFont);
	introText.setString("Frogger\nCreated by Tom.\nPress Space to begin.");
	introText.setFillColor(sf::Color::Black);
	introText.setOrigin(-150, -200);

	sf::Text endText;
	endText.setFont(mainFont);
	endText.setString("Game Over, you Won.\n Press space to play again");
	endText.setFillColor(sf::Color::Black);
	endText.setOrigin(-150, -200);

	sf::Text timerText;
	timerText.setFont(mainFont);
	timerText.setFillColor(sf::Color::Black);

	FinishedFont();

	sf::Clock timer;

	//Starting the main game window

	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) {
				window.close();
			}
			if (event.type == sf::Event::Resized){
				std::cout << "new width: " << event.size.width << std::endl;
				std::cout << "new height: " << event.size.height << std::endl;
			}
			//If space is pressed and the show splash is true then it will start the game
			//Or it will restart the game if the end game is shown
			//Or it will move the sprite if the game loop is running.
			if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space) && showSplash){
				showSplash = false;
				timer.restart();
				RestartedGame();
			} else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space) && gameWon) {
				mainGuySprite.setPosition(300, 760);
				showSplash = true;
				gameWon = false;
			}
			else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left) && !showSplash && !gameWon) {
				mainGuySprite = Move(mainGuySprite, 0);
			}
			else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right) && !showSplash && !gameWon) {
				mainGuySprite = Move(mainGuySprite, 1);
			}
			else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up) && !showSplash && !gameWon) {
				mainGuySprite = Move(mainGuySprite, 2);
			}
			else if (sf::Keyboard::isKeyPressed(sf::Keyboard::Down) && !showSplash && !gameWon) {
				mainGuySprite = Move(mainGuySprite, 3);
			}
		}
		//If showsplash is true then it will show the splash screen
		//If the gamewon is true then it will show the game won screen
		//or else it will run the main game loop
		if (showSplash) {
			window.clear(sf::Color::Black);
			window.draw(froggerSplashSprite);
			window.draw(introText);
		} else if (gameWon) {
			window.clear(sf::Color::Black);
			window.draw(froggerSplashSprite);
			window.draw(endText);
		}else{
			window.clear(sf::Color::Black);
			window.draw(backgroundSprite);
			window.draw(mainGuySprite);
			//Moving the sprites and drawing them
			cars = MoveCars(cars);
			for (int i = 0; i < 60; i++) {
				window.draw(cars[i]);
			}
			lillypadSprites = MoveLillypads(lillypadSprites);
			birdSprites = MoveBirds(birdSprites);
			for (int i = 0; i < 30; i++) {
				window.draw(lillypadSprites[i]);
				window.draw(birdSprites[i]);
			}
			//Checking if the sprites overlap with the main guy sprite. If so then reset the player.
			for (int i = 0; i < 60; i++) {
				if (mainGuySprite.getGlobalBounds().intersects(cars[i].getGlobalBounds())) {
					mainGuySprite.setPosition(300, 760);
					PlayerDied();
				}
			}
			for (int i = 0; i < 30; i++) {
				if (mainGuySprite.getGlobalBounds().intersects(birdSprites[i].getGlobalBounds())) {
					mainGuySprite.setPosition(300, 760);
					PlayerDied();
				}
			}
			for (int i = 0; i < 30; i++) {
				if (mainGuySprite.getGlobalBounds().intersects(lillypadSprites[i].getGlobalBounds())) {
					mainGuySprite.setPosition(300, 760);
					PlayerDied();
				}
			}
			//Check if the player reached the end
			if (GameWon(mainGuySprite)) {
				gameWon = true;
			}
			//Update timer and draw it.
			std::string actualTime = StringOfTime(timer);
			timerText.setString(actualTime);
			window.draw(timerText);
		}
		window.display();
	}
	return 0;
}

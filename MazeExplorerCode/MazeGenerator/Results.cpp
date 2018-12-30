#include "Results.h"
#include <iostream>
#include <fstream>
#include <string>

// This function saves the option and the length of the time in a text file
void SaveResult(int option, std::string result) {
	std::ofstream textFile;
	textFile.open("results.txt", std::ios::app);
	std::string outputString = std::to_string(option) + " " + result;
	textFile << outputString << "\n";
	textFile.close();
}
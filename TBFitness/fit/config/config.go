package config

import (
	"encoding/json"
	"os"
)

// Configuration ...
type Configuration struct {
	MySQLConnection string
}

// GetConfig ...
func GetConfig(version string) (*Configuration, error) {
	fileLocation := "/fit/config/config.development.json"
	if version == "production" {
		fileLocation = "./config/config.production.json"
	} else if version == "testing" {
		fileLocation = "./config.development.json"
	}

	file, err := os.Open(fileLocation)
	if err != nil {
		return nil, err
	}
	defer file.Close()

	decoder := json.NewDecoder(file)
	var config Configuration
	err = decoder.Decode(&config)
	if err != nil {
		return nil, err
	}
	return &config, err
}

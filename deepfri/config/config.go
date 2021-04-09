package config

import (
	"sync"

	"github.com/spf13/viper"
)

// DBConfig is a structure of a database configuration
type DBConfig struct {
	DBHost string
	DBPort string
	DBName string
	DBUser string
	DBPass string
	DBSSL  string
}

var syncConfigureOnce sync.Once

// Configure ...
func Configure() {
	syncConfigureOnce.Do(configure)
}

func configure() {
	viper.SetEnvPrefix("DEEPFRI")
	configureDatabase()
	envVarSetup("PORT", 5000)
	envVarSetup("HOST", "0.0.0.0")
}

func envVarSetup(name string, defaultValue interface{}) {
	viper.SetDefault(name, defaultValue)
	viper.BindEnv(name)
}

// GetListenAddress ...
func GetListenAddress() string {
	return viper.GetString("HOST") + ":" + viper.GetString("PORT")
}

func configureDatabase() {
	envVarSetup("DB_HOST", "db.deepfri")
	envVarSetup("DB_PORT", "5432")
	envVarSetup("DB_NAME", "deepfri")
	envVarSetup("DB_USER", "postgres")
	envVarSetup("DB_PASS", "")
	envVarSetup("DB_SSL", "disable")
}

// GetDB returns the database configuration structure
func GetDB() DBConfig {
	return DBConfig{
		DBHost: viper.GetString("DB_HOST"),
		DBPort: viper.GetString("DB_PORT"),
		DBName: viper.GetString("DB_NAME"),
		DBUser: viper.GetString("DB_USER"),
		DBPass: viper.GetString("DB_PASS"),
		DBSSL:  viper.GetString("DB_SSL"),
	}
}

package database

import (
	"database/sql"
	"deepfri/config"
	"fmt"

	_ "github.com/lib/pq" // blank import to get the postgres package
)

// MaxOpenConnectionPerNode ...
const MaxOpenConnectionPerNode = 10

var dbInstance DataStore

// PostgresDataStore ...
type PostgresDataStore struct {
	DB *sql.DB
}

// DataStore ...
type DataStore interface {
	ConnCheck() bool
	Query(query string, params []interface{}) (*sql.Rows, error)
	QueryRow(query string, params []interface{}) *sql.Row
	Exec(query string, params []interface{}) (sql.Result, error)
	Insert(query string, params []interface{}) (int, error)
}

// NewPostgresDataStore ...
func NewPostgresDataStore() {
	db := PostgresDB()
	if db != nil {
		var d = &PostgresDataStore{
			DB: db,
		}

		dbInstance = d
	}
}

// GetInstance ...
func GetInstance() DataStore {
	if dbInstance == nil || !dbInstance.ConnCheck() {
		NewPostgresDataStore()
	}

	return dbInstance
}

// SetDataStore Sets the datastore
func SetDataStore(d DataStore) {
	dbInstance = d
}

// PostgresDB Create postgres client
func PostgresDB() *sql.DB {
	var db *sql.DB
	var dbConfig = config.GetDB()

	psqlSource := fmt.Sprintf(
		"host='%s' port='%s' user='%s' password='%s' dbname='%s' sslmode='%s' fallback_application_name='%s'",
		dbConfig.DBHost,
		dbConfig.DBPort,
		dbConfig.DBUser,
		dbConfig.DBPass,
		dbConfig.DBName,
		dbConfig.DBSSL,
		"deep_fri",
	)

	db, err := sql.Open("postgres", psqlSource)
	if err != nil {
		fmt.Println(err)
		return nil
	}

	err = db.Ping()
	if err != nil {
		fmt.Println(err)
		return nil
	}

	db.SetMaxOpenConns(MaxOpenConnectionPerNode)

	return db
}

// ConnCheck Bool check if connection exists
func (d PostgresDataStore) ConnCheck() bool {
	err := d.DB.Ping()
	return err == nil
}

// Query queries multiple rows
func (d PostgresDataStore) Query(query string, params []interface{}) (*sql.Rows, error) {
	return d.DB.Query(query, params...)
}

// QueryRow Execute query and returns single row
func (d PostgresDataStore) QueryRow(query string, params []interface{}) *sql.Row {
	return d.DB.QueryRow(query, params...)
}

// Exec ...
func (d PostgresDataStore) Exec(query string, params []interface{}) (sql.Result, error) {
	return d.DB.Exec(query, params...)
}

// Insert assumes the query will be RETURNING an int
func (d PostgresDataStore) Insert(query string, params []interface{}) (int, error) {
	var id int
	err := d.QueryRow(query, params).Scan(&id)
	return id, err
}

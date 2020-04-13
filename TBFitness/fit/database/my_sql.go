package database

import (
	"database/sql"

	_ "github.com/go-sql-driver/mysql"
)

var instance DataStore

// MySQLDataStore ...
type MySQLDataStore struct {
	DB *sql.DB
}

// DataStore ...
type DataStore interface {
	Query(query string, params []interface{}) (*sql.Rows, error)
	QueryRow(query string, params []interface{}) *sql.Row
	Exec(query string, params []interface{}) (sql.Result, error)
	Insert(query string, params []interface{}) (sql.Result, error)
	Update(query string, params []interface{}) (int64, error)
}

// NewMySQLDataStore ...
func NewMySQLDataStore(connectionURL string) *DataStore {
	instance = MySQLDataStore{
		DB: mySQLDB(connectionURL),
	}
	return &instance
}

// SetDataStore ...
func SetDataStore(d DataStore) {
	instance = d
}

// GetDataStore ...
func GetDataStore() DataStore {
	if instance == nil {
		panic("DataStore has not been set")
	}

	return instance
}

// PostgresDB Create postgres client
func mySQLDB(connectionURL string) *sql.DB {
	db, err := sql.Open("mysql", connectionURL)
	if err != nil {
		panic(err.Error()) // Just for example purpose. You should use proper error handling instead of panic
	}

	// Open doesn't open a connection. Validate DSN data:
	err = db.Ping()
	if err != nil {
		panic(err.Error()) // proper error handling instead of panic in your app
	}
	return db
}

// Query queries multiple rows
func (d MySQLDataStore) Query(query string, params []interface{}) (*sql.Rows, error) {
	return d.DB.Query(query, params...)
}

// QueryRow Execute query and returns single row
func (d MySQLDataStore) QueryRow(query string, params []interface{}) *sql.Row {
	return d.DB.QueryRow(query, params...)
}

// Exec ...
func (d MySQLDataStore) Exec(query string, params []interface{}) (sql.Result, error) {
	return d.DB.Exec(query, params...)
}

// Insert assumes the query will be RETURNING an int
func (d MySQLDataStore) Insert(query string, params []interface{}) (sql.Result, error) {
	return d.Exec(query, params)
}

// Update returns affected rows
func (d MySQLDataStore) Update(query string, params []interface{}) (int64, error) {
	return getAffectedRows(d.Exec(query, params))
}

func getAffectedRows(result sql.Result, err error) (int64, error) {
	if err != nil {
		return 0, err
	}

	id, err := result.RowsAffected()
	if err != nil {
		return 0, err
	}
	return id, nil
}

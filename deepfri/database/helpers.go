package database

import (
	"database/sql"
)

// ScanRowToMap takes a row and assigns it to a map
func ScanRowToMap(row *sql.Row, colNames []string) (map[string]interface{}, error) {
	// https://kylewbanks.com/blog/query-result-to-map-in-golang
	// Create a slice of interface{}'s to represent each column,
	// and a second slice to contain pointers to each item in the columns slice.
	columns := make([]interface{}, len(colNames))
	columnPointers := make([]interface{}, len(colNames))
	for i := range columns {
		columnPointers[i] = &columns[i]
	}

	// Scan the result into the column pointers...
	if err := row.Scan(columnPointers...); err != nil {
		return nil, err
	}

	// Create our map, and retrieve the value for each column from the pointers slice,
	// storing it in the map with the name of the column as the key.
	m := make(map[string]interface{})
	for i, colName := range colNames {
		val := columnPointers[i].(*interface{})
		m[colName] = *val
	}

	return m, nil
}

// ScanRowsToMap ...
func ScanRowsToMap(rows *sql.Rows, colNames []string) ([]map[string]interface{}, error) {
	var s []map[string]interface{}

	for rows.Next() {
		// https://kylewbanks.com/blog/query-result-to-map-in-golang
		// Create a slice of interface{}'s to represent each column,
		// and a second slice to contain pointers to each item in the columns slice.
		columns := make([]interface{}, len(colNames))
		columnPointers := make([]interface{}, len(colNames))
		for i := range columns {
			columnPointers[i] = &columns[i]
		}

		// Scan the result into the column pointers...
		if err := rows.Scan(columnPointers...); err != nil {
			return nil, err
		}

		// Create our map, and retrieve the value for each column from the pointers slice,
		// storing it in the map with the name of the column as the key.
		m := make(map[string]interface{})
		for i, colName := range colNames {
			val := columnPointers[i].(*interface{})
			m[colName] = *val
		}
		s = append(s, m)
	}

	return s, nil
}

package database

import (
	"fmt"
	"sort"
	"strings"
)

func getPlaceholders(count int) string {
	placeholders := make([]string, count)
	for i := 0; i < count; i++ {
		placeholders[i] = "?"
	}
	return strings.Join(placeholders, ", ")
}

func getKeysFromMap(data map[string]interface{}) []string {
	keys := make([]string, len(data))
	i := 0
	for key := range data {
		keys[i] = key
		i++
	}
	sort.Strings(keys)
	return keys
}

func getValuesFromMap(data map[string]interface{}) []interface{} {
	vals := make([]interface{}, len(data))
	keys := getKeysFromMap(data)
	for i, key := range keys {
		vals[i] = data[key]
	}
	return vals
}

func InsertMap(m map[string]interface{}, table string, pkey string, safeInsert bool) (int64, error) {
	// Remove primary key from map, as it will likely be zero/empty
	if !safeInsert {
		delete(m, pkey)
	}

	// Build query
	keys := getKeysFromMap(m)
	// nolint: gas
	query := fmt.Sprintf(
		"INSERT INTO %s (%s) VALUES (%s)",
		table,
		strings.Join(keys, ","),
		getPlaceholders(len(m)),
	)

	params := getValuesFromMap(m)

	// Get results
	db := GetDataStore()

	result, err := db.Insert(query, params)
	if err != nil {
		return 0, err
	}
	return result.LastInsertId()
}

// UpdateMap ...
func UpdateMap(m map[string]interface{}, table string, pkey string) error {
	// Remove key from update fields
	id := m[pkey]
	delete(m, pkey)

	// Build query
	keys := getKeysFromMap(m)
	params := getValuesFromMap(m)
	placeholders := make([]string, len(keys))
	for i, k := range keys {
		placeholders[i] = fmt.Sprintf("%s = ?", k)
	}
	// nolint: gas
	query := fmt.Sprintf(
		"UPDATE %s SET %s WHERE %s = %d",
		table,
		strings.Join(placeholders, ", "),
		pkey,
		id,
	)

	db := GetDataStore()
	affectedRows, err := db.Update(query, params)

	if affectedRows != 1 {
		return fmt.Errorf("expected one affected row but found %d", affectedRows)
	}

	return err
}

package database

import (
	"database/sql"
	"errors"
	"fmt"
)

var imageResponseColumns = []string{
	"images_id",
	"images_original_name",
	"images_file_name",
	"images_filter",
	"images_caption",
	"images_url",
	"images_width",
	"images_height",
	"images_timestamp",
}

func InsertImage(m map[string]interface{}) (int, error) {
	delete(m, "images_id")

	query := fmt.Sprintf(
		`INSERT INTO
			images (images_original_name, images_file_name, images_filter, images_caption, images_url, images_timestamp)
		VALUES
			($1, '', $2, $3, '', $4)
		RETURNING
			images_id`,
	)

	params := []interface{}{m["images_original_name"], m["images_filter"], m["images_caption"], m["images_timestamp"]}

	db := GetInstance()
	if db != nil {
		return db.Insert(query, params)
	}

	return 0, nil
}

// RetrieveImage ...
func RetrieveImage(imageID int) (map[string]interface{}, error) {
	query := `
		SELECT *
		FROM images
		WHERE images_id = $1
		LIMIT 1
	`
	params := []interface{}{imageID}
	db := GetInstance()
	if db == nil {
		return nil, fmt.Errorf("Could not connect to DB")
	}
	row := db.QueryRow(query, params)

	if row == nil {
		return nil, fmt.Errorf("Could not retrieve Image with ID %d", imageID)
	}

	result, err := ScanRowToMap(row, imageResponseColumns)
	if err != nil {
		if err == sql.ErrNoRows {
			return nil, fmt.Errorf("Could not retrieve Image with ID %d", imageID)
		}
		return nil, err
	}

	return result, nil
}

func UpdateProcessedImage(name string, width, height, imageID int) error {
	query := `
		UPDATE
			images
		SET
			images_file_name = $1,
			images_width = $2,
			images_height = $3
		WHERE
			images_id = $4
	`
	params := []interface{}{name, width, height, imageID}
	db := GetInstance()
	if db == nil {
		return fmt.Errorf("Could not connect to DB")
	}
	_, err := db.Exec(query, params)
	return err
}

// RetrieveRecentImages ...
func RetrieveRecentImages(amount int) ([]map[string]interface{}, error) {
	query := `
		SELECT *
		FROM images
		WHERE images_file_name != ''
		ORDER BY images_id desc
		LIMIT $1
	`
	params := []interface{}{amount}
	db := GetInstance()
	if db != nil {
		rows, err := db.Query(query, params)

		if err != nil {
			return nil, err
		}
		defer rows.Close()

		result, err := ScanRowsToMap(rows, imageResponseColumns)
		if err != nil {
			return nil, err
		}

		return result, nil
	}
	return nil, errors.New("No db")
}

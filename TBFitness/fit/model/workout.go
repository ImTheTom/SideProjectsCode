package model

import (
	"fit/database"
	"fmt"
)

// WorkoutColumns ...
var WorkoutColumns = []string{
	"workout_id",
	"workout_user_id",
	"workout_name",
}

// Workout ...
type Workout struct {
	ID     int    `json:"id"`
	UserID int    `json:"user_id"`
	Name   string `json:"name"`
}

type WorkoutData struct {
	ID     int    `json:"id"`
	UserID int    `json:"user_id"`
	Name   string `json:"name"`
	Lifts  []Lift `json:"lifts"`
}

// ToMap ...
func (w *Workout) ToMap() map[string]interface{} {
	m := map[string]interface{}{
		"workout_id":      w.ID,
		"workout_user_id": w.UserID,
		"workout_name":    w.Name,
	}
	return m
}

// InsertNew ...
func (w *Workout) InsertNew() (int64, error) {
	m := w.ToMap()
	return database.InsertMap(m, "workout", WorkoutColumns[0], false)
}

// Update ...
func (w *Workout) Update() error {
	m := w.ToMap()
	return database.UpdateMap(m, "workout", WorkoutColumns[0])
}

// RetrieveWorkouts ...
func RetrieveWorkouts(userID int) ([]Workout, error) {
	var result []Workout
	db := database.GetDataStore()
	query := `SELECT * FROM workout WHERE workout_user_id = ? ORDER BY lift_id;`
	params := []interface{}{userID}
	rows, err := db.Query(query, params)
	if err != nil {
		fmt.Println(err.Error())
		return result, err
	}

	var workout_id int
	var workout_user_id int
	var workout_name string

	defer rows.Close()
	for rows.Next() {
		err = rows.Scan(&workout_id, &workout_user_id, &workout_name)
		if err != nil {
			return result, err
		}
		item := Workout{
			ID:     workout_id,
			UserID: workout_user_id,
			Name:   workout_name,
		}
		result = append(result, item)
	}
	return result, nil
}

// RetrieveWorkout ...
func RetrieveWorkout(workoutID int) (*WorkoutData, error) {
	var result WorkoutData
	db := database.GetDataStore()
	query := `SELECT * FROM workout WHERE workout_id = ? ORDER BY lift_id;`
	params := []interface{}{workoutID}
	row := db.QueryRow(query, params)
	if row != nil {
		var workoutID int
		var workoutUserID int
		var workoutName string
		err := row.Scan(&workoutID, &workoutUserID, &workoutName)
		if err != nil {
			fmt.Println(err.Error())
			return &result, err
		}
		lifts, _ := RetrieveLiftsForWorkout(workoutID)
		result.ID = workoutID
		result.UserID = workoutUserID
		result.Name = workoutName
		result.Lifts = lifts
	}
	return &result, nil
}

// RemoveWorkout ...
func RemoveWorkout(workoutID int) error {
	query := `DELETE FROM workout WHERE workout_id = ?;`
	params := []interface{}{workoutID}

	_, err := database.GetDataStore().Exec(query, params)
	if err != nil {
		return err
	}

	return nil
}

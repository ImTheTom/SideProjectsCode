package model

import (
	"fit/database"
	"fmt"
)

// WorkoutEdits ...
type WorkoutEdits struct {
	WorkoutData WorkoutData `json:"current"`
	Available   []Lift      `json:"available"`
}

// RetrieveEditWorkouts ...
func RetrieveEditWorkouts(id int) (*WorkoutEdits, error) {
	var we WorkoutEdits
	wd, err := RetrieveWorkout(id)
	if err != nil {
		return &we, err
	}
	we.WorkoutData = *wd
	a, err := RetrieveLiftsNotInWorkout(id)
	if err != nil {
		return &we, err
	}
	we.Available = a
	return &we, nil
}

// RetrieveLiftsNotInWorkout ...
func RetrieveLiftsNotInWorkout(id int) ([]Lift, error) {
	var result []Lift
	db := database.GetDataStore()
	query := `select * from lift where lift_id not in (select lift_id from lifts where workout_id = ?) ORDER BY lift_id;`
	params := []interface{}{id}
	rows, err := db.Query(query, params)
	if err != nil {
		fmt.Println(err.Error())
		return result, err
	}

	var lift_id int
	var lift_user_id int
	var lift_name string
	var lift_weight float32
	var lift_reps int
	var lift_sets int

	defer rows.Close()
	for rows.Next() {
		err = rows.Scan(&lift_id, &lift_user_id, &lift_name, &lift_weight, &lift_reps, &lift_sets)
		if err != nil {
			return result, err
		}
		item := Lift{
			ID:     lift_id,
			UserID: lift_user_id,
			Name:   lift_name,
			Weight: lift_weight,
			Reps:   lift_reps,
			Sets:   lift_sets,
		}
		result = append(result, item)
	}
	return result, nil
}

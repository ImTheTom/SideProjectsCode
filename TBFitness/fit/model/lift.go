package model

import (
	"fit/database"
	"fmt"
)

// LiftColumns ...
var LiftColumns = []string{
	"lift_id",
	"lift_user_id",
	"lift_name",
	"lift_weight",
	"lift_reps",
	"lift_sets",
}

// Lift ...
type Lift struct {
	ID     int     `json:"id"`
	UserID int     `json:"user_id"`
	Name   string  `json:"name"`
	Weight float32 `json:"weight"`
	Reps   int     `json:"reps"`
	Sets   int     `json:"sets"`
}

// ToMap ...
func (l *Lift) ToMap() map[string]interface{} {
	m := map[string]interface{}{
		"lift_id":      l.ID,
		"lift_user_id": l.UserID,
		"lift_name":    l.Name,
		"lift_weight":  l.Weight,
		"lift_reps":    l.Reps,
		"lift_sets":    l.Sets,
	}
	return m
}

// InsertNew ...
func (l *Lift) InsertNew() (int64, error) {
	m := l.ToMap()
	return database.InsertMap(m, "lift", LiftColumns[0], false)
}

// Update ...
func (l *Lift) Update() error {
	m := l.ToMap()
	return database.UpdateMap(m, "lift", LiftColumns[0])
}

// RetrieveLiftsForUser ...
func RetrieveLiftsForUser(userID int) ([]Lift, error) {
	var result []Lift
	db := database.GetDataStore()
	query := `SELECT * FROM lift WHERE lift_user_id = ? ORDER BY lift_id;`
	params := []interface{}{userID}
	rows, err := db.Query(query, params)
	if err != nil {
		fmt.Println(err.Error())
		return result, err
	}

	var id int
	var userdbID int
	var name string
	var weight float32
	var reps int
	var sets int

	defer rows.Close()
	for rows.Next() {
		err = rows.Scan(&id, &userdbID, &name, &weight, &reps, &sets)
		if err != nil {
			return result, err
		}
		item := Lift{
			ID:     id,
			UserID: userID,
			Name:   name,
			Weight: weight,
			Reps:   reps,
			Sets:   sets,
		}
		result = append(result, item)
	}
	return result, nil
}

// RetrieveLiftsForWorkout ...
func RetrieveLiftsForWorkout(lifts_id int) ([]Lift, error) {
	var result []Lift
	db := database.GetDataStore()
	query := `select l.lift_id, l.lift_user_id, l.lift_name, l.lift_weight, l.lift_reps, l.lift_sets from lift l, lifts l2 where l2.lift_id = l.lift_id and workout_id = ? ORDER BY lift_id;`
	params := []interface{}{lifts_id}
	rows, err := db.Query(query, params)

	var lift_id int
	var lift_user_id int
	var lift_name string
	var lift_weight float32
	var lift_reps int
	var lift_sets int

	if err != nil {
		fmt.Println(err.Error())
	}
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

// RemoveLift ...
func RemoveLift(lift_id int) error {
	query := `DELETE FROM lift WHERE lift_id = ?;`
	params := []interface{}{lift_id}

	_, err := database.GetDataStore().Exec(query, params)
	if err != nil {
		return err
	}

	return nil
}

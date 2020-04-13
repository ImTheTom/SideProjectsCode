package model

import "fit/database"

import "fmt"

// LiftsColumns ...
var LiftsColumns = []string{
	"lift_id",
	"workout_id",
}

// Lifts ...
type Lifts struct {
	LiftID    int `json:"lift_id"`
	WorkoutID int `json:"workout_id"`
}

// ToMap ...
func (l *Lifts) ToMap() map[string]interface{} {
	m := map[string]interface{}{
		"lift_id":    l.LiftID,
		"workout_id": l.WorkoutID,
	}
	return m
}

// InsertNew ...
func (l *Lifts) InsertNew() (int64, error) {
	m := l.ToMap()
	return database.InsertMap(m, "lifts", LiftsColumns[0], true)
}

// Update ...
func (l *Lifts) Update() error {
	m := l.ToMap()
	return database.UpdateMap(m, "lifts", LiftsColumns[0])
}

// Remove ...
func (l *Lifts) Remove() error {
	query := `DELETE FROM lifts WHERE lift_id = ? AND workout_id = ?;`
	params := []interface{}{l.LiftID, l.WorkoutID}

	_, err := database.GetDataStore().Exec(query, params)
	if err != nil {
		return err
	}

	return nil
}

// InsertArrayOfLifts ...
func InsertArrayOfLifts(lifts []Lifts) (int64, error) {
	query := `INSERT INTO lifts(lift_id, workout_id) VALUES`

	for _, lift := range lifts {
		query += fmt.Sprintf(`(%d, %d),`, lift.LiftID, lift.WorkoutID)
	}

	query = query[:len(query)-1]
	query = query + ";"

	result, err := database.GetDataStore().Exec(query, nil)
	if err != nil {
		return 0, err
	}

	return result.LastInsertId()
}

// RemoveArrayOfLifts ...
func RemoveArrayOfLifts(lifts []Lifts) error {
	query := `DELETE FROM lifts WHERE `

	for _, lift := range lifts {
		query += fmt.Sprintf(`lift_id = %d AND workout_id = %d OR `, lift.LiftID, lift.WorkoutID)
	}

	query = query[:len(query)-4]
	query = query + ";"

	_, err := database.GetDataStore().Exec(query, nil)
	if err != nil {
		return err
	}

	return nil
}

package controllers

import (
	"fit/database"
	"fmt"
	"net/http"
	"net/http/httptest"
	"strconv"
	"strings"
	"testing"
)

func TestGetWorkoutID(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("GET", "/workout/1", nil)
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected := `{"id":1,"user_id":1,"name":"Push Day","lifts":[{"id":1,"user_id":1,"name":"Bench Press","weight":15,"reps":5,"sets":4},{"id":2,"user_id":1,"name":"OverHead Press","weight":22.5,"reps":10,"sets":3}]}
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

func TestGetWorkoutsID(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("GET", "/workouts/1", nil)
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected := `[{"id":1,"user_id":1,"name":"Push Day"},{"id":2,"user_id":1,"name":"Pull Day"}]
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

func TestCreateAndUpdateAndDeleteWorkouts(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("POST", "/workout", strings.NewReader(`{
		"id": 0,
		"user_id": 1,
		"name": "Leg day"
	}`))
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	response := strings.TrimSuffix(rr.Body.String(), "\n")

	createdWorkout, err := strconv.Atoi(response)
	if err != nil {
		t.Errorf("Had an error failing test")
	}

	if createdWorkout == 0 {
		t.Errorf("Could not create workout")
	}

	req, err = http.NewRequest("PUT", "/workout", strings.NewReader(fmt.Sprintf(`{
		"id": %d,
		"user_id": 1,
		"name": "Not Leg day"
	}`, createdWorkout)))
	if err != nil {
		t.Fatal(err)
	}
	rr = httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected := `"OK"
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}

	url := fmt.Sprintf("/workout/%d", createdWorkout)
	req, err = http.NewRequest("DELETE", url, nil)
	if err != nil {
		t.Fatal(err)
	}
	rr = httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected = `"Ok"
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

package controllers

import (
	"fit/database"
	"net/http"
	"net/http/httptest"
	"testing"
)

func TestGetWorkoutEditID(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("GET", "/workoutedit/1", nil)
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected := `{"current":{"id":1,"user_id":1,"name":"Push Day","lifts":[{"id":1,"user_id":1,"name":"Bench Press","weight":15,"reps":5,"sets":4},{"id":2,"user_id":1,"name":"OverHead Press","weight":22.5,"reps":10,"sets":3}]},"available":[{"id":3,"user_id":1,"name":"Dumbell Incline","weight":15,"reps":10,"sets":10},{"id":4,"user_id":1,"name":"Tricep Pulldown","weight":18,"reps":10,"sets":3}]}
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

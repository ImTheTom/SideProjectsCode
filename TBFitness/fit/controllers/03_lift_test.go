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

func TestGetLiftID(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("GET", "/lift/1", nil)
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

	r.ServeHTTP(rr, req)
	if status := rr.Code; status != http.StatusOK {
		t.Errorf("handler returned wrong status code: got %v want %v",
			status, http.StatusOK)
	}

	expected := `[{"id":1,"user_id":1,"name":"Bench Press","weight":15,"reps":5,"sets":4},{"id":2,"user_id":1,"name":"OverHead Press","weight":22.5,"reps":10,"sets":3},{"id":3,"user_id":1,"name":"Dumbell Incline","weight":15,"reps":10,"sets":10},{"id":4,"user_id":1,"name":"Tricep Pulldown","weight":18,"reps":10,"sets":3}]
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

func TestCreateAndUpdateAndDeleteLift(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("POST", "/lift", strings.NewReader(`{
		"id": 0,
		"user_id": 1,
		"name": "Bicep curl",
		"weight": 15,
		"reps": 10,
		"sets": 5
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

	createdLift, err := strconv.Atoi(response)
	if err != nil {
		t.Errorf("Had an error failing test")
	}

	if createdLift == 0 {
		t.Errorf("Could not create lift")
	}

	req, err = http.NewRequest("PUT", "/lift", strings.NewReader(fmt.Sprintf(`{
		"id": %d,
		"user_id": 1,
		"name": "Bicep curl",
		"weight": 16,
		"reps": 10,
		"sets": 5
	}`, createdLift)))
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

	url := fmt.Sprintf("/lift/%d", createdLift)
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

	expected = `"OK"
`

	if rr.Body.String() != expected {
		t.Errorf("handler returned unexpected body: got %v want %v",
			rr.Body.String(), expected)
	}
}

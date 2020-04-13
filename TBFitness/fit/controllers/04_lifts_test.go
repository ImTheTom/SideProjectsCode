package controllers

import (
	"fit/database"
	"net/http"
	"net/http/httptest"
	"strconv"
	"strings"
	"testing"
)

func TestCreateAndUpdateAndDeleteLifts(t *testing.T) {
	database.NewMySQLDataStore("user:password@tcp(0.0.0.0:3306)/fit")

	r := CreateRouter()

	req, err := http.NewRequest("DELETE", "/lifts", strings.NewReader(`[{
		"lift_id": 1,
		"workout_id": 1
	}]`))
	if err != nil {
		t.Fatal(err)
	}
	rr := httptest.NewRecorder()

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

	req, err = http.NewRequest("POST", "/lifts", strings.NewReader(`[{
		"lift_id": 1,
		"workout_id": 1
	}]`))
	if err != nil {
		t.Fatal(err)
	}
	rr = httptest.NewRecorder()

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

	if createdLift == 1 {
		t.Errorf("Could not create lifts")
	}
}

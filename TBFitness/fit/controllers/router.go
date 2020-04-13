package controllers

import "github.com/gorilla/mux"

func CreateRouter() *mux.Router {
	r := mux.NewRouter()

	r.HandleFunc("/", Healthz)
	// Workout
	r.HandleFunc("/workout/{id}", GetWorkout).Methods("GET")
	r.HandleFunc("/workouts/{id}", GetWorkouts).Methods("GET")
	r.HandleFunc("/workout", CreateWorkout).Methods("POST")
	r.HandleFunc("/workout", UpdateWorkout).Methods("PUT")
	r.HandleFunc("/workout/{id}", DeleteWorkout).Methods("DELETE")
	// Lift
	r.HandleFunc("/lift/{id}", GetLift).Methods("GET")
	r.HandleFunc("/lift/{id}", DeleteLift).Methods("DELETE")
	r.HandleFunc("/lift", CreateLift).Methods("POST")
	r.HandleFunc("/lift", UpdateLift).Methods("PUT")
	// Lifts
	r.HandleFunc("/lifts", AddLifts).Methods("POST")
	r.HandleFunc("/lifts", RemoveLifts).Methods("DELETE")
	// Edit Workouts
	r.HandleFunc("/workoutedit/{id}", GetWorkoutEdit).Methods("GET")
	r.HandleFunc("/workoutedit", CreateLiftInWorkout).Methods("POST")
	r.HandleFunc("/workoutedit", DeleteLiftFromWorkout).Methods("DELETE")

	return r
}

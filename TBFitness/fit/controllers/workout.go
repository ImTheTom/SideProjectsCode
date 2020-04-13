package controllers

import (
	"encoding/json"
	"fit/model"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

func GetWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	workout, err := model.RetrieveWorkout(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, workout)
	}
}

func GetWorkouts(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	workout, err := model.RetrieveWorkouts(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, workout)
	}
}

func GetWorkoutEdit(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	workout, err := model.RetrieveEditWorkouts(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, workout)
	}
}

func CreateLiftInWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lifts model.Lifts
	err := decoder.Decode(&lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	id, err := lifts.InsertNew()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, id)
	}
}

func DeleteLiftFromWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lifts model.Lifts
	err := decoder.Decode(&lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	err = lifts.Remove()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "Ok")
	}
}

func CreateWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var workout model.Workout
	err := decoder.Decode(&workout)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	id, err := workout.InsertNew()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, id)
	}
}

func UpdateWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var workout model.Workout
	err := decoder.Decode(&workout)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	err = workout.Update()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "OK")
	}
}

func DeleteWorkout(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	err = model.RemoveWorkout(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "Ok")
	}
}

package controllers

import (
	"encoding/json"
	"fit/model"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

func GetLift(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	lifts, err := model.RetrieveLiftsForUser(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, lifts)
	}
}

func CreateLift(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lift model.Lift
	err := decoder.Decode(&lift)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	id, err := lift.InsertNew()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, id)
	}
}

func UpdateLift(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lift model.Lift
	err := decoder.Decode(&lift)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	err = lift.Update()
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "OK")
	}
}

func DeleteLift(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	params := mux.Vars(r)
	id := params["id"]
	idInt, err := strconv.Atoi(id)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, "ID Error")
		return
	}
	err = model.RemoveLift(idInt)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "OK")
	}
}

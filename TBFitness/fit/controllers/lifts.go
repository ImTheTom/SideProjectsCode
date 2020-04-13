package controllers

import (
	"encoding/json"
	"fit/model"
	"net/http"
)

func AddLifts(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lifts []model.Lifts
	err := decoder.Decode(&lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	id, err := model.InsertArrayOfLifts(lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, id)
	}
}

func RemoveLifts(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json; charset=UTF-8")
	decoder := json.NewDecoder(r.Body)
	var lifts []model.Lifts
	err := decoder.Decode(&lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	}
	err = model.RemoveArrayOfLifts(lifts)
	if err != nil {
		writeErrorResponse(w, http.StatusBadRequest, err.Error())
	} else {
		writeOKResponse(w, "OK")
	}
}

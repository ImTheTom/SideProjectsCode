package main

import (
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"net/http"

	token "../token"
)

// Index ...
func Index(w http.ResponseWriter, r *http.Request) {
	fmt.Fprint(w, "JWT Golang server!\n")
}

//curl -H "Content-Type: application/json" -d '{"id":1}' http://localhost:8080/encode

// EncodeRequest ...
func EncodeRequest(w http.ResponseWriter, r *http.Request) {
	var decodedJWT DecodedJWT
	body, err := ioutil.ReadAll(io.LimitReader(r.Body, 1048576))

	if err != nil {
		panic(err)
	}

	if err := r.Body.Close(); err != nil {
		panic(err)
	}

	if err := json.Unmarshal(body, &decodedJWT); err != nil {
		w.Header().Set("Content-Type", "application/json; charset=UTF-8")
		w.WriteHeader(422) // unprocessable entity
		if err := json.NewEncoder(w).Encode(err); err != nil {
			panic(err)
		}
	}

	jwt, _ := token.Encode(decodedJWT.ID)
	var encodedJWT EncodedJWT
	encodedJWT.JWT = jwt

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusCreated)
	json.NewEncoder(w).Encode(encodedJWT)
}

// DecodeRequest ...
func DecodeRequest(w http.ResponseWriter, r *http.Request) {
	var encodedJWT EncodedJWT
	body, err := ioutil.ReadAll(io.LimitReader(r.Body, 1048576))

	if err != nil {
		panic(err)
	}

	if err := r.Body.Close(); err != nil {
		panic(err)
	}

	if err := json.Unmarshal(body, &encodedJWT); err != nil {
		w.Header().Set("Content-Type", "application/json; charset=UTF-8")
		w.WriteHeader(422) // unprocessable entity
		if err := json.NewEncoder(w).Encode(err); err != nil {
			panic(err)
		}
	}

	id, _ := token.Decode(encodedJWT.JWT)
	var decodedJWT DecodedJWT
	decodedJWT.ID = id

	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusCreated)
	json.NewEncoder(w).Encode(decodedJWT)
}

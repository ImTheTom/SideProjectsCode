package main

import "net/http"

// Route ...
type Route struct {
	Name        string
	Method      string
	Pattern     string
	HandlerFunc http.HandlerFunc
}

// Routes ...
type Routes []Route

var routes = Routes{
	Route{
		"Index",
		"GET",
		"/",
		Index,
	},
	Route{
		"EncodeRequest",
		"POST",
		"/encode",
		EncodeRequest,
	},
	Route{
		"DecodeRequest",
		"POST",
		"/decode",
		DecodeRequest,
	},
}

package controllers

import (
	"github.com/julienschmidt/httprouter"
)

// NewRouter used to create a router
func NewRouter(routes Routes) *httprouter.Router {

	router := httprouter.New()
	for _, route := range routes {
		var handle httprouter.Handle

		handle = route.HandlerFunc

		router.Handle(route.Method, route.Path, handle)
	}

	return router
}

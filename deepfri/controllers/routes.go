package controllers

import "github.com/julienschmidt/httprouter"

// Route structure for route and their information
type Route struct {
	Name        string
	Method      string
	Path        string
	HandlerFunc httprouter.Handle
}

// Routes array of Route
type Routes []Route

// AllRoutes function used to get all routes and their information
func AllRoutes() Routes {
	routes := Routes{
		Route{"Image Get", "GET", "/image/:id", getImage},
		Route{"Image Post", "POST", "/image", saveImage},
		Route{"Image Filter", "GET", "/filter/:id", filterImage},
		Route{"Image Recent", "GET", "/recent/:amount", getRecentImages},
	}
	return routes
}

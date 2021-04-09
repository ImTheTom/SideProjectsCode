package util

import (
	"math/rand"
	"strconv"
)

// CreateRandomPath ...
func CreateRandomPath(identifier string) string {
	return "images/" + identifier + "-" + strconv.Itoa(rand.Int()) + ".png"
}

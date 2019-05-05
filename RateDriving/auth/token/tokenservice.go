package tok

import (
	"fmt"
	"io/ioutil"

	"github.com/dgrijalva/jwt-go"
)

var key, _ = ioutil.ReadFile("key.txt") // just pass the file name

// Decode ...
func Decode(tokenString string) (id float64, err error) {
	token, err := jwt.Parse(tokenString, func(token *jwt.Token) (interface{}, error) {

		if _, ok := token.Method.(*jwt.SigningMethodHMAC); !ok {
			return nil, fmt.Errorf("Unexpected signing method: %v", token.Header["alg"])
		}

		return key, nil
	})

	if claims, ok := token.Claims.(jwt.MapClaims); ok && token.Valid {
		return claims["id"].(float64), nil
	}
	return 0, err
}

// Encode ...
func Encode(userid float64) (tokenString string, err error) {
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, jwt.MapClaims{
		"id": userid,
	})

	tokenString, err = token.SignedString(key)
	return tokenString, err
}

package main	

import (
	"fmt"
	"crypto/sha1"
	"os"
	"time"
)

var match string = "test"

var found = false

func search(size int, hash, alphabet, current []byte){
	if(found){
		return
	}
	if(size<=0){
		h := sha1.New()
		
		h.Write([]byte(string(current)))
		
		bytes := h.Sum(nil)

		if(equal(hash,bytes)){
			fmt.Printf("Found: %s. Hashed it is: %x\n", string(current), bytes)
			found = true
		}

	}else{
		for i:=0; i<len(alphabet); i++{
			current[size-1]=alphabet[i];
			search(size-1,hash,alphabet,current)
		}
	}
}

func generateAlphabet(alphabet []byte){
	for a:=0; a<10; a++{
		alphabet[a] = byte(a+48)
	}
	for a:=10; a<26+10; a++{
		alphabet[a] = byte(a+55)
	}
	for a:=36; a<62; a++{
		alphabet[a] = byte(a+61)
	}
}

func equal(a, b []byte) bool {
    if len(a) != len(b) {
        return false
    }
    for i, v := range a {
        if v != b[i] {
            return false
        }
    }
    return true
}

func main() {

	match = os.Args[1]

	h := sha1.New()

	h.Write([]byte(match))

	bytes := h.Sum(nil)

	fmt.Printf("Searching for: %s. Hashed it is: %x\n", match, bytes)

	alphabet := make([]byte, 62)

	generateAlphabet(alphabet)

	now := time.Now()

	/*for i:=1; i<15; i++{
		if(!found){
			var currentArray = make([]byte, i)
			go search(i, bs, alphabet, currentArray)
		}
	}
	
	fmt.Println("Enter to stop searching or wait for a response.")

	fmt.Scanln()
	*/

	for i:=1; i<15; i++{
		if(!found){
			var currentArray = make([]byte, i)
			search(i, bytes, alphabet, currentArray)
		}
	}

	diff := time.Now().Sub(now)

	fmt.Printf("Found in: %s\n", diff)

	fmt.Println("Ending...")
}

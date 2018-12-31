package main

import(
	"fmt"
	"io/ioutil"
	"net/http"
	"mvdan.cc/xurls"
	"strings"
	"regexp"
	"time"
	"database/sql"
	_ "github.com/go-sql-driver/mysql"
	"math/rand"
	"os"
	ftp "github.com/jlaffaye/ftp"
	"bytes"
)

var ftpUrl string = "URL" //URL to ftp server

var ftpUser string  = "username" //Log in to FTP server

var ftpPass string = "password" //Password to FTP server

var values [16] int //Current Values stored

var upUrls [100] string //Upcoming URLS

var prUrls [100] string //Previous URLS

var sites = [11]string{"https://www.youtube.com",
				   "https://github.com/",
				   "https://www.google.com",
				   "https://twitter.com/",
				   "https://www.twitch.tv/",
				   "https://www.reddit.com/",
				   "https://www.wikipedia.org/",
				   "https://au.yahoo.com/",
				   "https://www.amazon.com",
				   "https://www.abc.net.au/",
				   "https://www.dailymail.co.uk/auhome/index.html"} //Possible starting points

func selectNewUrl() string {
	rand.Seed(time.Now().Unix())
					
	return sites[rand.Intn(len(sites))]
}

func getContent(url string) string{
	fmt.Printf("Looking Up %s ...\n", url)

	if url[:4] != "http" {
		return "fail"
	}

	resp, err := http.Get(url) //Load the http site

	if resp == nil {
		return "fail"
	}
	
	if err != nil && resp.StatusCode != 200 {
		return "fail"
	}

	defer resp.Body.Close()

	html, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		return "fail"
	}

	return string(html)
}

func removeHead(content string) string{
	r, _ := regexp.Compile("body")
	locations:=r.FindStringIndex(content)

	if len(locations) == 0 {
		return "fail"
	}

	return content[locations[0]:]
}

func findURLS(content string) []string{
	return xurls.Strict().FindAllString(content, -1)
}

func removeURLS(content string) string{
	r := regexp.MustCompile("<a ?href.*</a>")
	return r.ReplaceAllString(content, "<URL>")
}

func removeCommas(content string) string{
	return strings.Replace(content, ",", "", -1)
}

func findNumbers(content string) []string{
	r := regexp.MustCompile("[0-9]+")
	return r.FindAllString(content, -1)
}

func updateValues(numbers [] string){
	for _, number := range numbers{
		i := len(number)-1 //If length is 1 then it goes in the 0 index
		if i >= 16 {
			i = 15
		}
		values[i] = values[i] + 1
	}
}

func updateURLS(url string, urls [] string){
	pUrl := false

	for _,v := range urls {
		for i:=0; i<len(prUrls); i++ { //Check if previously went there
			if prUrls[i] == v{
				pUrl = true
				break
			}
		}

		for i:=0; i<len(upUrls); i++ { //Check if new url should go into upcoming urls
			if pUrl {
				break //Break out if pUrl was set to true
			}else if upUrls[i]=="" {
				upUrls[i] = v
				break
			}else if upUrls[i]==v {
				break
			}else if upUrls[i]==url {
				break
			}
		}
		pUrl = false
	}

}

func getFirstUrl() string{
	next := upUrls[0]
	shiftUpUrlsToLeft()

	if(next==""){
		next = selectNewUrl()
	}

	return next
}

func shiftUpUrlsToLeft() {
	for i:=0; i<len(upUrls)-1; i++ {
		upUrls[i] = upUrls[i+1]
	}

	upUrls[len(upUrls)-1] = ""
}

func updatePreviousUrls(newUrl string){
	for i:=len(prUrls)-1; i>0; i-- {
		prUrls[i] = prUrls[i-1]
	}

	prUrls[0] = newUrl
}

func resetUrls() {
	for i:=0; i<len(upUrls); i++ {
		upUrls[i] = ""
		prUrls[i] = ""
	}
}

func updateDatabase(){
	file, err := os.Create("count.txt")

    if err != nil {
		fmt.Println("Could not create file")
		return
	}
	
	defer file.Close()
	
	db, err := sql.Open("mysql", "root:test@/numbers")
	if err != nil {
		fmt.Println(err)
		return
	}

	var databaseValues [16] int
	index := 0

	rows, err := db.Query("SELECT * FROM count")
	if err != nil {
		fmt.Println(err)
		return
	}

	for rows.Next() {
		var id int
		var amount int

		err = rows.Scan(&id, &amount)
		if err != nil {
			fmt.Println(err)
			return
		}

		databaseValues[index] = amount
		index = index + 1
	}

	stmt, err := db.Prepare("update count set amount=? where id=?")
	if err != nil {
		fmt.Println(err)
		return
	}

	for i:=0; i<len(databaseValues); i++ {
		databaseValues[i] = values[i] + databaseValues[i]

		fmt.Fprintf(file, "%d\n",databaseValues[i])

		id := i+1
		_, err = stmt.Exec(databaseValues[i], id)
		if err != nil {
			fmt.Println(err)
			return
		}
		values[i] = 0
	}

	fmt.Println("Updated Values")

	uploadFile()
}

func uploadFile() {
	fmt.Println("Connecting to ftp...")

	c, err := ftp.Dial(ftpUrl)
	if err != nil {
		fmt.Println(err)
		return
	}

	c.Login(ftpUser, ftpPass)

	defer c.Quit()

	s,_:=ioutil.ReadFile("count.txt")
	d := string(s)
	data := bytes.NewBufferString(d)

	err = c.Stor("public_html/count.txt",data) //Store the buffered string to the database
	if err != nil {
		fmt.Println(err)
		return
	}

	fmt.Println("Uploaded file")
}

func main(){

	url:=selectNewUrl()

	count := 0

	count2 := 0

	for {

		text := getContent(url)

		if(text != "fail"){

			text = removeHead(text)

			if(text != "fail") {

				urls := findURLS(text)

				updatePreviousUrls(url)

				updateURLS(url, urls)

				text = removeURLS(text)

				text = removeCommas(text)

				numberStrings := findNumbers(text)

				updateValues(numberStrings)

			}

		}

		count = count + 1

		count2 = count2 + 1

		if count > 30 {
			updateDatabase()
			count = 0
		}

		if count2 > 100 {
			resetUrls()
			count2 = 0
		}

		url = getFirstUrl()

		fmt.Println(values)

		fmt.Println("Waiting sixty seconds")

		time.Sleep(60*time.Second)

	}
	
}
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

var ftpUrl string = "serverurl"

var ftpUser string  = "username"

var ftpPass string = "password"

var values [16] int

var upcomingUrls [500] string

var previousUrls [100] string

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
				   "https://www.dailymail.co.uk/auhome/index.html"}

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
	err = c.Stor("public_html/count.txt",data)
	if err != nil {
		fmt.Println(err)
		return
	}
	fmt.Println("Uploaded file")
}

func getContent(url string) string{
	fmt.Printf("Looking Up %s ...\n", url)

	if url[:4] != "http" {
		return "fail"
	}

	resp, err := http.Get(url)

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
	content = content[locations[0]:]
	return content
}

func removeURLS(content string) string{
	r := regexp.MustCompile("<a ?href.*</a>")
	return r.ReplaceAllString(content, "<URL>")
}

func removeCommas(content string) string{
	text := strings.Replace(content, ",", "", -1)
	return text
}

func findNumbers(content string) []string{
	re := regexp.MustCompile("[0-9]+")
	return re.FindAllString(content, -1)
}

func updateValues(numbers [] string){
	for _, number := range numbers{
		index := len(number)-1
		if index >= 16 {
			index = 15
		}
		values[index] = values[index] + 1
	}
}

func updateURLS(url string, urls [] string){
	previousFound := false
	for _,v := range urls {
		for i:=0; i<len(previousUrls); i++ {
			if previousUrls[i] == v{
				previousFound = true
				break
			}
		}
		for i:=0; i<len(upcomingUrls); i++ {
			if previousFound {
				break
			}
			if upcomingUrls[i]=="" {
				upcomingUrls[i] = v
				break
			}else if upcomingUrls[i]==v {
				break
			}else if upcomingUrls[i]==url {
				break
			}
		}
		previousFound = false
	}
}

func getFirstUrl() string{
	next := upcomingUrls[0]
	shiftUrlsToLeft()
	if(next==""){
		next = selectNewUrl()
	}
	return next
}

func shiftUrlsToLeft() {
	for i:=0; i<len(upcomingUrls)-1; i++ {
		upcomingUrls[i] = upcomingUrls[i+1]
	}
	upcomingUrls[499] = ""
}

func updatePreviousUrls(newUrl string){
	for i:=len(previousUrls)-1; i>0; i-- {
		previousUrls[i] = previousUrls[i-1]
	}
	previousUrls[0] = newUrl
}

func resetUrls() {
	for i:=0; i<len(upcomingUrls); i++ {
		upcomingUrls[i] = ""
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

func selectNewUrl() string {
	rand.Seed(time.Now().Unix()) // initialize global pseudo random generator
	
	return sites[rand.Intn(len(sites))]
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

				urls := xurls.Strict().FindAllString(text, -1)

				text = removeURLS(text)

				text = removeCommas(text)

				numberStrings := findNumbers(text)

				updateValues(numberStrings)

				fmt.Println(values)

				updatePreviousUrls(url)

				updateURLS(url, urls)

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

		fmt.Println("Waiting sixty seconds")

		time.Sleep(60*time.Second)

	}
	
}
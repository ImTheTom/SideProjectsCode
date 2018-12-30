# Web Scraper
This repository is a for a web scraper I created. It searches for numbers on the internet and records their frequency of the size.
        For example if it found a site with the numbers: 2, 5, 100. Then the 1 size frequency would increase by 2 and the 100 would
        increase by 1.
        The web scraper works by loading a page. Scanning the body for links and numbers. It stores the numbers and waits 60 seconds
        before loading another page that was found on a previous page. After 30 rotations it updates the MySQL server by adding onto
        the values in the database. It then creates a text file from the database and uploads it to my server via a FTP protocol.
        The text file is then used by this php page to create the values. After 100 rotations it scraps all the upcoming URLS stored
        and selects a new random starting point.
        The page created can be found [Here](https://tombowyer.com/internetnumbers)
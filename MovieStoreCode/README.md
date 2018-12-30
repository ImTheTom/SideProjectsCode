# Movie Store Databse

This repository is for the gui based movie store application that interacts with the MySQL database.

The database itself is pretty basic. As seen in the ER diagram image file in the repostiroy. The movie table is 1NF since you would expect a movie store toy have more than one copy of a new release. Having a table with unique codes of the copies and what movie ID it releates to would allow for a higher normal form. 

The gui allows for:
* A connection to the databse
* Renting a movie
* Returning a movie
* Searching the movies and seeing if they are rented or not
* Search the users table
* Add a new user to the table
* Add a new movie to the table
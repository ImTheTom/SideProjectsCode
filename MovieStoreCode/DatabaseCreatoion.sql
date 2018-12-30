CREATE DATABASE MovieStore;

USE MovieStore;

CREATE TABLE customers(
	customerID INT AUTO_INCREMENT PRIMARY KEY,
    firstName VARCHAR(30) NOT NULL,
    lastName VARCHAR(30) NOT NULL,
    DOB DATE NOT NULL,
    mailingAddress VARCHAR (255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    postcode CHAR(4) NOT NULL,
    overdue FLOAT(10,2) DEFAULT 0
);

CREATE TABLE movie(
	movieID INT AUTO_INCREMENT PRIMARY KEY,
    movieName VARCHAR(30) NOT NULL,
    quantityInStock int DEFAULT 0,
    grade ENUM('A','B','C','D') DEFAULT 'A',
    releaseYear CHAR(4) NOT NULL,
    runningTime int NOT NULL,
    genre ENUM('Horror', 'Action', 'Drama', 'Thriller', 'Fiction')
);

CREATE TABLE rentals(
	rentalID INT AUTO_INCREMENT PRIMARY KEY,
	movieID INT,
    customerID INT,
    dateRented DATE NOT NULL,
    returnByDate DATE NOT NULL,
    dateReturned Date,
    FOREIGN KEY (movieID) REFERENCES movie(movieID),
    FOREIGN KEY (customerID) REFERENCES customers(customerID)
);

    
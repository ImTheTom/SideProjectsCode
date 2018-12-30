-- MySQL dump 10.13  Distrib 5.7.20, for Win64 (x86_64)
--
-- Host: localhost    Database: moviestore
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customers` (
  `customerID` int(11) NOT NULL AUTO_INCREMENT,
  `firstName` varchar(30) NOT NULL,
  `lastName` varchar(30) NOT NULL,
  `DOB` date NOT NULL,
  `mailingAddress` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `postcode` char(4) NOT NULL,
  `overdue` float(10,2) DEFAULT '0.00',
  PRIMARY KEY (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (6,'Kevin','Jones','1980-12-29','23 Real road, Albion','KJones@gmail.com','4230',0.00),(7,'Tom','Smith','1998-04-15','1040 Logan Road, Logan','Retr0@hotmail.com','4630',0.00),(8,'Tiffany','Smith','1996-02-27','1040 Logan Road, Logan','TiffanyMarbles@gmail.com','4630',0.00),(9,'Mary','Lamb','1991-05-10','15 main street, Brisbane','sheeeeep@hotmail.com','4001',0.00),(10,'Bart','Simpson','2001-11-22','742 Evergreen Terrance, Springfield','SimpsonsFan23@hotmail.com','4201',0.00),(11,'James','Kingston','1990-02-10','123 Fake Street, Bunderberg','JamesssK@gmail.com','4210',0.00);
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movie`
--

DROP TABLE IF EXISTS `movie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movie` (
  `movieID` int(11) NOT NULL AUTO_INCREMENT,
  `movieName` varchar(30) NOT NULL,
  `grade` enum('A','B','C','D') DEFAULT 'A',
  `releaseYear` char(4) NOT NULL,
  `runningTime` int(11) NOT NULL,
  `genre` enum('Horror','Action','Drama','Thriller','Fiction') DEFAULT NULL,
  `isRented` varchar(5) DEFAULT 'No',
  PRIMARY KEY (`movieID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie`
--

LOCK TABLES `movie` WRITE;
/*!40000 ALTER TABLE `movie` DISABLE KEYS */;
INSERT INTO `movie` VALUES (6,'IT','A','2017',135,'Horror','no'),(7,'Inception','C','2010',148,'Thriller','yes'),(8,'Blade Runner 2049','A','2017',164,'Action','No'),(9,'The Godfather','D','1972',178,'Drama','No'),(10,'Deadpool','B','2016',108,'Action','No'),(13,'Logan','B','2017',141,'Action','No');
/*!40000 ALTER TABLE `movie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rentals`
--

DROP TABLE IF EXISTS `rentals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rentals` (
  `rentalID` int(11) NOT NULL AUTO_INCREMENT,
  `movieID` int(11) DEFAULT NULL,
  `customerID` int(11) DEFAULT NULL,
  `dateRented` date DEFAULT NULL,
  `returnByDate` date DEFAULT NULL,
  `dateReturned` date DEFAULT NULL,
  PRIMARY KEY (`rentalID`),
  KEY `movieID` (`movieID`),
  KEY `customerID` (`customerID`),
  CONSTRAINT `rentals_ibfk_1` FOREIGN KEY (`movieID`) REFERENCES `movie` (`movieID`),
  CONSTRAINT `rentals_ibfk_2` FOREIGN KEY (`customerID`) REFERENCES `customers` (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rentals`
--

LOCK TABLES `rentals` WRITE;
/*!40000 ALTER TABLE `rentals` DISABLE KEYS */;
INSERT INTO `rentals` VALUES (1,6,6,'2017-11-19','2017-11-20','2017-11-19'),(10,7,6,'2017-11-19','2017-11-23',NULL);
/*!40000 ALTER TABLE `rentals` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-19 18:22:21

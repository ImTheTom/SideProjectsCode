-- MySQL dump 10.13  Distrib 5.7.20, for Win64 (x86_64)
--
-- Host: localhost    Database: short
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
-- Table structure for table `urls`
--

DROP TABLE IF EXISTS `urls`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `urls` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `original` mediumtext,
  `short` char(6) DEFAULT NULL,
  `created` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `urls`
--

LOCK TABLES `urls` WRITE;
/*!40000 ALTER TABLE `urls` DISABLE KEYS */;
INSERT INTO `urls` VALUES (13,'http://test.com','abixkj','2019-01-02 19:00:02.000000'),(14,'http://twitch.tv','lYV3gR','2019-01-02 19:15:49.000000'),(15,'http://twitch.tv','kKXV2e','2019-01-02 19:30:19.000000'),(16,'http://test.com','okVAz5','2019-01-02 19:54:47.000000'),(17,'http://test.com','oJHRX0','2019-01-02 19:57:01.000000'),(18,'http://as','vhJf0s','2019-01-02 19:57:52.000000'),(19,'http://as','DsZRJG','2019-01-02 19:59:01.000000'),(20,'http://13reasonswhy','tgCOYf','2019-01-02 20:00:11.000000'),(21,'http://www.google.com','PH2rbu','2019-01-02 20:06:39.000000'),(22,'http://www.tombowyer.com','Q38Ks2','2019-01-03 18:16:09.000000'),(23,'https://www.youtube.com/','OyEIwG','2019-01-03 18:42:22.000000'),(24,'homelink','N3zYsT','2019-01-03 18:45:45.000000'),(25,'http://test.com','taqTqQ','2019-01-03 18:50:58.000000'),(26,'http://test','3NhbJy','2019-01-03 18:52:13.000000'),(27,'http://test.com','cMK2YZ','2019-01-03 18:56:47.000000'),(28,'http://www.tombowyer.com','OcjNOZ','2019-01-03 19:26:25.000000'),(29,'http://www.tombowyer.com','xGInle','2019-01-03 19:30:24.000000'),(30,'http://www.tombowyer.com','g51OGl','2019-01-03 19:36:30.000000'),(31,'http://test.com','FfwYvQ','2019-01-03 19:37:35.000000');
/*!40000 ALTER TABLE `urls` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-03 18:39:21

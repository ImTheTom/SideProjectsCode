-- MySQL dump 10.13  Distrib 5.7.20, for Win64 (x86_64)
--
-- Host: localhost    Database: quotes
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
-- Table structure for table `quotes`
--

DROP TABLE IF EXISTS `quotes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `quotes` (
  `idquotes` int(11) NOT NULL AUTO_INCREMENT,
  `quote` mediumtext NOT NULL,
  `author` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  PRIMARY KEY (`idquotes`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quotes`
--

LOCK TABLES `quotes` WRITE;
/*!40000 ALTER TABLE `quotes` DISABLE KEYS */;
INSERT INTO `quotes` VALUES (1,'You have to fight to reach your dream. You have to sacrifice and work hard for it.','Lionel Messi','Sport'),(2,'The best decisions aren’t made with your mind, but with your instinct.','Lionel Messi','Sport'),(3,'Money is not a motivating factor…My motivation comes from playing the game I love. If I wasn’t paid to be a professional footballer, I would willingly play for nothing.','Lionel Messi','Sport'),(4,'I start early and I stay late, day after day, year after year. It took me 17 years and 114 days to become an overnight success.','Lionel Messi','Sport'),(5,'Your love makes me strong. Your hate makes me unstoppable.','Cristiano Ronaldo','Sport'),(6,'But I don’t want to be compared to anyone – I’d like to impose my own style of play and do the best for myself and for the club here.','Cristiano Ronaldo','Sport'),(7,'I would be very proud if, one day, I’m held in the same esteem as George Best or Beckham. It’s what I’m working hard towards.','Cristiano Ronaldo','Sport'),(8,'After I joined, the manager asked me what number I’d like. I said 28. But Ferguson said ‘No, you’re going to have No. 7’ and the famous shirt was an extra source of motivation. I was forced to live up to such an honor.','Cristiano Ronaldo','Sport'),(9,'My fellow Americans, we are and always will be a nation of immigrants. We were strangers once, too.','Barack Obama','Politics'),(10,'If you\'re walking down the right path and you\'re willing to keep walking, eventually you\'ll make progress.','Barack Obama','Politics'),(11,'Change will not come if we wait for some other person or some other time. We are the ones we\'ve been waiting for. We are the change that we seek.','Barack Obama','Politics'),(12,'No dream is too big. No challenge is too great. Nothing we want for our future is beyond our reach.','Donald Trump','Politics'),(13,'We must speak our minds openly, debate our disagreements honestly, but always pursue solidarity.','Donald Trump','Politics'),(14,'Without passion you don\'t have energy, with out energy you have nothing.','Donald Trump','Politics');
/*!40000 ALTER TABLE `quotes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-07-02 17:12:09

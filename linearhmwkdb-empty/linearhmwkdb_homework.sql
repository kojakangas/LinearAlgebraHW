CREATE DATABASE  IF NOT EXISTS `linearhmwkdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `linearhmwkdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: linearhmwkdb
-- ------------------------------------------------------
-- Server version	5.6.14

SET GLOBAL event_scheduler = ON;

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
-- Table structure for table `homework`
--

DROP TABLE IF EXISTS `homework`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `homework` (
  `homeworkid` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `points` varchar(45) NOT NULL,
  `dueDate` datetime NOT NULL,
  `status` varchar(45) NOT NULL DEFAULT 'Assigned',
  PRIMARY KEY (`homeworkid`),
  UNIQUE KEY `homeworkid_UNIQUE` (`homeworkid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `homework`
--

LOCK TABLES `homework` WRITE;
/*!40000 ALTER TABLE `homework` DISABLE KEYS */;
/*!40000 ALTER TABLE `homework` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- UPDATES PAST DUE HOMEWORK AND HMWKASSIGNMENT

DROP EVENT IF EXISTS `update_homework`;

DELIMITER |

CREATE EVENT `update_homework` 
ON SCHEDULE EVERY 1 DAY STARTS CURDATE() + INTERVAL 24 HOUR
DO BEGIN
	UPDATE linearhmwkdb.homework SET status = "Complete" WHERE dueDate <= NOW();
	UPDATE linearhmwkdb.hmwkassignment hw SET status = "Late" WHERE (SELECT status from linearhmwkdb.homework where homeworkId = hw.homeworkId) = "Complete" && status != "Complete";

END|
DELIMITER ;

-- Dump completed on 2013-10-31 17:24:18

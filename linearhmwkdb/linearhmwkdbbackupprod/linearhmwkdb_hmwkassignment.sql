CREATE DATABASE  IF NOT EXISTS `linearhmwkdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `linearhmwkdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: mcs.drury.edu    Database: linearhmwkdb
-- ------------------------------------------------------
-- Server version	5.6.14-log

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
-- Table structure for table `hmwkassignment`
--

DROP TABLE IF EXISTS `hmwkassignment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hmwkassignment` (
  `assignmentId` bigint(20) NOT NULL AUTO_INCREMENT,
  `homeworkId` bigint(20) NOT NULL,
  `grade` decimal(10,2) NOT NULL DEFAULT '0.00',
  `status` varchar(45) NOT NULL DEFAULT 'Assigned',
  `currentQuestion` bigint(20) NOT NULL DEFAULT '1',
  `userId` bigint(20) NOT NULL,
  PRIMARY KEY (`assignmentId`),
  UNIQUE KEY `assignmentId_UNIQUE` (`assignmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hmwkassignment`
--

LOCK TABLES `hmwkassignment` WRITE;
/*!40000 ALTER TABLE `hmwkassignment` DISABLE KEYS */;
INSERT INTO `hmwkassignment` VALUES (1,1,1.00,'Complete',1,2),(2,2,0.00,'Late',1,2),(3,1,1.00,'Complete',1,3),(4,2,0.00,'Late',1,3),(5,1,0.15,'Complete',1,4),(6,2,0.49,'Late',2,4),(7,1,1.00,'Complete',1,5),(8,2,1.00,'Late',2,5),(9,1,1.00,'Complete',1,6),(10,2,3.46,'Complete',4,6),(11,1,1.00,'Complete',1,7),(12,2,2.48,'Late',4,7),(13,1,1.00,'Complete',1,8),(14,2,0.32,'Late',2,8),(15,1,0.00,'Late',1,9),(16,2,3.26,'Complete',4,9),(17,1,0.00,'Late',1,10),(18,2,3.54,'Complete',4,10),(19,1,0.00,'Late',1,11),(20,2,3.58,'Complete',4,11),(21,1,0.00,'Late',1,12),(22,2,2.52,'Complete',4,12),(23,1,0.32,'Complete',1,13),(24,2,3.28,'Complete',4,13),(25,1,1.00,'Complete',1,14),(26,2,3.56,'Complete',4,14),(27,1,0.49,'Complete',1,15),(28,2,0.44,'Late',2,15),(29,1,1.00,'Complete',1,16),(30,2,0.00,'Late',1,16),(31,1,0.00,'Late',1,17),(32,2,3.38,'Complete',4,17),(33,1,0.00,'Late',1,18),(34,2,2.46,'Complete',4,18),(35,1,0.00,'Late',1,19),(36,2,2.30,'Late',4,19),(37,1,0.00,'Late',1,20),(38,2,0.49,'Late',2,20),(39,1,1.00,'Complete',1,21),(40,2,2.15,'Late',4,21),(41,1,0.00,'Late',1,22),(42,2,1.86,'Late',3,22),(43,1,0.73,'Complete',1,23),(44,2,3.50,'Complete',4,23),(45,1,0.00,'Late',1,24),(46,2,2.40,'Late',4,24),(47,1,0.00,'Late',1,25),(48,2,2.37,'Complete',4,25),(49,1,0.60,'Complete',1,26),(50,2,1.00,'Late',2,26);
/*!40000 ALTER TABLE `hmwkassignment` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-02-25 14:39:35

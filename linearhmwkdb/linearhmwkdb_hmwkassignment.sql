CREATE DATABASE  IF NOT EXISTS `linearhmwkdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `linearhmwkdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: linearhmwkdb
-- ------------------------------------------------------
-- Server version	5.6.14

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
  `grade` decimal(10,0) NOT NULL DEFAULT '0',
  `status` varchar(45) NOT NULL DEFAULT 'Assigned',
  `currentQuestion` bigint(20) NOT NULL DEFAULT '1',
  `userId` bigint(20) NOT NULL,
  PRIMARY KEY (`assignmentId`),
  UNIQUE KEY `assignmentId_UNIQUE` (`assignmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hmwkassignment`
--

LOCK TABLES `hmwkassignment` WRITE;
/*!40000 ALTER TABLE `hmwkassignment` DISABLE KEYS */;
INSERT INTO `hmwkassignment` VALUES (1,1,0,'Assigned',1,1),(2,1,0,'Assigned',1,2),(3,2,0,'Assigned',1,1),(4,2,0,'Assigned',1,2);
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

-- Dump completed on 2013-10-29 17:37:56
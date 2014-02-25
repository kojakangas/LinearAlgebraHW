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
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `userId` bigint(20) NOT NULL AUTO_INCREMENT,
  `first` varchar(45) NOT NULL,
  `last` varchar(45) NOT NULL,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `role` varchar(45) NOT NULL DEFAULT 'S',
  PRIMARY KEY (`userId`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  UNIQUE KEY `usercol_UNIQUE` (`userId`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Scott','Sigman','ssigman','ba1097e043808f9eb7b002527965e66eea68f996','I'),(2,'Donald','Duck','dduck','403e35a2b0243d40400af6bb358b5c546cddd981','S'),(3,'Buggs','Bunny','bbunny','403e35a2b0243d40400af6bb358b5c546cddd981','S'),(4,'Jeffrey','Robinson','HaloToxin','3f532e3aa56e393fda930298047e5c9101d16cf2','S'),(5,'debbie','peana','dpeana','baa5798610d831a07c7a195ac22e5dd1530dba21','S'),(6,'Stephanie','Mazzoni','smazzoni','3708cf23bf5bcd14a2383a4fb24c4af1fb4fb352','S'),(7,'Alexa','Busch','ABusch','71922ef452d0dd15e56526b1ec98c0704d8e91af','S'),(8,'Drew','Gann','agann003','fad24824c08bfd36f1791d6b62600b3e7fedfdc6','S'),(9,'Anjan','Shrestha','ashrestha003','43e299793336d3ea984bfc72a5e4017c82feed4b','S'),(10,'Caleb','Templeton','ctemps7','67b555c86dcbcef0f570a33af15527e3108da045','S'),(11,'Dalton','Sly','dsly','f6704ab80b4dcc5925980892ec18f440d004f44d','S'),(12,'Trey','Norris','Znorris','85c3fd2ead11b352bef5ca50d312570a3809087e','S'),(13,'Josef','Polodna','munstr','56ea82186419d4458e3c3970820b7369c3af4684','S'),(14,'McKenna','Feltes','mfeltes','185861a335a143dcb1b8fe066c894c5ab33ea761','S'),(15,'Loli','Baquerizo','mbaquerizo','ea533fa7032bc6fdd5004eab941a2457bedfd2e5','S'),(16,'Daisy','Duck','daisyDuck','403e35a2b0243d40400af6bb358b5c546cddd981','S'),(17,'Barrett','Cummins','bcummins','da55a29900fe7301a787ec86ebbc365cac3cdcfb','S'),(18,'shahad','sadeq','ssadeq','e3022f933c44a307426b85f643ed6294752c6ea5','S'),(19,'Guy','Tennison','gtennison','057a8d722d6f630ea52562809cf1a5246f4f7a4c','S'),(20,'Wayne','Elliott','welliott','4785d6182d8df5be18ac38211f3767cb1946286d','S'),(21,'Minh','Duong','minhduong','f9a56c2e71299bded231b80925b4aac6b1bf1cf5','S'),(22,'Kurt','Smith','ksmith034','80161b461e9b2a41a6a2abcc3c0ba0d4c03491c8','S'),(23,'Brendan','Birdsong','bbirdsong','dc76e9f0c0006e8f919e0c515c66dbba3982f785','S'),(24,'Kieran','Ojakangas','kojakangas','68305fb5c4ff13a66e07f9d57ce67b271d765e69','S'),(25,'Tyler','Jenkins','Cooper640','5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8','S'),(26,'Test','User','tuser','dc76e9f0c0006e8f919e0c515c66dbba3982f785','S');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
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

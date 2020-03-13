CREATE DATABASE  IF NOT EXISTS `projektlabor` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_german2_ci */;
USE `projektlabor`;
-- MariaDB dump 10.17  Distrib 10.4.10-MariaDB, for Win64 (AMD64)
--
-- Host: 127.0.0.1    Database: projektlabor
-- ------------------------------------------------------
-- Server version	10.4.10-MariaDB

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
-- Table structure for table `ibutton`
--

DROP TABLE IF EXISTS `ibutton`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ibutton` (
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `Typ` varchar(15) COLLATE utf8mb4_german2_ci NOT NULL,
  UNIQUE KEY `iButtonID` (`iButtonID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ibutton`
--

LOCK TABLES `ibutton` WRITE;
/*!40000 ALTER TABLE `ibutton` DISABLE KEYS */;
INSERT INTO `ibutton` VALUES ('48EAE319008BA100','DS1990A'),('9E31E31900D3A100','DS1990A'),('A225E41900A6A100','DS1990A'),('A4D2E21900EAA100','DS1990A'),('A570017248BC0','DS1990A'),('ACA001B9E2B0','DS1990A'),('C08B241700574100','DS1990A'),('EE2ADB190013A100','DS1990A'),('FF30E419006C4100','DS1990A');
/*!40000 ALTER TABLE `ibutton` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `log`
--

DROP TABLE IF EXISTS `log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `log` (
  `LogID` int(10) NOT NULL AUTO_INCREMENT,
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Starttime` datetime NOT NULL,
  `Endtime` datetime DEFAULT NULL,
  PRIMARY KEY (`LogID`),
  KEY `iButtonID` (`iButtonID`),
  KEY `MaschinenID` (`MaschinenID`),
  CONSTRAINT `log_ibfk_3` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`),
  CONSTRAINT `log_ibfk_4` FOREIGN KEY (`MaschinenID`) REFERENCES `maschine` (`MaschinenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `log`
--

LOCK TABLES `log` WRITE;
/*!40000 ALTER TABLE `log` DISABLE KEYS */;
INSERT INTO `log` VALUES (1,'A4D2E21900EAA100','pl3drb','2020-02-21 08:15:12','2020-02-21 10:23:01'),(2,'FF30E419006C4100','pl4sbs','2020-02-27 10:24:24','2020-02-27 10:53:41');
/*!40000 ALTER TABLE `log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maschine`
--

DROP TABLE IF EXISTS `maschine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maschine` (
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Bezeichnung` varchar(30) COLLATE utf8mb4_german2_ci NOT NULL,
  PRIMARY KEY (`MaschinenID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maschine`
--

LOCK TABLES `maschine` WRITE;
/*!40000 ALTER TABLE `maschine` DISABLE KEYS */;
INSERT INTO `maschine` VALUES ('pl1cnc','CNC-Maschine'),('pl23dd','3D-Drucker'),('pl3drb','Drehbank'),('pl4sbs','Schubladensystem');
/*!40000 ALTER TABLE `maschine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `UserID` int(10) NOT NULL AUTO_INCREMENT,
  `Vorname` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Nachname` varchar(30) COLLATE utf8mb4_german2_ci NOT NULL,
  `E_Mail` varchar(40) COLLATE utf8mb4_german2_ci NOT NULL,
  `Keymember` tinyint(1) NOT NULL,
  `Benutzername` varchar(20) COLLATE utf8mb4_german2_ci DEFAULT NULL,
  `Passwort` varchar(100) COLLATE utf8mb4_german2_ci DEFAULT NULL,
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `iButtonID` (`iButtonID`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Berthold','Sommer','b.sommer@berufskolleg-rheine.de',1,'somm','$2a$11$BaquQT.bkS1hR/8XLUQg9umoioV3BGutiiy10Kqb3752zTaSyaR9C','FF30E419006C4100'),(2,'Damian','Zdanowitsch','d.zdanowicz@berufskolleg-rheine.de',1,'zdan','pass123','C08B241700574100'),(3,'Luca','Schöneberg','ls@gmx.de',0,'schoeneberg','$2a$11$9s/r98RrGbk9m9DE2uNWzOijZNjqfjdmFKhj4AUVU0N0D7yLrb5KK','A4D2E21900EAA100');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `zuweisung`
--

DROP TABLE IF EXISTS `zuweisung`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `zuweisung` (
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Datum` date NOT NULL,
  PRIMARY KEY (`MaschinenID`,`iButtonID`),
  KEY `iButtonID` (`iButtonID`),
  CONSTRAINT `zuweisung_ibfk_3` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`),
  CONSTRAINT `zuweisung_ibfk_4` FOREIGN KEY (`MaschinenID`) REFERENCES `maschine` (`MaschinenID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zuweisung`
--

LOCK TABLES `zuweisung` WRITE;
/*!40000 ALTER TABLE `zuweisung` DISABLE KEYS */;
INSERT INTO `zuweisung` VALUES ('ACA001B9E2B0','pl1cnc','2020-03-13'),('FF30E419006C4100','pl1cnc','2020-03-04'),('ACA001B9E2B0','pl23dd','2020-03-13'),('C08B241700574100','pl23dd','2020-03-05'),('FF30E419006C4100','pl23dd','2020-03-04'),('A225E41900A6A100','pl3drb','2020-03-05'),('ACA001B9E2B0','pl3drb','2020-03-13'),('FF30E419006C4100','pl3drb','2020-03-04'),('ACA001B9E2B0','pl4sbs','2020-03-13'),('FF30E419006C4100','pl4sbs','2020-03-04');
/*!40000 ALTER TABLE `zuweisung` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-13 17:46:07

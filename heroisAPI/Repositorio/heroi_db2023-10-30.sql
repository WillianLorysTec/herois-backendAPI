-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: heroi_db
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `herois`
--

DROP TABLE IF EXISTS `herois`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `herois` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Nome` varchar(120) NOT NULL,
  `NomeHeroi` varchar(120) NOT NULL,
  `DataNascimento` datetime DEFAULT NULL,
  `Altura` float NOT NULL,
  `Peso` float NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `herois`
--

LOCK TABLES `herois` WRITE;
/*!40000 ALTER TABLE `herois` DISABLE KEYS */;
/*!40000 ALTER TABLE `herois` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `heroissuperpoderes`
--

DROP TABLE IF EXISTS `heroissuperpoderes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `heroissuperpoderes` (
  `HeroiId` int unsigned NOT NULL,
  `SuperpoderId` int unsigned NOT NULL,
  PRIMARY KEY (`HeroiId`,`SuperpoderId`),
  KEY `fk_Herois_has_Superpoderes_Superpoderes1_idx` (`SuperpoderId`),
  KEY `fk_Herois_has_Superpoderes_Herois_idx` (`HeroiId`),
  CONSTRAINT `fk_Herois_has_Superpoderes_Herois` FOREIGN KEY (`HeroiId`) REFERENCES `herois` (`Id`),
  CONSTRAINT `fk_Herois_has_Superpoderes_Superpoderes1` FOREIGN KEY (`SuperpoderId`) REFERENCES `superpoderes` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `heroissuperpoderes`
--

LOCK TABLES `heroissuperpoderes` WRITE;
/*!40000 ALTER TABLE `heroissuperpoderes` DISABLE KEYS */;
/*!40000 ALTER TABLE `heroissuperpoderes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `superpoderes`
--

DROP TABLE IF EXISTS `superpoderes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `superpoderes` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `Superpoder` varchar(50) NOT NULL,
  `Descricao` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `superpoderes`
--

LOCK TABLES `superpoderes` WRITE;
/*!40000 ALTER TABLE `superpoderes` DISABLE KEYS */;
INSERT INTO `superpoderes` VALUES (1,'Raio Lazer','Fica com os olhos vermelhos e solta raio lazer'),(2,'Vira Flor','Vira uma florzinha bem linda'),(3,'Ser um programador','É um grande poder'),(4,'Imortalidade','Imortalidade');
/*!40000 ALTER TABLE `superpoderes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-30  4:39:44

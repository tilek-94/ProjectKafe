-- --------------------------------------------------------
-- Хост:                         localhost
-- Версия сервера:               5.7.29-log - MySQL Community Server (GPL)
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных basakafe
CREATE DATABASE IF NOT EXISTS `basakafe` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `basakafe`;

-- Дамп структуры для таблица basakafe.checks
CREATE TABLE IF NOT EXISTS `checks` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CheckCount` int(11) NOT NULL,
  `TableId` int(11) NOT NULL,
  `DateTimeCheck` datetime(6) NOT NULL,
  `WaiterId` int(11) NOT NULL,
  `Status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Checks_TableId` (`TableId`),
  KEY `IX_Checks_WaiterId` (`WaiterId`),
  CONSTRAINT `FK_Checks_Tables_TableId` FOREIGN KEY (`TableId`) REFERENCES `tables` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Checks_Waiters_WaiterId` FOREIGN KEY (`WaiterId`) REFERENCES `waiters` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.checks: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `checks` DISABLE KEYS */;
/*!40000 ALTER TABLE `checks` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.foods
CREATE TABLE IF NOT EXISTS `foods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text,
  `Price` decimal(65,30) NOT NULL,
  `Image` longtext,
  `ParentCategoryId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.foods: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `foods` DISABLE KEYS */;
/*!40000 ALTER TABLE `foods` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.locations
CREATE TABLE IF NOT EXISTS `locations` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.locations: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `locations` DISABLE KEYS */;
/*!40000 ALTER TABLE `locations` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.options
CREATE TABLE IF NOT EXISTS `options` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(50) DEFAULT NULL,
  `Value` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.options: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `options` DISABLE KEYS */;
INSERT INTO `options` (`Id`, `Key`, `Value`) VALUES
	(1, 'Test1', 'test2');
/*!40000 ALTER TABLE `options` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FoodId` int(11) NOT NULL,
  `CheckId` int(11) NOT NULL,
  `CountFood` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Orders_CheckId` (`CheckId`),
  KEY `IX_Orders_FoodId` (`FoodId`),
  CONSTRAINT `FK_Orders_Checks_CheckId` FOREIGN KEY (`CheckId`) REFERENCES `checks` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Orders_Foods_FoodId` FOREIGN KEY (`FoodId`) REFERENCES `foods` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.orders: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.products
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.products: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
/*!40000 ALTER TABLE `products` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.receiptgoods
CREATE TABLE IF NOT EXISTS `receiptgoods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DateTimeReceiptGoods` datetime(6) NOT NULL,
  `Count` int(11) NOT NULL,
  `Price` double NOT NULL,
  `ProductId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ReceiptGoods_ProductId` (`ProductId`),
  CONSTRAINT `FK_ReceiptGoods_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.receiptgoods: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `receiptgoods` DISABLE KEYS */;
/*!40000 ALTER TABLE `receiptgoods` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.recipes
CREATE TABLE IF NOT EXISTS `recipes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FoodId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `CountPoduct` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Recipes_FoodId` (`FoodId`),
  KEY `IX_Recipes_ProductId` (`ProductId`),
  CONSTRAINT `FK_Recipes_Foods_FoodId` FOREIGN KEY (`FoodId`) REFERENCES `foods` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Recipes_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.recipes: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `recipes` DISABLE KEYS */;
/*!40000 ALTER TABLE `recipes` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.tables
CREATE TABLE IF NOT EXISTS `tables` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `LocationId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Tables_LocationId` (`LocationId`),
  CONSTRAINT `FK_Tables_Locations_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `locations` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.tables: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `tables` DISABLE KEYS */;
/*!40000 ALTER TABLE `tables` ENABLE KEYS */;

-- Дамп структуры для таблица basakafe.waiters
CREATE TABLE IF NOT EXISTS `waiters` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Pass` varchar(50) DEFAULT NULL,
  `Tel` varchar(50) DEFAULT NULL,
  `address` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Дамп данных таблицы basakafe.waiters: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `waiters` DISABLE KEYS */;
/*!40000 ALTER TABLE `waiters` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

CREATE DATABASE IF NOT EXISTS `gummibearkingdom` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `gummibearkingdom`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20180428041251_Initial', '1.1.2');

CREATE TABLE `categories` (
  `CategoryId` int(11) NOT NULL,
  `Name` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `categories` (`CategoryId`, `Name`) VALUES
(1, 'Toys'),
(2, 'Candy'),
(3, 'Art'),
(4, 'Apparel');

CREATE TABLE `products` (
  `ProductId` int(11) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `Description` longtext,
  `Name` longtext,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `products` (`ProductId`, `CategoryId`, `Description`, `Name`, `Price`) VALUES
(1, 1, 'Gummy bear toy', 'Gummy Bear Doll', 35),
(2, 2, 'tasty treat', 'Gummy Bear', 3),
(3, 3, '3x4 foot poster of a Gummy Bear', 'Gummy Bear Poster', 20),
(4, 2, 'A healthy start to the day! Fiber filled cereal with gummy bears in it!', 'Gummy Bears Cereal', 5),
(5, 4, 'Gummi Bear Kingdom Tshirt - assorted Sizes', 'Gummi Bear Kingdom T', 25);

CREATE TABLE `reviews` (
  `ReviewId` int(11) NOT NULL,
  `Author` longtext,
  `Content_Body` longtext,
  `ProductId` int(11) NOT NULL,
  `rating` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `reviews` (`ReviewId`, `Author`, `Content_Body`, `ProductId`, `rating`) VALUES
(1, 'Jasun', 'this is really yummy', 1, 5),
(5, 'Oscar', 'too sweet!', 1, 1),
(6, 'Vanessa', 'lovely doll', 2, 5),
(7, 'Barry', 'nothing like the original!', 1, 5),
(8, 'Regina', 'fits the decor of my home perfectly', 3, 5),
(9, 'Francis', 'fits, but colors are not like the picture', 5, 2),
(10, 'Troy', 'needed it for a party. came on time.', 5, 4),
(11, 'Vance', 'too big!', 3, 2),
(15, 'Hope', 'the poster is great. shipped late. boohoo', 3, 3),
(16, 'Albert', 'what? gross!!!', 4, 1);


ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

ALTER TABLE `categories`
  ADD PRIMARY KEY (`CategoryId`);

ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductId`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`);

ALTER TABLE `reviews`
  ADD PRIMARY KEY (`ReviewId`),
  ADD KEY `IX_Reviews_ProductId` (`ProductId`);


ALTER TABLE `categories`
  MODIFY `CategoryId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
ALTER TABLE `products`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
ALTER TABLE `reviews`
  MODIFY `ReviewId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

ALTER TABLE `products`
  ADD CONSTRAINT `FK_Products_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`CategoryId`) ON DELETE CASCADE;
CREATE DATABASE IF NOT EXISTS `gummibearkingdom_tests` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `gummibearkingdom_tests`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `categories` (
  `CategoryId` int(11) NOT NULL,
  `Name` longtext
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `products` (
  `ProductId` int(11) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `Description` longtext,
  `Name` longtext,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `reviews` (
  `ReviewId` int(11) NOT NULL,
  `Author` longtext,
  `Content_Body` longtext,
  `ProductId` int(11) NOT NULL,
  `rating` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

ALTER TABLE `categories`
  ADD PRIMARY KEY (`CategoryId`);

ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductId`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`);

ALTER TABLE `reviews`
  ADD PRIMARY KEY (`ReviewId`),
  ADD KEY `IX_Reviews_ProductId` (`ProductId`);


ALTER TABLE `categories`
  MODIFY `CategoryId` int(11) NOT NULL AUTO_INCREMENT;
ALTER TABLE `products`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT;
ALTER TABLE `reviews`
  MODIFY `ReviewId` int(11) NOT NULL AUTO_INCREMENT;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 06. Mrz 2020 um 19:46
-- Server-Version: 10.4.8-MariaDB
-- PHP-Version: 7.3.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `projektlabor` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_german2_ci */;

USE `projektlabor`;

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `projektlabor`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ibutton`
--

CREATE TABLE `ibutton` (
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `Typ` varchar(15) COLLATE utf8mb4_german2_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;

--
-- Daten für Tabelle `ibutton`
--

INSERT INTO `ibutton` (`iButtonID`, `Typ`) VALUES
('48EAE319008BA100', 'DS1990A'),
('9E31E31900D3A100', 'DS1990A'),
('A225E41900A6A100', 'DS1990A'),
('A4D2E21900EAA100', 'DS1990A'),
('C08B241700574100', 'DS1990A'),
('EE2ADB190013A100', 'DS1990A'),
('FF30E419006C4100', 'DS1990A');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `log`
--

CREATE TABLE `log` (
  `LogID` int(10) NOT NULL,
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Starttime` datetime NOT NULL,
  `Endtime` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;

--
-- Daten für Tabelle `log`
--

INSERT INTO `log` (`LogID`, `iButtonID`, `MaschinenID`, `Starttime`, `Endtime`) VALUES
(1, 'A4D2E21900EAA100', 'pl3drb', '2020-02-21 08:15:12', '2020-02-21 10:23:01'),
(2, 'FF30E419006C4100', 'pl4sbs', '2020-02-27 10:24:24', '2020-02-27 10:53:41');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `maschine`
--

CREATE TABLE `maschine` (
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Bezeichnung` varchar(30) COLLATE utf8mb4_german2_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;

--
-- Daten für Tabelle `maschine`
--

INSERT INTO `maschine` (`MaschinenID`, `Bezeichnung`) VALUES
('pl1cnc', 'CNC-Maschine'),
('pl23dd', '3D-Drucker'),
('pl3drb', 'Drehbank'),
('pl4sbs', 'Schubladensystem');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

CREATE TABLE `user` (
  `UserID` int(10) NOT NULL,
  `Vorname` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Nachname` varchar(30) COLLATE utf8mb4_german2_ci NOT NULL,
  `E-Mail` varchar(40) COLLATE utf8mb4_german2_ci NOT NULL,
  `Keymember` tinyint(1) NOT NULL,
  `Benutzername` varchar(20) COLLATE utf8mb4_german2_ci DEFAULT NULL,
  `Passwort` varchar(30) COLLATE utf8mb4_german2_ci DEFAULT NULL,
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;

--
-- Daten für Tabelle `user`
--

INSERT INTO `user` (`UserID`, `Vorname`, `Nachname`, `E-Mail`, `Keymember`, `Benutzername`, `Passwort`, `iButtonID`) VALUES
(1, 'Berthold', 'Sommer', 'b.sommer@berufskolleg-rheine.de', 1, 'somm', 'pass123', 'FF30E419006C4100'),
(2, 'Damian', 'Zdanowitsch', 'd.zdanowicz@berufskolleg-rheine.de', 1, 'zdan', 'pass123', 'C08B241700574100'),
(3, 'Luca', 'Schöneberg', 'ls@gmx.de', 0, NULL, NULL, 'A4D2E21900EAA100'),
(4, 'Maximilian', 'Musterknabe', 'mm@muster.de', 0, NULL, NULL, 'A225E41900A6A100');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `zuweisung`
--

CREATE TABLE `zuweisung` (
  `iButtonID` varchar(16) COLLATE utf8mb4_german2_ci NOT NULL,
  `MaschinenID` varchar(20) COLLATE utf8mb4_german2_ci NOT NULL,
  `Datum` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_german2_ci;

--
-- Daten für Tabelle `zuweisung`
--

INSERT INTO `zuweisung` (`iButtonID`, `MaschinenID`, `Datum`) VALUES
('A4D2E21900EAA100', 'pl1cnc', '2020-03-05'),
('FF30E419006C4100', 'pl1cnc', '2020-03-04'),
('A4D2E21900EAA100', 'pl23dd', '2020-03-04'),
('C08B241700574100', 'pl23dd', '2020-03-05'),
('FF30E419006C4100', 'pl23dd', '2020-03-04'),
('A225E41900A6A100', 'pl3drb', '2020-03-05'),
('A4D2E21900EAA100', 'pl3drb', '2020-03-05'),
('FF30E419006C4100', 'pl3drb', '2020-03-04'),
('FF30E419006C4100', 'pl4sbs', '2020-03-04');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `ibutton`
--
ALTER TABLE `ibutton`
  ADD UNIQUE KEY `iButtonID` (`iButtonID`) USING BTREE;

--
-- Indizes für die Tabelle `log`
--
ALTER TABLE `log`
  ADD PRIMARY KEY (`LogID`),
  ADD KEY `iButtonID` (`iButtonID`),
  ADD KEY `MaschinenID` (`MaschinenID`);

--
-- Indizes für die Tabelle `maschine`
--
ALTER TABLE `maschine`
  ADD PRIMARY KEY (`MaschinenID`);

--
-- Indizes für die Tabelle `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserID`),
  ADD KEY `iButtonID` (`iButtonID`);

--
-- Indizes für die Tabelle `zuweisung`
--
ALTER TABLE `zuweisung`
  ADD PRIMARY KEY (`MaschinenID`,`iButtonID`),
  ADD KEY `iButtonID` (`iButtonID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `log`
--
ALTER TABLE `log`
  MODIFY `LogID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `user`
--
ALTER TABLE `user`
  MODIFY `UserID` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `log`
--
ALTER TABLE `log`
  ADD CONSTRAINT `log_ibfk_3` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`),
  ADD CONSTRAINT `log_ibfk_4` FOREIGN KEY (`MaschinenID`) REFERENCES `maschine` (`MaschinenID`);

--
-- Constraints der Tabelle `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`);

--
-- Constraints der Tabelle `zuweisung`
--
ALTER TABLE `zuweisung`
  ADD CONSTRAINT `zuweisung_ibfk_3` FOREIGN KEY (`iButtonID`) REFERENCES `ibutton` (`iButtonID`),
  ADD CONSTRAINT `zuweisung_ibfk_4` FOREIGN KEY (`MaschinenID`) REFERENCES `maschine` (`MaschinenID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

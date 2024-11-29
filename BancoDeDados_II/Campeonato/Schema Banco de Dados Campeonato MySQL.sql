DROP DATABASE IF EXISTS tournmentDB;
CREATE DATABASE tournmentDB;
USE tournmentDB;

CREATE TABLE tournment (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    startDate DATETIME NOT NULL,
    endDate DATETIME NULL,
    CHECK (startDate < endDate)
);

CREATE TABLE player (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE modality (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    individual BOOLEAN NOT NULL,
    description VARCHAR(500) NOT NULL,
    maxParticipants INT NOT NULL
);

CREATE TABLE team (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE modalityInTournment(
	idTournment INT NOT NULL,
    idModality INT NOT NULL,
    CONSTRAINT fk_modalityInTournment_idTournment FOREIGN KEY (idTournment) REFERENCES tournment(id),
    CONSTRAINT fk_modalityInTournment_idModality FOREIGN KEY (idModality) REFERENCES modality(id)
);

CREATE TABLE modalityRegistrations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idModality INT NOT NULL,
    idPlayer INT NULL,
    idTeam INT NULL,
    typeParticipants CHAR(1) NOT NULL, -- 'I' (Individual) or 'T' (Team)
    CONSTRAINT fk_modalityRegistrations_idModality FOREIGN KEY (idModality) REFERENCES modality(id),
    CONSTRAINT fk_modalityRegistrations_idPlayer FOREIGN KEY (idPlayer) REFERENCES player(id),
    CONSTRAINT fk_modalityRegistrations_idTeam FOREIGN KEY (idTeam) REFERENCES team(id)
);

CREATE TABLE playerInTeam (
    idPlayer INT NOT NULL,
    idTeam INT NOT NULL,
    CONSTRAINT fk_playerInTeam_idPlayer FOREIGN KEY (idPlayer) REFERENCES player(id),
    CONSTRAINT fk_playerInTeam_idTeam FOREIGN KEY (idTeam) REFERENCES team(id)
);

CREATE TABLE `match` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idModality INT NOT NULL,
    idWinningTeam INT NULL,
    idWinningPlayer INT NULL,
    type_ CHAR(1) NOT NULL, -- 'I' (Individual) or 'T' (Team)
    matchTime DATETIME NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'scheduled', -- Ex.: "scheduled", "running", "completed"
    CONSTRAINT fk_match_idModality FOREIGN KEY (idModality) REFERENCES modality(id),
    CONSTRAINT fk_match_idWinningPlayer FOREIGN KEY (idWinningPlayer) REFERENCES player(id),
    CONSTRAINT fk_match_idWinningTeam FOREIGN KEY (idWinningTeam) REFERENCES team(id)
);

CREATE TABLE matchParticipants (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idMatch INT NOT NULL,
    idPlayer INT NULL,
    idTeam INT NULL,
    CONSTRAINT fk_matchParticipants_idMatch FOREIGN KEY (idMatch) REFERENCES `match`(id),
    CONSTRAINT fk_matchParticipants_idPlayer FOREIGN KEY (idPlayer) REFERENCES player(id),
    CONSTRAINT fk_matchParticipants_idTeam FOREIGN KEY (idTeam) REFERENCES team(id)
);

CREATE TABLE bracket (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idMatch INT NOT NULL,
    fase INT NOT NULL, -- Ex.: 1 for octaves, 2 for quarters, etc.
    position INT NOT NULL, -- Position in the bracket
    parentId INT NULL,
    CONSTRAINT fk_bracket_idMatch FOREIGN KEY (idMatch) REFERENCES `match`(id),
    CONSTRAINT fk_bracket_bracketId FOREIGN KEY (parentId) REFERENCES bracket(id)
);

CREATE TABLE tournmentPoints (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idModality INT NOT NULL,
    idTeam INT NULL,
    idPlayer INT NULL,
    points INT NOT NULL DEFAULT 0,
    CONSTRAINT fk_tournmentPoints_idModality FOREIGN KEY (idModality) REFERENCES modality(id),
    CONSTRAINT fk_tournmentPoints_idTeam FOREIGN KEY (idTeam) REFERENCES team(id),
    CONSTRAINT fk_tournmentPoints_idPlayer FOREIGN KEY (idPlayer) REFERENCES player(id)
);

CREATE TABLE matchPoints (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idMatch INT NOT NULL,
    idPlayer INT NULL,
    idTeam INT NULL,
    points INT NOT NULL DEFAULT 0,
    CONSTRAINT fk_matchPoints_idMatch FOREIGN KEY (idMatch) REFERENCES `match`(id),
    CONSTRAINT fk_matchPoints_idPlayer FOREIGN KEY (idPlayer) REFERENCES player(id),
    CONSTRAINT fk_matchPoints_idTeam FOREIGN KEY (idTeam) REFERENCES team(id)
);

CREATE TABLE matchHistory (
    id INT AUTO_INCREMENT PRIMARY KEY,
    idMatch INT NOT NULL,
    `description` VARCHAR(500) NOT NULL,
    CONSTRAINT fk_matchHistory_idMatch FOREIGN KEY (idMatch) REFERENCES `match`(id)
);

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE logsSystem (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NOT NULL,
    action VARCHAR(255) NOT NULL,
    eventType ENUM('INFO', 'ERROR', 'UPDATE', 'DELETE') NOT NULL,
    time DATETIME NOT NULL,
    CONSTRAINT fk_logsSystem_userId FOREIGN KEY (userId) REFERENCES users(id)
);
-- Adding a user
INSERT INTO users (username) VALUES ('Username');

-- Adding more players
INSERT INTO player (name)
VALUES 
    ('Alice Johnson'),
    ('Bob Smith'),
    ('Charlie Brown'),
    ('Diana Prince'),
    ('Eve Adams'),
    ('Franklin Harris'),
    ('Grace Lee'),
    ('Henry Ford'),
    ('Isla Fisher'),
    ('Jack Daniels'),
    ('Karen Jones'),
    ('Leo Messi'),
    ('Martha Stewart'),
    ('Nathan Drake'),
    ('Olivia Wilde'),
    ('Paul McCartney'),
    ('Quinn Fabray'),
    ('Rachel Green'),
    ('Samuel Jackson'),
    ('Tina Fey'),
    ('Ursula K. Le Guin'),
    ('Victor Hugo'),
    ('Wanda Maximoff'),
    ('Xander Cage'),
    ('Yvonne Strahovski'),
    ('Zachary Levi');


-- Adding more teams
INSERT INTO team (name)
VALUES 
    ('The Champions'),
    ('Dream Team'),
    ('Rising Stars'),
    ('Eagle Squad'),
    ('Falcon Force'),
    ('Golden Warriors'),
    ('Stormbreakers'),
    ('Shadow Hunters'),
    ('Iron Giants'),
    ('Phoenix Fire');


-- Assigning players to teams
INSERT INTO playerInTeam (idPlayer, idTeam)
VALUES 
    (1, 1), (2, 1), (3, 1), (4, 1), -- Team 1
    (5, 2), (6, 2), (7, 2), (8, 2), -- Team 2
    (9, 3), (10, 3), (11, 3), (12, 3), -- Team 3
    (13, 4), (14, 4), (15, 4), (16, 4), -- Team 4
    (17, 5), (18, 5), (19, 5), (20, 5), -- Team 5
    (21, 6), (22, 6), (23, 6), (24, 6), -- Team 6
    (25, 7), (26, 7), (1, 7), (2, 7), -- Team 7
    (3, 8), (4, 8), (5, 8), (6, 8), -- Team 8
    (7, 9), (8, 9), (9, 9), (10, 9), -- Team 9
    (11, 10), (12, 10), (13, 10), (14, 10); -- Team 10


-- Populate Tournaments
INSERT INTO tournment (name, startDate, endDate) VALUES 
('Spring Championship', '2024-03-01 10:00:00', '2024-03-31 18:00:00'),
('Summer Tournament', '2024-06-01 10:00:00', '2024-06-30 18:00:00');


-- Populate Modalities
INSERT INTO modality (name, individual, description, maxParticipants) VALUES 
('Chess', TRUE, 'Individual strategy game', 16),
('Soccer', FALSE, 'Team-based sports game', 10),
('Tennis', TRUE, 'Individual or duo racket sport', 8);


-- Link Modalities to Tournaments
INSERT INTO modalityInTournment (idTournment, idModality) VALUES 
(1, 1), -- Chess in Spring Championship
(1, 2), -- Soccer in Spring Championship
(2, 3), -- Tennis in Summer Tournament
(2, 1); -- Chess in Summer Tournament


INSERT INTO modalityRegistrations (idModality, idPlayer, idTeam, typeParticipants)
VALUES 
    -- Individual Modality (Chess)
    (1, 1, NULL, 'I'), -- Player 1 (Competing independently)
    (1, 2, NULL, 'I'), -- Player 2 (Competing independently)
    (1, 3, 1, 'I'), -- Player 3 (Part of Team 1)
    (1, 4, 2, 'I'), -- Player 4 (Part of Team 2)
    -- Team Modality (Soccer)
    (2, NULL, 1, 'T'), -- Team 1
    (2, NULL, 2, 'T'), -- Team 2
    (2, NULL, 3, 'T'), -- Team 3
    (2, NULL, 4, 'T'); -- Team 4


-- Adding matches for each modality
-- Chess
-- Round 1 Matches
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (1, 5, NULL, 'I', '2024-03-10 14:00:00', 'completed'), -- Match 1: Player 5 wins
    (1, 6, NULL, 'I', '2024-03-10 15:00:00', 'completed'), -- Match 2: Player 6 wins
    (1, 7, NULL, 'I', '2024-03-10 16:00:00', 'completed'), -- Match 3: Player 7 wins
    (1, 8, NULL, 'I', '2024-03-10 17:00:00', 'completed'); -- Match 4: Player 8 wins

-- Round 2 Matches
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (1, 5, NULL, 'I', '2024-03-17 14:00:00', 'completed'), -- Match 5: Player 5 wins
    (1, 8, NULL, 'I', '2024-03-17 15:00:00', 'completed'); -- Match 6: Player 8 wins

-- Final Match
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (1, 5, NULL, 'I', '2024-03-24 14:00:00', 'completed'); -- Match 7: Player 5 wins

-- Soccer
-- Round 1 Matches
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (2, NULL, 1, 'T', '2024-03-11 14:00:00', 'completed'), -- Match 1: Team 1 wins
    (2, NULL, 2, 'T', '2024-03-11 16:00:00', 'completed'), -- Match 2: Team 2 wins
    (2, NULL, 3, 'T', '2024-03-11 18:00:00', 'completed'), -- Match 3: Team 3 wins
    (2, NULL, 4, 'T', '2024-03-11 20:00:00', 'completed'); -- Match 4: Team 4 wins

-- Semi-Finals
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (2, NULL, 1, 'T', '2024-03-18 14:00:00', 'completed'), -- Match 5: Team 1 wins
    (2, NULL, 4, 'T', '2024-03-18 16:00:00', 'completed'); -- Match 6: Team 4 wins

-- Final Match
INSERT INTO `match` (idModality, idWinningPlayer, idWinningTeam, type_, matchTime, status)
VALUES 
    (2, NULL, 1, 'T', '2024-03-25 14:00:00', 'completed'); -- Match 7: Team 1 wins


-- Adding participants for each match
-- Chess
-- Round 1 Participants
INSERT INTO matchParticipants (idMatch, idPlayer, idTeam)
VALUES 
    (5, 5, NULL), (5, 6, NULL), -- Match 1
    (6, 7, NULL), (6, 8, NULL); -- Match 2

-- Round 2 Participants
INSERT INTO matchParticipants (idMatch, idPlayer, idTeam)
VALUES 
    (7, 5, NULL), (7, 8, NULL); -- Final Match

-- Soccer
-- Round 1 Participants
INSERT INTO matchParticipants (idMatch, idPlayer, idTeam)
VALUES 
    (8, NULL, 1), (8, NULL, 2), -- Match 1
    (9, NULL, 3), (9, NULL, 4); -- Match 2

-- Semi-Finals
INSERT INTO matchParticipants (idMatch, idPlayer, idTeam)
VALUES 
    (10, NULL, 1), (10, NULL, 4); -- Final Match


-- Adding points for each match
INSERT INTO matchPoints (idMatch, idPlayer, idTeam, points)
VALUES 
    -- Match 1
    (5, 5, NULL, 3), (5, 6, NULL, -1),
    -- Match 2
    (6, 8, NULL, 3), (6, 7, NULL, -1),
    -- Final Match
    (7, 5, NULL, 3), (7, 8, NULL, -1);

INSERT INTO matchPoints (idMatch, idPlayer, idTeam, points)
VALUES 
    -- Match 1
    (8, NULL, 1, 3), (8, NULL, 2, -1),
    -- Match 2
    (9, NULL, 3, 3), (9, NULL, 4, -1);



-- Adding a simple bracket structure
-- Chess
-- Round 1 (Phase 1)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (1, 1, 1, NULL), -- Match 1, Phase 1
    (2, 1, 2, NULL), -- Match 2, Phase 1
    (3, 1, 3, NULL), -- Match 3, Phase 1
    (4, 1, 4, NULL); -- Match 4, Phase 1

-- Round 2 (Phase 2)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (5, 2, 1, NULL), -- Match 5, Phase 2
    (6, 2, 2, NULL); -- Match 6, Phase 2

-- Final (Phase 3)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (7, 3, 1, NULL); -- Match 7, Phase 3

-- Soccer
-- Round 1 (Phase 1)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (8, 1, 1, NULL), -- Match 8, Phase 1
    (9, 1, 2, NULL), -- Match 9, Phase 1
    (10, 1, 3, NULL), -- Match 10, Phase 1
    (11, 1, 4, NULL); -- Match 11, Phase 1

-- Semi-Finals (Phase 2)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (12, 2, 1, NULL), -- Match 12, Phase 2
    (13, 2, 2, NULL); -- Match 13, Phase 2

-- Final (Phase 3)
INSERT INTO bracket (idMatch, fase, position, parentId)
VALUES 
    (14, 3, 1, NULL); -- Match 14, Phase 3


-- Adding match history
-- Chess
INSERT INTO matchHistory (idMatch, `description`)
VALUES 
    (1, 'Player 5 defeated Player 1 in Round 1'),
    (2, 'Player 6 defeated Player 2 in Round 1'),
    (3, 'Player 7 defeated Player 3 in Round 1'),
    (4, 'Player 8 defeated Player 4 in Round 1'),
    (5, 'Player 5 defeated Player 6 in Round 2'),
    (6, 'Player 8 defeated Player 7 in Round 2'),
    (7, 'Player 5 defeated Player 8 in the Final to win the Chess tournament');

-- Soccer
INSERT INTO matchHistory (idMatch, `description`)
VALUES 
    (8, 'Team 1 defeated Team 5 in Round 1'),
    (9, 'Team 2 defeated Team 6 in Round 1'),
    (10, 'Team 3 defeated Team 7 in Round 1'),
    (11, 'Team 4 defeated Team 8 in Round 1'),
    (12, 'Team 1 defeated Team 2 in the Semi-Final'),
    (13, 'Team 4 defeated Team 3 in the Semi-Final'),
    (14, 'Team 1 defeated Team 4 in the Final to win the Soccer tournament');

-- Calculating and adding tournament points
INSERT INTO tournmentPoints (idModality, idTeam, idPlayer, points)
VALUES 
    -- Chess
    (1, NULL, 5, 9), -- Player 5 Total Points
    (1, NULL, 6, -1), -- Player 6
    (1, NULL, 7, 3), -- Player 7
    (1, NULL, 8, 5), -- Player 8

    -- Soccer
    (2, 1, NULL, 6), -- Team 1
    (2, 2, NULL, -1), -- Team 2
    (2, 3, NULL, 3), -- Team 3
    (2, 4, NULL, 3); -- Team 4

-- Ending the tournment early
UPDATE tournment
SET endDate = '2024-03-31 18:00:00'
WHERE id = 1;

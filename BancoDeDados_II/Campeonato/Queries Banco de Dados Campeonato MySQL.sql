-- Select players on a tournment
SELECT 
    p.name AS playerName, 
    m.name AS modalityName 
FROM player p
JOIN modalityRegistrations mr ON p.id = mr.idPlayer
JOIN modalityInTournment mt ON mr.idModality = mt.idModality
JOIN modality m ON mt.idModality = m.id
WHERE mt.idTournment = 1; -- In this case Chess


-- Select all teams in a tournment
SELECT 
    t.name AS teamName, 
    m.name AS modalityName 
FROM team t
JOIN modalityRegistrations mr ON t.id = mr.idTeam
JOIN modalityInTournment mt ON mr.idModality = mt.idModality
JOIN modality m ON mt.idModality = m.id
; -- WHERE mt.idTournment = 2; -- To search for a pecific modality


-- List a team's tournment points
SELECT t.name, tp.points
FROM team t
JOIN tournmentPoints tp ON t.id = tp.idTeam
WHERE tp.idModality IN (
    SELECT idModality 
    FROM modalityInTournment 
    WHERE idTournment = 1 -- In this case "Spring Championship"
) ORDER BY tp.points DESC;


-- Select scheduled matches
SELECT m.id, m.matchTime, m.status, m.type_ 
FROM `match` m
JOIN modalityInTournment mt ON m.idModality = mt.idModality
WHERE mt.idTournment = 1 AND m.status = 'scheduled'; -- In this case "Spring Championship" is completed, so nothing is shown


-- Select completed matches
SELECT 
    m.id AS matchId,
    m.matchTime,
    m.status,
    CASE 
        WHEN m.type_ = 'I' THEN (SELECT p.name FROM player p WHERE p.id = m.idWinningPlayer)
        ELSE (SELECT t.name FROM team t WHERE t.id = m.idWinningTeam)
    END AS winner,
    mh.description AS matchSummary,
    b.fase AS phase
FROM `match` m
LEFT JOIN matchHistory mh ON m.id = mh.idMatch
LEFT JOIN bracket b ON m.id = b.idMatch
WHERE m.status = 'completed';


-- Overall standings for a tournament 
SELECT 
    COALESCE(t.name, p.name) AS participant, 
    tp.points 
FROM tournmentPoints tp
LEFT JOIN team t ON tp.idTeam = t.id
LEFT JOIN player p ON tp.idPlayer = p.id
WHERE tp.idModality IN (
    SELECT idModality 
    FROM modalityInTournment 
    WHERE idTournment = 1
)
ORDER BY tp.points DESC;


-- Standings for each modality in the tournament
SELECT 
    COALESCE(t.name, p.name) AS participant, 
    tp.points 
FROM tournmentPoints tp
LEFT JOIN team t ON tp.idTeam = t.id
LEFT JOIN player p ON tp.idPlayer = p.id
WHERE tp.idModality IN (
    SELECT idModality 
    FROM modalityInTournment 
    WHERE idTournment = 1 -- in this case the "Spring Championship"
    AND idModality = 1 -- in this case Chess
)
ORDER BY tp.points DESC;


-- Select Brackets
SELECT b.fase, b.position, b.parentId, 
       CASE 
           WHEN m.type_ = 'I' THEN (SELECT p.name FROM player p JOIN matchParticipants mp ON p.id = mp.idPlayer WHERE mp.idMatch = b.idMatch LIMIT 1)
           ELSE (SELECT t.name FROM team t JOIN matchParticipants mp ON t.id = mp.idTeam WHERE mp.idMatch = b.idMatch LIMIT 1)
       END AS participant
FROM bracket b
JOIN `match` m ON b.idMatch = m.id;


-- Select all matches of a player or team
SELECT 
    m.id AS matchId,
    m.matchTime,
    m.type_ AS matchType,
    m.status,
    CASE 
        WHEN m.type_ = 'I' THEN (SELECT p.name FROM player p WHERE p.id = m.idWinningPlayer)
        WHEN m.type_ = 'T' THEN (SELECT t.name FROM team t WHERE t.id = m.idWinningTeam)
        ELSE 'N/A'
    END AS winner
FROM `match` m
JOIN matchParticipants mp ON m.id = mp.idMatch
WHERE mp.idPlayer = -1 OR mp.idTeam = 1; -- in this case the team "The Campions"


-- Select user action logs
SELECT ls.action, ls.eventType, ls.time, u.username
FROM logsSystem ls
JOIN users u ON ls.userId = u.id
WHERE ls.time >= NOW() - INTERVAL 7 DAY;

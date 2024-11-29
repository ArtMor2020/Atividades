
-- Triggers for logging actions into player table
DELIMITER $$
CREATE TRIGGER log_insert_player
AFTER INSERT ON player
FOR EACH ROW
BEGIN
    INSERT INTO logsSystem (userId, action, eventType, time)
    VALUES (1, CONCAT('Inserted new player with ID ', NEW.id, ': Name "', NEW.name, '"'), 'INFO', NOW());
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER log_update_player
AFTER UPDATE ON player
FOR EACH ROW
BEGIN
    INSERT INTO logsSystem (userId, action, eventType, time)
    VALUES (1, CONCAT('Updated player ID ', OLD.id, ': Name changed from "', OLD.name, '" to "', NEW.name, '"'), 'UPDATE', NOW());
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER log_delete_player
AFTER DELETE ON player
FOR EACH ROW
BEGIN
    INSERT INTO logsSystem (userId, action, eventType, time)
    VALUES (1, CONCAT('Deleted player ID ', OLD.id, ': Name "', OLD.name, '" removed'), 'DELETE', NOW());
END$$
DELIMITER ;
use AirTransport;

-- Creating companies

INSERT INTO company (id, name)
VALUES 
    (1, 'Delta AirLines'),
    (2, 'American AirLines'),
    (3, 'Malasya AirLines'),
    (4, 'Spirit AirLines'),
    (5, 'Ryanair');

-- Creating aircrafts 

INSERT INTO aircraft (id_company, type, model, number_of_seats)
VALUES 
    (1, 'Airliner', 'Boeing 747', 16), -- Numero de assentos reduzido para simplificar
    (1, 'Airliner', 'Boeing 747', 16),
    (1, 'Airliner', 'Airbus A300', 10),
    (1, 'Airliner', 'Airbus A300', 10),
    (1, 'Helicopter', 'Eurocopter EC225 Super Puma', 6);
    
INSERT INTO aircraft (id_company, type, model, number_of_seats)
VALUES 
    (2, 'Airliner', 'Boeing 747', 16),
    (2, 'Airliner', 'Boeing 747', 16),
    (2, 'Airliner', 'Airbus A300', 10),
    (2, 'Airliner', 'Airbus A300', 10),
    (2, 'Helicopter', 'Eurocopter EC225 Super Puma', 6);

INSERT INTO aircraft (id_company, type, model, number_of_seats)
VALUES 
    (3, 'Airliner', 'Boeing 747', 16),
    (3, 'Airliner', 'Boeing 747', 16),
    (3, 'Airliner', 'Airbus A300', 10),
    (3, 'Airliner', 'Airbus A300', 10),
    (3, 'Helicopter', 'Eurocopter EC225 Super Puma', 6);

INSERT INTO aircraft (id_company, type, model, number_of_seats)
VALUES 
    (4, 'Airliner', 'Boeing 747', 16),
    (4, 'Airliner', 'Boeing 747', 16),
    (4, 'Airliner', 'Airbus A300', 10),
    (4, 'Airliner', 'Airbus A300', 10),
    (4, 'Helicopter', 'Eurocopter EC225 Super Puma', 6);
    
INSERT INTO aircraft (id_company, type, model, number_of_seats)
VALUES 
    (5, 'Airliner', 'Boeing 747', 16),
    (5, 'Airliner', 'Boeing 747', 16),
    (5, 'Airliner', 'Airbus A300', 10),
    (5, 'Airliner', 'Airbus A300', 10),
    (5, 'Helicopter', 'Eurocopter EC225 Super Puma', 6);

-- Creating airports

INSERT INTO airport (id, name, location)
VALUES 
    (1, 'alpha', 'alpha'),
    (2, 'beta', 'beta'),
    (3, 'gamma', 'gamma'),
    (4, 'delta', 'delta'),
    (5, 'epsilon', 'epsilon'),
    (6, 'zeta', 'zeta');
    
-- Creating flights

INSERT INTO flight (id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time)
VALUES
    (1, 1, 1, 2, '2024-05-16 10:30:00', '2024-05-16 11:30:00'),
    (2, 2, 1, 3, '2024-07-16 9:30:00',  '2024-07-16 11:30:00'),
    (3, 3, 1, 4, '2024-06-16 11:30:00', '2024-06-16 14:30:00'),
    (4, 4, 1, 5, '2024-06-16 14:30:00', '2024-06-16 18:30:00'),
    (5, 5, 1, 6, '2024-06-16 19:30:00', '2024-06-17 00:30:00');

-- Creating layovers
-- Flight 2

INSERT INTO layover (id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time)
VALUES 
    (2, 1, 2, '2024-06-16 10:30:00', '2024-06-16 11:30:00');

-- Flight 3

INSERT INTO layover (id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time)
VALUES 
    (3, 1, 2, '2024-07-16 12:30:00', '2024-06-16 13:30:00'),
    (3, 2, 3, '2024-07-16 13:30:00', '2024-06-16 14:30:00');

-- Flight 4

INSERT INTO layover (id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time)
VALUES 
    (4, 1, 2, '2024-08-16 15:30:00', '2024-06-16 16:30:00'),
    (4, 2, 3, '2024-08-16 16:30:00', '2024-06-16 17:30:00'),
    (4, 3, 4, '2024-08-16 17:30:00', '2024-06-16 18:30:00');

-- Flight 5

INSERT INTO layover (id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time)
VALUES 
    (5, 1, 2, '2024-09-16 20:30:00', '2024-06-16 21:30:00'),
    (5, 2, 3, '2024-09-16 21:30:00', '2024-06-16 22:30:00'),
    (5, 3, 4, '2024-09-16 22:30:00', '2024-06-16 23:30:00'),
    (5, 4, 5, '2024-09-16 23:30:00', '2024-06-17 00:30:00');
    
-- Creating people 

INSERT INTO passenger (id, name)
VALUES 
    (-1, 'Empty'),
    (1, 'Sahar Patel'),
    (2, 'Miles Yang'),
    (3, 'Amie Duke'),
    (4, 'Sophie Riddle'),
    (5, 'Edward Barker'),
    (6, 'Dennis Hale'),
    (7, 'Inaaya Mata'),
    (8, 'Alejandro Pham'),
    (9, 'Elouise Valentine'),
    (10, 'Karim Key'),
    (11, 'Devon Arnold'),
    (12, 'Filip Kelly'),
    (13, 'Jannat Lam'),
    (14, 'Elysia Skinner'),
    (15, 'Jazmin Shaffer'),
    (16, 'Seamus Gaines'),
    (17, 'Yasmin Gallegos'),
    (18, 'Rita Rivers'),
    (19, 'Jake Woodward'),
    (20, 'Fletcher Knight'),
    (21, 'Lily-May Marks'),
    (22, 'Lowri Pennington'),
    (23, 'Faizan England'),
    (24, 'Seth Pearson'),
    (25, 'Cian Norman');
    
-- Adding seats/people to flights 

INSERT INTO list_passangers_flight (id_flight, id_passenger, is_window_seat, is_right, seat_number)
VALUES 
    (1, 1, 1, 1, 11),
    (1, 2, 0, 1, 12),
    (1, 3, 0, 0, 13),
    (1, 4, 1, 0, 14),
    (1, 5, 1, 1, 21),
    (1, -1, 0, 1, 22),
    (1, -1, 0, 0, 23),
    (1, -1, 1, 0, 24),
    (1, -1, 1, 1, 31),
    (1, -1, 0, 1, 32),
    (1, -1, 0, 0, 33),
    (1, -1, 1, 0, 34),
    (1, 9, 1, 1, 41),
    (1, 8, 0, 1, 42),
    (1, 7, 0, 0, 43),
    (1, 6, 1, 0, 44);
    
INSERT INTO list_passangers_flight (id_flight, id_passenger, is_window_seat, is_right, seat_number)
VALUES 
    (2, 1, 1, 1, 11),
    (2, 2, 0, 1, 12),
    (2, 3, 0, 0, 13),
    (2, 4, 1, 0, 14),
    (2, 5, 1, 1, 21),
    (2, -1, 0, 1, 22),
    (2, -1, 0, 0, 23),
    (2, -1, 1, 0, 24),
    (2, -1, 1, 1, 31),
    (2, -1, 0, 1, 32),
    (2, -1, 0, 0, 33),
    (2, -1, 1, 0, 34),
    (2, 9, 1, 1, 41),
    (2, 8, 0, 1, 42),
    (2, 7, 0, 0, 43),
    (2, 6, 1, 0, 44);
    
INSERT INTO list_passangers_flight (id_flight, id_passenger, is_window_seat, is_right, seat_number)
VALUES 
    (3, 1, 1, 1, 11),
    (3, 2, 0, 1, 12),
    (3, 3, 0, 0, 13),
    (3, 4, 1, 0, 14),
    (3, 5, 1, 1, 21),
    (3, -1, 0, 1, 22),
    (3, -1, 0, 0, 23),
    (3, -1, 1, 0, 24),
    (3, -1, 1, 1, 31),
    (3, -1, 0, 1, 32);
    
INSERT INTO list_passangers_flight (id_flight, id_passenger, is_window_seat, is_right, seat_number)
VALUES 
    (4, 1, 1, 1, 11),
    (4, 2, 0, 1, 12),
    (4, 3, 0, 0, 13),
    (4, 4, 1, 0, 14),
    (4, 5, 1, 1, 21),
    (4, -1, 0, 1, 22),
    (4, -1, 0, 0, 23),
    (4, -1, 1, 0, 24),
    (4, -1, 1, 1, 31),
    (4, -1, 0, 1, 32);
    
INSERT INTO list_passangers_flight (id_flight, id_passenger, is_window_seat, is_right, seat_number)
VALUES 
    (5, 1, 1, 1, 11),
    (5, 2, 0, 1, 12),
    (5, 3, 0, 0, 13),
    (5, 4, 1, 0, 14),
    (5, 5, 1, 1, 21),
    (5, -1, 0, 1, 22),
    (5, -1, 0, 0, 23),
    (5, -1, 1, 0, 24),
    (5, -1, 1, 1, 31),
    (5, -1, 0, 1, 32);

-- Atividade Avaliativa - Transporte Aereo
IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'AirTransport')
BEGIN
    DROP SCHEMA AirTransport;
END
GO

CREATE SCHEMA AirTransport;
GO

CREATE DATABASE AirTransport;
GO

USE AirTransport;
GO

-- Main Tables

CREATE TABLE company
(
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE aircraft
(
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    type VARCHAR(255) NOT NULL,
    model VARCHAR(255) NOT NULL,
    number_of_seats INT NOT NULL,
    id_company INT NOT NULL,
    
    CONSTRAINT fk_aircraft_id_company FOREIGN KEY (id_company) REFERENCES company(id)
);

CREATE TABLE airport
(
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    location VARCHAR(255) NOT NULL
);

CREATE TABLE passenger
(
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE flight
(
    id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_aircraft INT NOT NULL,
    id_origin_airport INT NOT NULL,
    id_destination_airport INT NOT NULL,
    exit_time DATETIME NOT NULL,
    estimated_arrival_time DATETIME NOT NULL,
    
    CONSTRAINT fk_flight_id_aircraft FOREIGN KEY (id_aircraft) REFERENCES aircraft(id),
    CONSTRAINT fk_flight_id_origin_airport FOREIGN KEY (id_origin_airport) REFERENCES airport(id),
    CONSTRAINT fk_flight_id_destination_airport FOREIGN KEY (id_destination_airport) REFERENCES airport(id)
);

CREATE TABLE layover
(
    id_flight INT NOT NULL,
    id_origin_airport INT NOT NULL,
    id_destination_airport INT NOT NULL,
    exit_time DATETIME NOT NULL,
    estimated_arrival_time DATETIME NOT NULL,
    
    PRIMARY KEY (id_flight, id_origin_airport),
    
    CONSTRAINT fk_layover_id_flight FOREIGN KEY (id_flight) REFERENCES flight(id),
    CONSTRAINT fk_layover_id_origin_airport FOREIGN KEY (id_origin_airport) REFERENCES airport(id),
    CONSTRAINT fk_layover_id_destination_airport FOREIGN KEY (id_destination_airport) REFERENCES airport(id)
);

-- Lists 

CREATE TABLE list_passengers_flight
(
    id_flight INT NOT NULL,
    id_passenger INT NULL,
    is_window_seat BIT NOT NULL,
    is_right BIT NOT NULL,
    seat_number INT NOT NULL,
    
    CONSTRAINT fk_list_passengers_flight_id_flight FOREIGN KEY (id_flight) REFERENCES flight(id),
    CONSTRAINT fk_list_passengers_flight_id_passenger FOREIGN KEY (id_passenger) REFERENCES passenger(id)
);
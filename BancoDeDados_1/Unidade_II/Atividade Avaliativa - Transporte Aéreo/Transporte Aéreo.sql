-- Atividade Avaliativa - Transporte Aereo
drop schema if exists AirTransport;

create database AirTransport;
use AirTransport;

-- Main Tables

create table company
(
	id int not null auto_increment primary key,
    name varchar(255) not null
);

create table aircraft
(
	id int not null auto_increment primary key,
    type varchar(255) not null,
    model varchar(255) not null,
    number_of_seats int not null,
    id_company int not null,
    
    constraint fk_aircraft_id_company foreign key (id_company) references company(id)
);

create table airport
(
	id int not null auto_increment primary key,
    name varchar(255) not null,
    location varchar(255) not null
);

create table passenger
(
	id int not null auto_increment primary key,
    name varchar(255) not null
);

create table flight
(
	id int not null auto_increment primary key,
    id_aircraft int not null ,
    id_origin_airport int not null,
    id_destination_airport int not null,
    exit_time datetime not null,
    estimated_arrival_time datetime not null,
    
    constraint fk_flight_id_aircraft foreign key (id_aircraft) references aircraft(id),
    constraint fk_flight_id_origin_airport foreign key (id_origin_airport) references airport(id),
    constraint fk_flight_id_destination_airport foreign key (id_destination_airport) references airport(id)
);

create table layover
(
	id_flight int not null,
    id_origin_airport int not null,
    id_destination_airport int not null,
    exit_time datetime not null,
    estimated_arrival_time datetime not null,
    
    primary key (id_flight, id_origin_airport),
    
    constraint fk_layover_id_flight foreign key (id_flight) references flight(id),
    constraint fk_layover_id_origin_airport foreign key (id_origin_airport) references airport(id),
    constraint fk_layover_id_destination_airport foreign key (id_destination_airport) references airport(id)
);

-- Lists 

create table list_passangers_flight
(
	id_flight int not null,
    id_passenger int null default -1 ,
    is_window_seat bool not null,
    is_right bool not null,
    seat_number int not null,
    
    constraint fk_list_passengers_flight_id_flight foreign key (id_flight) references flight(id),
    constraint fk_list_passengers_flight_id_passenger foreign key (id_passenger) references passenger(id)
);
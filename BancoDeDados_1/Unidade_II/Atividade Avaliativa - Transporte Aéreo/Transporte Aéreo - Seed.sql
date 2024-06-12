use AirTransport;

-- Creating companies

insert into company (  id, name ) values ( 1, "Delta AirLines" );
insert into company (  id, name ) values ( 2, "American AirLines" );
insert into company (  id, name ) values ( 3, "Malasya AirLines" );
insert into company (  id, name ) values ( 4, "Spirit AirLines" );
insert into company (  id, name ) values ( 5, "Ryanair " );

-- Creating aircrafts 

insert into aircraft ( id_company, type, model, number_of_seats) values ( 1, "Airliner", "Boeing 747", 16 );  -- Numero de assentos reduzido para simplificar
insert into aircraft ( id_company, type, model, number_of_seats) values ( 1, "Airliner", "Boeing 747", 16 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 1, "Airliner", "Airbus A300", 10 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 1, "Airliner", "Airbus A300", 10 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 1, "Helicopter", "Eurocopter EC225 Super Puma", 6 );

insert into aircraft ( id_company, type, model, number_of_seats) values ( 2, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 2, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 2, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 2, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 2, "Helicopter", "Eurocopter EC225 Super Puma", 24 );

insert into aircraft ( id_company, type, model, number_of_seats) values ( 3, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 3, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 3, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 3, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 3, "Helicopter", "Eurocopter EC225 Super Puma", 24 );

insert into aircraft ( id_company, type, model, number_of_seats) values ( 4, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 4, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 4, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 4, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 4, "Helicopter", "Eurocopter EC225 Super Puma", 24 );

insert into aircraft ( id_company, type, model, number_of_seats) values ( 5, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 5, "Airliner", "Boeing 747", 366 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 5, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 5, "Airliner", "Airbus A300", 247 );
insert into aircraft ( id_company, type, model, number_of_seats) values ( 5, "Helicopter", "Eurocopter EC225 Super Puma", 24 );

-- Creating airports

insert into airport ( id, name, location ) values ( 1, "alpha", "alpha" );
insert into airport ( id, name, location ) values ( 2, "beta", "beta" );
insert into airport ( id, name, location ) values ( 3, "gamma", "gamma" );
insert into airport ( id, name, location ) values ( 4, "delta", "delta" );
insert into airport ( id, name, location ) values ( 5, "epsilon", "epsilon" );
insert into airport ( id, name, location ) values ( 6, "zeta", "zeta" );
insert into airport ( id, name, location ) values ( 7, "eta", "eta" );
insert into airport ( id, name, location ) values ( 8, "theta", "theta" );
insert into airport ( id, name, location ) values ( 9, "iota", "iota" );
insert into airport ( id, name, location ) values ( 10, "kappa", "kappa" );


-- Creating flights

insert into flight ( id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) 
values ( 1, 1, 1, 2, '2024-05-16 10:30:00', '2024-05-16 11:30:00');

insert into flight ( id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) 
values ( 2, 2, 1, 3, '2024-07-16 9:30:00', '2024-07-16 11:30:00');

insert into flight ( id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) 
values ( 3, 3, 1, 4, '2024-06-16 11:30:00', '2024-06-16 14:30:00');

insert into flight ( id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) 
values ( 4, 4, 1, 5, '2024-06-16 14:30:00', '2024-06-16 18:30:00');

insert into flight ( id, id_aircraft, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) 
values ( 5, 5, 1, 6, '2024-06-16 19:30:00', '2024-06-17 00:30:00');

-- Creating layovers

insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 2, 1, 2, '2024-06-16 10:30:00', '2024-06-16 11:30:00');

insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 3, 1, 2, '2024-07-16 12:30:00', '2024-06-16 13:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 3, 2, 3, '2024-07-16 13:30:00', '2024-06-16 14:30:00');

insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 4, 1, 2, '2024-08-16 15:30:00', '2024-06-16 16:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 4, 2, 3, '2024-08-16 16:30:00', '2024-06-16 17:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 4, 3, 4, '2024-08-16 17:30:00', '2024-06-16 18:30:00');

insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 5, 1, 2, '2024-09-16 20:30:00', '2024-06-16 21:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 5, 2, 3, '2024-09-16 21:30:00', '2024-06-16 22:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 5, 3, 4, '2024-09-16 22:30:00', '2024-06-16 23:30:00');
insert into layover ( id_flight, id_origin_airport, id_destination_airport, exit_time, estimated_arrival_time ) values ( 5, 4, 5, '2024-09-16 23:30:00', '2024-06-17 00:30:00');

-- Creating people 

insert into passenger ( id, name ) values ( -1, "Empty" );
insert into passenger ( id, name ) values ( 1, "Sahar Patel" );
insert into passenger ( id, name ) values ( 2, "Miles Yang" );
insert into passenger ( id, name ) values ( 3, "Amie Duke" );
insert into passenger ( id, name ) values ( 4, "Sophie Riddle" );
insert into passenger ( id, name ) values ( 5, "Edward Barker" );
insert into passenger ( id, name ) values ( 6, "Dennis Hale" );
insert into passenger ( id, name ) values ( 7, "Inaaya Mata" );
insert into passenger ( id, name ) values ( 8, "Alejandro Pham" );
insert into passenger ( id, name ) values ( 9, "Elouise Valentine" );
insert into passenger ( id, name ) values ( 10, "Karim Key" );
insert into passenger ( id, name ) values ( 11, "Devon Arnold" );
insert into passenger ( id, name ) values ( 12, "Filip Kelly" );
insert into passenger ( id, name ) values ( 13, "Jannat Lam" );
insert into passenger ( id, name ) values ( 14, "Elysia Skinner" );
insert into passenger ( id, name ) values ( 15, "Jazmin Shaffer" );
insert into passenger ( id, name ) values ( 16, "Seamus Gaines" );
insert into passenger ( id, name ) values ( 17, "Yasmin Gallegos" );
insert into passenger ( id, name ) values ( 18, "Rita Rivers" );
insert into passenger ( id, name ) values ( 19, "Jake Woodward" );
insert into passenger ( id, name ) values ( 20, "Fletcher Knight" );
insert into passenger ( id, name ) values ( 21, "Lily-May Marks" );
insert into passenger ( id, name ) values ( 22, "Lowri Pennington" );
insert into passenger ( id, name ) values ( 23, "Faizan England" );
insert into passenger ( id, name ) values ( 24, "Seth Pearson" );
insert into passenger ( id, name ) values ( 25, "Cian Norman" );

-- Adding seats/people to flights 

insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 1, 1, 1, 11);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 2, 0, 1, 12);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 3, 0, 0, 13);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 4, 1, 0, 14);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 5, 1, 1, 21);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 0, 1, 22);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 0, 0, 23);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 1, 0, 24);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 1, 1, 31);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 0, 1, 32);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 0, 0, 33);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, -1, 1, 0, 34);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 9, 1, 1, 41);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 8, 0, 1, 42);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 7, 0, 0, 43);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 1, 6, 1, 0, 44);

insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 1, 1, 1, 11);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 2, 0, 1, 12);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 3, 0, 0, 13);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 4, 1, 0, 14);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 5, 1, 1, 21);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 0, 1, 22);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 0, 0, 23);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 1, 0, 24);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 1, 1, 31);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 0, 1, 32);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 0, 0, 33);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, -1, 1, 0, 34);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 9, 1, 1, 41);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 8, 0, 1, 42);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 7, 0, 0, 43);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 6, 1, 0, 44);

insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, 1, 1, 1, 11);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, 2, 0, 1, 12);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, 3, 0, 0, 13);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, 4, 1, 0, 14);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, 5, 1, 1, 21);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, -1, 0, 1, 22);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, -1, 0, 0, 23);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, -1, 1, 0, 24);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, -1, 1, 1, 31);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 3, -1, 0, 1, 32);

insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, 1, 1, 1, 11);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, 2, 0, 1, 12);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, 3, 0, 0, 13);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, 4, 1, 0, 14);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, 5, 1, 1, 21);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, -1, 0, 1, 22);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, -1, 0, 0, 23);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, -1, 1, 0, 24);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, -1, 1, 1, 31);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 4, -1, 0, 1, 32);

insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 1, 1, 1, 11);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 2, 0, 1, 12);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 3, 0, 0, 13);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 4, 1, 0, 14);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 5, 1, 1, 21);
insert into list_passangers_flight ( id_flight, id_passenger, is_window_seat, is_right, seat_number) values ( 2, 6, 0, 1, 22);

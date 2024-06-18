use AirTransport;

-- Listagem de Aeronaves ordenadas por tipo;

select 
	aircraft.id as ID,
    aircraft.type as Tipo,
    aircraft.model as Modelo,
    aircraft.number_of_seats as 'Num Assentos',
    company.name as Empresa
from 
	aircraft,
    company
where
	aircraft.id_company = company.id
order by 
	type, 
    model 
asc;

-- Relatório de Vôos das Aeronaves por período 

select 
	flight.id as "ID Voo",
    aircraft.id as "ID Aeronave",
    aircraft.model as 'Aeronave',
    airport.name as 'Aeroporto Origem',
    destinyAirport.name as 'Aeroporto Destino',
    flight.exit_time as "Horario de Saída",
    flight.estimated_arrival_time as "Horario Estimado de Chegada"
from 
	flight,
    aircraft,
    airport,
    (select airport.id, airport.name from airport) as destinyAirport
where
	aircraft.id = flight.id_aircraft
	and flight.id_destination_airport = destinyAirport.id
    and flight.id_origin_airport = airport.id
order by 
	exit_time
asc;

-- Listagem de vôos que fazem escala em um determinado local;

select
	flight.id as 'ID Voo',
    aircraft.model as Aeronave,
    airport.name as 'Aeroporto Origem',
    destinyAirport.name as 'Aeroporto Destino',
    layover.exit_time as'Horário Estimado de Saída',
    layover.estimated_arrival_time as'Horário Estimado de Chegada'
from
	airport, layover, flight, aircraft,
    (select airport.id, airport.name from airport) as destinyAirport
where
	layover.id_origin_airport = airport.id 
    and layover.id_destination_airport = destinyAirport.id
    and layover.id_flight = flight.id 
    and aircraft.id = flight.id_aircraft
    -- and layover.id_origin_airport = 1              -- Nesse caso o aeroporto de id = 1 (alpha)
order by 
	layover.exit_time 
asc;

-- Exibição de poltronas disponíveis em um determinado vôo/avião;

select 
    id_flight as Voo, 
    if (id_passenger = -1, 'Sim', 'Não' ) as "Disponivel",
    seat_number as "Numero Assento",
    if (is_right = 1, 'Sim', 'Não' ) as "Lado Direito",
    if (is_window_seat = 1, 'Sim', 'Não' )  as "Janela"
from 
	list_passangers_flight 
where 
	id_flight = 4      -- Nesse caso o voo 4
    and id_passenger = -1
order by 
	id_flight, seat_number 
asc;


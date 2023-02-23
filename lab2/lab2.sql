-- PostgreSql version 15.2

--create table for import csv
DROP TABLE IF EXISTS station_data;
CREATE TABLE IF NOT EXISTS station_data 
(
	id SERIAL PRIMARY KEY,
	route VARCHAR(50) NOT NULL,
	bus_stop_range FLOAT NOT NULL,
	bus_stop_name VARCHAR(50) DEFAULT 'Без названия',
	bus_stop_direction VARCHAR(10) NOT NULL,
	is_have_pavilion VARCHAR(10) DEFAULT 'Нет'
);

--import data from .csv file
SET client_encoding = UTF8;
COPY station_data(route, bus_stop_range, bus_stop_name, bus_stop_direction, is_have_pavilion) 
FROM 'E:\Projects\Databases\lab2\station_v4.csv'
DELIMITER ';'
CSV HEADER;

--create tables
DROP TABLE IF EXISTS stop_on_the_road, road, locality_name, bus_stop, placement_along_the_road;

CREATE TABLE IF NOT EXISTS locality_name
(
	id SERIAL PRIMARY KEY,
	locality_name VARCHAR(50) UNIQUE NOT NULL	
);
CREATE TABLE IF NOT EXISTS road
(
	id SERIAL PRIMARY KEY,
	start_point INTEGER NOT NULL,
	finish_point INTEGER NOT NULL,
	FOREIGN KEY(start_point) REFERENCES locality_name (id),
	FOREIGN KEY(finish_point) REFERENCES locality_name (id)
);
CREATE TABLE IF NOT EXISTS bus_stop
(
	id SERIAL PRIMARY KEY,
	is_have_pavilion BOOLEAN DEFAULT false,
	bus_stop_name VARCHAR(50) DEFAULT 'Без названия'	
);
CREATE TABLE IF NOT EXISTS placement_along_the_road
(
	id SERIAL PRIMARY KEY,
	placement_along_the_road VARCHAR(10) UNIQUE
);
CREATE TABLE IF NOT EXISTS stop_on_the_road
(
	id SERIAL PRIMARY KEY,
	bus_stop_id INTEGER NOT NULL,
	range_from_start FLOAT NOT NULL,
	placement_along_the_road_id INTEGER,
	road_id INTEGER,
	FOREIGN KEY(bus_stop_id) REFERENCES bus_stop (id),
	FOREIGN KEY(placement_along_the_road_id) REFERENCES placement_along_the_road (id),
	FOREIGN KEY(road_id) REFERENCES road (id)
);

-- insert data for new tables
INSERT INTO placement_along_the_road(placement_along_the_road)
SELECT DISTINCT 
  bus_stop_direction 
FROM station_data;

INSERT INTO locality_name(locality_name)
SELECT DISTINCT 
	split_part(station_data.route, '-', 1) 
FROM station_data;	
INSERT INTO locality_name(locality_name)
SELECT DISTINCT 
	split_part(station_data.route, '-', -1) 
FROM station_data
ON CONFLICT (locality_name)
 	DO NOTHING;

INSERT INTO bus_stop(bus_stop_name, is_have_pavilion)
SELECT
	CASE WHEN bus_stop_name IS NULL THEN 'Не указано' ELSE bus_stop_name END,
	CASE WHEN is_have_pavilion = 'Есть' THEN true ELSE false END
FROM station_data;

INSERT INTO road(start_point, finish_point) 
SELECT lnm_sp.id, lnm_fp.id
FROM station_data AS sd
    INNER JOIN locality_name AS lnm_sp ON lnm_sp.locality_name = split_part(sd.route, '-', 1) 
    INNER JOIN locality_name AS lnm_fp ON lnm_fp.locality_name = split_part(sd.route, '-', -1);

INSERT INTO stop_on_the_road(bus_stop_id, range_from_start, placement_along_the_road_id, road_id)
SELECT bs.id
FROM station_data AS sd
	INNER JOIN bus_stop AS bs ON bs.bus_stop_name = sd.bus_stop_name AND 

	

	
	
		














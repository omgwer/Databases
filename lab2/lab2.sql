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
FROM 'E:\Projects\Databases\lab2\station.csv'
DELIMITER ';'
CSV HEADER;

--create tables
DROP TABLE IF EXISTS stop_on_the_road, road, locality_name, placement_along_the_road;

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
CREATE TABLE IF NOT EXISTS placement_along_the_road
(
	id SERIAL PRIMARY KEY,
	placement_along_the_road VARCHAR(10) UNIQUE
);
CREATE TABLE IF NOT EXISTS stop_on_the_road
(
	id SERIAL PRIMARY KEY,
	is_have_pavilion VARCHAR(10) DEFAULT 'Не указано',
	bus_stop_name VARCHAR(50) DEFAULT 'Без названия',	
	range_from_start NUMERIC(7,3) NOT NULL,
	placement_along_the_road_id INTEGER,
	road_id INTEGER,
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
	BTRIM(split_part(station_data.route, '-', 1))
FROM station_data;	
INSERT INTO locality_name(locality_name)
SELECT DISTINCT 
	BTRIM(split_part(station_data.route, '-', -1))
FROM station_data
ON CONFLICT (locality_name)
 	DO NOTHING;

INSERT INTO road(start_point, finish_point) 
SELECT DISTINCT lnm_sp.id, lnm_fp.id
FROM station_data AS sd
    INNER JOIN locality_name AS lnm_sp ON lnm_sp.locality_name = BTRIM(split_part(sd.route, '-', 1))
    INNER JOIN locality_name AS lnm_fp ON lnm_fp.locality_name = BTRIM(split_part(sd.route, '-', -1));

INSERT INTO stop_on_the_road(is_have_pavilion, bus_stop_name, range_from_start, placement_along_the_road_id, road_id)
SELECT DISTINCT 
		sd.is_have_pavilion, 
		CASE WHEN sd.bus_stop_name IS NULL THEN 'Не указано' ELSE sd.bus_stop_name END, 
		sd.bus_stop_range, 
		pl_r.id, 
		r.id
FROM station_data AS sd
	INNER JOIN placement_along_the_road AS pl_r ON sd.bus_stop_direction = pl_r.placement_along_the_road
	INNER JOIN locality_name AS lnm_sp ON lnm_sp.locality_name = BTRIM(split_part(sd.route, '-', 1)) 
    INNER JOIN locality_name AS lnm_fp ON lnm_fp.locality_name = BTRIM(split_part(sd.route, '-', -1))
	INNER JOIN road AS r ON r.start_point = lnm_sp.id AND r.finish_point = lnm_fp.id;

-- search data scripts
-- Выбрать все остановки с павильоном
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
WHERE
	st_r.is_have_pavilion = 'Есть';	
-- Выбрать все остановки слева от дороги “Звенигово - Шелангер - Морки”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
WHERE
	ln_sp.locality_name = 'Звенигово' AND ln_fp.locality_name = 'Морки'
	AND pl_r.placement_along_the_road = 'Слева';
-- Выбрать все остановки по названию “Дачи”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
WHERE
	st_r.bus_stop_name = 'Дачи';
--Выбрать все остановки в интервале от 20 до 80 километров включительно на дороге “Йошкар-Ола - Уржум”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
WHERE
	ln_sp.locality_name = 'Йошкар–Ола' AND ln_fp.locality_name = 'Уржум' AND
	st_r.range_from_start > 20.0 AND st_r.range_from_start <= 80.0;
--найти минимальное расстояние между остановками на различных автомобильных дорогах с условием, 
--что перемещение между дорогами происходит в конечных пунктах (например, в населённых пунктах “Звенигово” и “Морки” на 
--дороге “Звенигово - Шелангер - Морки”).
WITH first_road AS 
(
	SELECT 
		ln_sp.locality_name AS start_point, 
		ln_fp.locality_name AS finish_point,
		st_r.bus_stop_name,
		st_r.range_from_start
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
		INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point	
), second_road AS 
(
	SELECT 
		ln_sp.locality_name AS start_point, 
		ln_fp.locality_name AS finish_point,
		st_r.bus_stop_name,
		st_r.range_from_start
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
		INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
),
max_range AS (
	SELECT DISTINCT		
		ln_sp.locality_name AS start_point, 
		ln_fp.locality_name AS finish_point,
		MAX(st_r.range_from_start) AS max_range
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point
		INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.finish_point
	GROUP BY ln_sp.locality_name, ln_fp.locality_name	
)
SELECT
	fr.bus_stop_name,
	sr.bus_stop_name,
	round(mr.max_range - fr.range_from_start + sr.range_from_start, 3)	
FROM first_road AS fr
	INNER JOIN second_road AS sr ON fr.finish_point = sr.start_point
	INNER JOIN max_range AS mr ON fr.start_point = mr.start_point AND fr.finish_point = mr.finish_point;
	
	
















-- Выбрать в алфавитном порядке всех актёров, снимавшихся в фильмах без возрастных ограничений (рейтинг ‘G’)
SELECT DISTINCT first_name, last_name FROM actor AS ac
INNER JOIN film_actor AS fa ON ac.actor_id = fa.actor_id
INNER JOIN (SELECT * FROM film WHERE film.rating = 'G') AS fl ON fl.film_id = fa.film_id
ORDER BY first_name;
-- run 40msec

-- CREATE INDEX idx_film_rating ON film (rating);
SELECT DISTINCT actor.first_name, actor.last_name FROM actor
JOIN film_actor ON actor.actor_id = film_actor.actor_id
JOIN film ON film_actor.film_id = film.film_id
WHERE film.rating = 'G'
ORDER BY actor.last_name, actor.first_name;
--run 37msec

--CREATE INDEX idx_film_actor_actor_id ON film_actor (actor_id);
--CREATE INDEX idx_actor_last_first_name ON actor (last_name, first_name);
SELECT actor.first_name, actor.last_name
FROM actor
INNER JOIN film_actor ON actor.actor_id = film_actor.actor_id
INNER JOIN film ON film_actor.film_id = film.film_id
WHERE film.rating = 'G'
GROUP BY actor.actor_id
ORDER BY actor.last_name, actor.first_name;
-- run 38 msec
--Выбрать всех актёров с фамилией “HOPKINS”, снимавшихся более чем в 10 в фильмах с рейтингом ‘G’ или ‘PG’

--CREATE INDEX idx_film_actor_actor_id ON film_actor (actor_id);
CREATE INDEX idx_actor_last_first_name ON actor (last_name, first_name);
-- CREATE INDEX idx_film_actor_film_id ON film_actor (film_id);
-- CREATE INDEX idx_film_rating ON film (rating);
--EXPLAIN ANALYZE
SELECT actor.first_name, actor.last_name, COUNT(DISTINCT film.film_id) AS films_count
FROM actor
INNER JOIN film_actor ON actor.actor_id = film_actor.actor_id
INNER JOIN film ON film_actor.film_id = film.film_id
WHERE actor.last_name = 'HOPKINS'
  AND film.rating IN ('G', 'PG')
GROUP BY actor.actor_id
HAVING COUNT(DISTINCT film.film_id) > 10
ORDER BY films_count DESC;
-- run 38msec

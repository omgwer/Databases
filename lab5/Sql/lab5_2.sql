-- Выбрать всех актёров, у которых фамилия совпадает "ALLEN” и/или имя совпадает с “CUBA”
-- Выбрать всех актеров с именем CUBA
CREATE INDEX idx_actor_first_name ON actor (first_name, last_name);  -- ADD UNION 9.

EXPLAIN ANALYZE
SELECT first_name, last_name FROM actor
WHERE last_name = 'ALLEN' OR first_name = 'CUBA';

EXPLAIN ANALYZE
SELECT first_name, last_name FROM actor AS ac
WHERE ac.last_name = 'ALLEN'
UNION
SELECT first_name, last_name FROM actor AS ac 
WHERE ac.first_name = 'CUBA';

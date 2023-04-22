--Выбрать 10 фильмов, принёсших наибольшую выручку в прокате
--Примечание: выручка должна вычисляться по фактическим платежам в таблице payment

CREATE INDEX idx_film_id ON film USING btree (film_id);
CREATE INDEX idx_film_id ON inventory USING btree (film_id);
CREATE INDEX idx_rental_id ON payment USING btree (rental_id);

SELECT fl.title, SUM(pm.amount) AS revenue FROM film AS fl
INNER JOIN inventory AS inv ON inv.film_id = fl.film_id
INNER JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
INNER JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY fl.title
ORDER BY revenue DESC 
LIMIT 10;
--run 44ms
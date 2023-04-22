-- Выбрать общую выручку по различным категориям фильмов в порядке убывания размера выручки.
-- Примечание: выручка должна вычисляться по фактическим платежам в таблице payment

-- CREATE INDEX idx_film_id ON film USING btree (film_id);
-- CREATE INDEX idx_film_id ON inventory USING btree (film_id);
-- CREATE INDEX idx_rental_id ON payment USING btree (rental_id);

SELECT ctg.name, SUM(pm.amount) AS revenue FROM film AS fl
INNER JOIN film_category as fc ON fc.film_id = fl.film_id
INNER JOIN category as ctg ON fc.category_id = ctg.category_id
INNER JOIN inventory AS inv ON inv.film_id = fl.film_id
INNER JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
INNER JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY ctg.name
ORDER BY revenue DESC;
--run 44ms
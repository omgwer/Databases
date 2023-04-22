-- Выбрать все фильмы категории “Horror” и выручку по ним (включая фильмы с нулевой выручкой) в порядке убывания выручки
-- Примечание: выручка должна вычисляться по фактическим платежам в таблице payment

-- CREATE INDEX idx_film_id ON film USING btree (film_id);
-- CREATE INDEX idx_film_id ON inventory USING btree (film_id);
-- CREATE INDEX idx_rental_id ON payment USING btree (rental_id);

SELECT fl.title, SUM(pm.amount) AS revenue FROM film AS fl
INNER JOIN film_category as fc ON fc.film_id = (SELECT ctg.category_id FROM category AS ctg WHERE name = 'Horror')
INNER JOIN inventory AS inv ON inv.film_id = fl.film_id
INNER JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
INNER JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY fl.title
ORDER BY revenue DESC;
--run 44ms  кривое, не ищет.

SELECT fl.title, ctg.name, SUM(pm.amount) AS revenue FROM film AS fl
INNER JOIN film_category AS fc ON  fc.film_id = fl.film_id
INNER JOIN category AS ctg on ctg.category_id = fc.category_id AND ctg.name = 'Horror'
INNER JOIN inventory AS inv ON inv.film_id = fl.film_id
INNER JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
INNER JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY fl.title, ctg.name
ORDER BY revenue DESC;
-- 38msec  53row (нужно 56)

SELECT fl.title, ctg.name, SUM(pm.amount) AS revenue FROM film AS fl
INNER JOIN film_category AS fc ON  fc.film_id = fl.film_id
INNER JOIN category AS ctg on ctg.category_id = fc.category_id AND ctg.name = 'Horror'
LEFT JOIN inventory AS inv ON inv.film_id = fl.film_id
LEFT JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
LEFT JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY fl.title, ctg.name
ORDER BY revenue DESC;
-- 38msec 56 row WITH NULL

SELECT fl.title,SUM(COALESCE(pm.amount,0)) AS revenue FROM film AS fl
INNER JOIN film_category AS fc ON  fc.film_id = fl.film_id
INNER JOIN category AS ctg on ctg.category_id = fc.category_id AND ctg.name = 'Horror'
LEFT JOIN inventory AS inv ON inv.film_id = fl.film_id
LEFT JOIN rental AS rnt ON inv.inventory_id = rnt.inventory_id
LEFT JOIN payment AS pm ON pm.rental_id = rnt.rental_id
GROUP BY fl.title
ORDER BY revenue;
-- 38msec 56 row WITHOUNT NULL

-- кол-во ужастиков
SELECT fl.title, ctg.name FROM film AS fl
INNER JOIN film_category AS fc ON  fc.film_id = fl.film_id
INNER JOIN category AS ctg on ctg.category_id = fc.category_id AND ctg.name = 'Horror'





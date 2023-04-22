-- Выбрать всех актёров, у которых фамилия совпадает "ALLEN” и/или имя совпадает с “CUBA”
-- Выбрать всех актеров с именем CUBA

SELECT first_name, last_name FROM actor
WHERE last_name = 'ALLEN' OR (last_name = 'ALLEN' AND first_name = 'CUBA');

-- нет смысла выбирать фамилию
-- Выбрать всех актеров с именем CUBA
SELECT first_name, last_name FROM actor
WHERE first_name = 'CUBA';
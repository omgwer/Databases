-- db tables structure
SELECT * FROM information_schema.tables 
WHERE table_schema = 'public'
order by information_schema.tables.table_name
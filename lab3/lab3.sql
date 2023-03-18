-- PostgreSql version 15.2
-- ips_labs_3

DROP TABLE IF EXISTS course_enrollment, course_module, course, course_status, course_module_status;

-- now() - можно использовать для генерации времени
CREATE TABLE IF NOT EXISTS course
(
	course_id VARCHAR(36) PRIMARY KEY,
	version SERIAL,
	create_at TIMESTAMP NOT NULL DEFAULT now(),
	updated_at TIMESTAMP DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS course_status
(
	enrollment_id VARCHAR(36) PRIMARY KEY,
	progress NUMERIC(3, 0),
	duration INT
);

CREATE TABLE IF NOT EXISTS course_module
(
	module_id VARCHAR(36),	
	course_id VARCHAR(36),
	is_required VARCHAR(5),
	create_at TIMESTAMP NOT NULL DEFAULT now(),
	updated_at TIMESTAMP DEFAULT NULL,
	deleted_at TIMESTAMP DEFAULT NULL,
	PRIMARY KEY (module_id),
	FOREIGN KEY (course_id) REFERENCES course(course_id) ON UPDATE CASCADE ON DELETE CASCADE
	
);

CREATE TABLE IF NOT EXISTS course_module_status
(
	enrollment_id VARCHAR(36),
	module_id VARCHAR(36),
	progress NUMERIC(3, 0),
	duration INT,
	PRIMARY KEY (enrollment_id, module_id),
	FOREIGN KEY (enrollment_id) REFERENCES course_status(enrollment_id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (module_id) REFERENCES course_module(module_id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS course_enrollment
(
	enrollment_id VARCHAR(36),
	course_id VARCHAR(36),
	FOREIGN KEY(course_id) REFERENCES course(course_id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY(enrollment_id) REFERENCES course_status(enrollment_id) ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY(enrollment_id)	
);

--добавление значений
INSERT INTO course VALUES 
('test1', 1), ('testr2', 1);

INSERT INTO course_module VALUES
('testModuleId1', 'test1', 'false');


SELECT crs.course_id, crs.create_at, crs.updated_at, c_m.course_id, c_m.is_required
FROM course AS crs
INNER JOIN course_module AS c_m ON crs.course_id = c_m.course_id; 


INSERT INTO course (course_id)
SELECT crs.course_id
FROM course AS crs 
WHERE crs.course_id = 'test1'
ON CONFLICT (course_id) 
DO UPDATE SET
	course_id = 'testkke'
	
	
INSERT INTO course_status VALUES( @enrollmentId , @progress , @duration );



SELECT * FROM course;
select * from course_module;
SELECT * FROM course_enrollment;
SELECT * FROM course_status;

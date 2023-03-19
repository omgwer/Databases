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
	duration INT,
	deleted_at TIMESTAMP DEFAULT NULL	
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
	FOREIGN KEY (course_id) REFERENCES course(course_id) ON UPDATE CASCADE --ON DELETE CASCADE	
);

CREATE TABLE IF NOT EXISTS course_module_status
(
	enrollment_id VARCHAR(36),
	module_id VARCHAR(36),
	progress NUMERIC(3, 0),
	duration INT,
	deleted_at TIMESTAMP DEFAULT NULL,
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
	

INSERT INTO course_status (enrollment_id, progress, duration) VALUES ('test2', 5 , 5)
ON CONFLICT (enrollment_id)
DO UPDATE SET
	progress = 15, duration = 10;

INSERT INTO course_module_status (enrollment_id, module_id, progress, duration) VALUES ('test5', 'test1', 4, 5)
ON CONFLICT (enrollment_id, module_id)
DO UPDATE SET
	progress = CASE WHEN cm.is_required == false THEN 13 ELSE 100 END,
	--progress = 25, 
	duration = course_module_status.duration + 5;
SELECT * FROM course_module_status;

WITH course_module AS
(
	SELECT module_id, is_required
	FROM course_module
	WHERE module_id = 'test1'
)
UPDATE course_module_status AS cms 
SET
	progress = CASE WHEN cm.is_required = 'false' THEN 100 ELSE progress END
FROM course_module AS cm WHERE cm.module_id = cms.module_id;

SELECT * FROM course_module_status;

UPDATE course_module SET
	is_required = 'true'
WHERE
	module_id = 'test1';

insert into course_module VALUES ('moduleId2', 'testkek', 'true');

UPDATE course_status
SET
	deleted_at = NULL
FROM
	course_status AS cs 
	INNER JOIN course_enrollment AS ce ON cs.enrollment_id = ce.enrollment_id 	
WHERE course_status.enrollment_id = cs.enrollment_id AND ce.course_id = 'course_id_2';

UPDATE course_module_status
SET
	deleted_at = now()
FROM
	course_module_status AS cms 
	INNER JOIN course_enrollment AS ce ON cms.enrollment_id = ce.enrollment_id 	
WHERE course_module_status.enrollment_id = ce.enrollment_id AND ce.course_id = 'course_id_2';

UPDATE course_module
SET
	deleted_at = now()
WHERE course_id = 'course_id_2';


DELETE FROM course_enrollment
WHERE course_id = 'course_id_1';

	
INSERT INTO course VALUES('course_id_1'), ('course_id_2');
INSERT INTO course_status VALUES ('enrollment_id_1', 0, 0), ('enrollment_id_2', 0, 0);
INSERT INTO course_enrollment VALUES ('enrollment_id_1', 'course_id_1'),('enrollment_id_2', 'course_id_2');
INSERT INTO course_module VALUES 
	 ('module_id_1', 'course_id_1', 'false'), ('module_id_2', 'course_id_1', 'true'),
	 ('module_id_3', 'course_id_2', 'false'), ('module_id_4', 'course_id_2', 'true');
INSERT INTO course_module_status VALUES
	('enrollment_id_1', 'module_id_1', 0,0), ('enrollment_id_1', 'module_id_2', 0,0),
	('enrollment_id_2', 'module_id_3', 0,0), ('enrollment_id_2', 'module_id_4', 0,0);
	
TRUNCATE course, course_status, course_enrollment, course_module, course_module_status;

SELECT * FROM course;
SELECT * FROM course_status; --soft
SELECT * FROM course_enrollment;
select * from course_module; --soft
SELECT * FROM course_module_status; --soft



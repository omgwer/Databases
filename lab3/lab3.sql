-- PostgreSql version 15.2
-- ips_labs_3

DROP TABLE IF EXISTS course_enrollment, course_module, course, course_status, course_module_status;

-- now() - можно использовать для генерации времени
CREATE TABLE IF NOT EXISTS course
(
	course_id VARCHAR(36) PRIMARY KEY,
	create_at TIMESTAMP,
	updated_at TIMESTAMP 
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
	is_required BOOLEAN,
	create_at TIMESTAMP,
	updated_at TIMESTAMP,
	FOREIGN KEY (course_id) REFERENCES course(course_id),	
	PRIMARY KEY (module_id)
);

CREATE TABLE IF NOT EXISTS course_module_status
(
	enrollment_id VARCHAR(36),
	module_id VARCHAR(36),
	progress NUMERIC(3, 0),
	duration INT,
	PRIMARY KEY (enrollment_id, module_id),
	FOREIGN KEY (enrollment_id) REFERENCES course_status(enrollment_id),
	FOREIGN KEY (module_id) REFERENCES course_module(module_id)
);

CREATE TABLE IF NOT EXISTS course_enrollment
(
	enrollment_id VARCHAR(36),
	course_id VARCHAR(36),
	FOREIGN KEY(course_id) REFERENCES course(course_id),
	FOREIGN KEY(enrollment_id) REFERENCES course_status(enrollment_id),
	PRIMARY KEY(enrollment_id)	
);


USE amazenaf;

-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

-- CREATE TABLE jobs
-- (
--     id INT AUTO_INCREMENT NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId) 
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- )

-- CREATE TABLE contractors 
-- (
--     id INT AUTO_INCREMENT NOT NULL,
--     company VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- )

-- TRUNCATE TABLE contractors

-- CREATE TABLE jobcontractors
-- (
--     id INT AUTO_INCREMENT NOT NULL,
--     jobId INT NOT NULL,
--     contractorId INT NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (jobId)
--     REFERENCES jobs (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (contractorId)
--     REFERENCES contractors (id)
--     ON DELETE CASCADE
-- )
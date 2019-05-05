CREATE TABLE IF NOT EXISTS users
(
  user_id SERIAL NOT NULL,
  username varchar(20) UNIQUE NOT NULL,
  email VARCHAR (256) UNIQUE NOT NULL,
  salt CHAR(32) NOT NULL,
  password VARCHAR(64) NOT NULL,
  created_on TIMESTAMP(6) NOT NULL,
  last_login TIMESTAMP(6),
  PRIMARY KEY (user_id)
);


CREATE TABLE IF NOT EXISTS reviews
(
  review_id SERIAL NOT NULL,
  user_id integer NOT NULL,
  number_plate VARCHAR(6) NOT NULL,
  review_type SMALLINT NOT NULL,
  review_text VARCHAR(256) NOT NULL,
  created_on TIMESTAMP(6) NOT NULL,
  PRIMARY KEY (review_id),
  CONSTRAINT user_id_is_user_id_from_users FOREIGN KEY (user_id)
      REFERENCES users (user_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);
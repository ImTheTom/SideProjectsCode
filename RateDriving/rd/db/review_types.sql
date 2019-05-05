CREATE TABLE IF NOT EXISTS review_type
(
  review_type_id integer NOT NULL,
  review_type_string VARCHAR(30) NOT NULL,
  PRIMARY KEY (review_type_id)
);

INSERT INTO review_type (review_type_id, review_type_string) values(0, 'driving');
INSERT INTO review_type (review_type_id, review_type_string) values(5, 'parking');
INSERT INTO review_type (review_type_id, review_type_string) values(10, 'car');
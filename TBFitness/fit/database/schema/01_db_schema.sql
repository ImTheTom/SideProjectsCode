CREATE TABLE IF NOT EXISTS lift
(
	lift_id SERIAL PRIMARY KEY,
	lift_user_id INT,
	lift_name TEXT NOT NULL,
	lift_weight REAL,
	lift_reps INT,
	lift_sets INT
);

CREATE TABLE IF NOT EXISTS workout
(
	workout_id SERIAL PRIMARY KEY,
	workout_user_id INT,
	workout_name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS lifts
(
	lift_id INT PRIMARY KEY REFERENCES lift(lift_id) ON DELETE CASCADE,
	workout_id INT REFERENCES workout(workout_id) ON DELETE CASCADE
);
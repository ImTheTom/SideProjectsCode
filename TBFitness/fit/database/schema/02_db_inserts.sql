INSERT INTO lift(lift_id, lift_user_id, lift_name, lift_weight, lift_reps, lift_sets)
VALUES
(1, 1, 'Bench Press', 15, 5, 4),
(2, 1, 'OverHead Press', 22.5, 10, 3),
(3, 1, 'Dumbell Incline', 15, 10, 10),
(4, 1, 'Tricep Pulldown', 18, 10, 3);

INSERT INTO workout(workout_id, workout_user_id, workout_name)
VALUES
(1, 1, 'Push Day'),
(2, 1, 'Pull Day');

INSERT INTO lifts(lift_id, workout_id)
VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 2);

ALTER SEQUENCE lift_lift_id_seq RESTART WITH 1000;
ALTER SEQUENCE workout_workout_id_seq RESTART WITH 1000;

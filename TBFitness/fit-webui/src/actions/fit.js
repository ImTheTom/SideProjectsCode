export const lifts = (data) => ({
	type: "LIFTS",
	payload: data
});

export const workout = (data) => ({
	type: "WORKOUT",
	payload: data
});

export const workoutEdit = (data) => ({
	type: "WORKOUT_EDIT",
	payload: data
});

export const workout_edit_lift = (workoutIndex, liftIndex, type) => ({
	type: "WORKOUT_EDIT_LIFT",
	workoutIndex: workoutIndex,
	liftIndex: liftIndex,
	editType: type
});

export const edit_workout_lift = (editType, workoutIndex, liftIndex, amount) => ({
	type: "EDIT_WORKOUT_LIFT",
	editType: editType,
	workoutIndex: workoutIndex,
	liftIndex: liftIndex,
	amount: amount
});

export const save_workout_lift = (workoutIndex, liftIndex) => ({
	type: "SAVE_WORKOUT_LIFT",
	workoutIndex: workoutIndex,
	liftIndex: liftIndex,
});

export const delete_workout_lift = (workoutIndex, liftIndex) => ({
	type: "DELETE_WORKOUT_LIFT",
	workoutIndex: workoutIndex,
	liftIndex: liftIndex,
});

export const save_lift = (liftIndex) => ({
	type: "SAVE_LIFT",
	liftIndex: liftIndex,
});

export const delete_lift = (liftIndex) => ({
	type: "DELETE_LIFT",
	liftIndex: liftIndex,
});

export const edit_workout_lift_name = (workoutIndex, liftIndex, newName) => ({
	type: "EDIT_WORKOUT_LIFT_NAME",
	newName: newName,
	workoutIndex: workoutIndex,
	liftIndex: liftIndex,
});

export const edit_workout_name = (workoutIndex, newName) => ({
	type: "EDIT_WORKOUT_NAME",
	newName: newName,
	workoutIndex: workoutIndex,
});

export const edit_lift_name = (liftIndex, newName) => ({
	type: "EDIT_LIFT_NAME",
	newName: newName,
	liftIndex: liftIndex,
});

export const edit_lift = (editType, liftIndex, amount) => ({
	type: "EDIT_LIFT",
	editType: editType,
	liftIndex: liftIndex,
	amount: amount
});
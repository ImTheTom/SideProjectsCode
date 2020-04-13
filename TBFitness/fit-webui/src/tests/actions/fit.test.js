import {lifts, workout, workoutEdit, workout_edit_lift, edit_workout_lift, save_workout_lift, delete_workout_lift, save_lift, delete_lift, edit_workout_lift_name, edit_workout_name, edit_lift_name, edit_lift} from "../../actions/fit";

test("lifts should generate an action object", () => {
	const data = "add";
	const action = lifts(data);
	expect(action).toEqual({
		type: "LIFTS",
		payload: data
	});
});

test("workout should generate an action object", () => {
	const data = "remove";
	const action = workout(data);
	expect(action).toEqual({
		type: "WORKOUT",
		payload: data
	});
});

test("workoutEdit should generate an action object", () => {
	const data = "add";
	const action = workoutEdit(data);
	expect(action).toEqual({
		type: "WORKOUT_EDIT",
		payload: data
	});
});

test("workout_edit_lift should generate an action object", () => {
	const workoutIndex = 1;
	const liftIndex = 2;
	const editType = "add";
	const action = workout_edit_lift(workoutIndex, liftIndex, editType);
	expect(action).toEqual({
		type: "WORKOUT_EDIT_LIFT",
		workoutIndex: workoutIndex,
		liftIndex: liftIndex,
		editType: editType
	});
});

test("edit_workout_lift should generate an action object", () => {
	const workoutIndex = 1;
	const liftIndex = 2;
	const editType = "add";
	const amount = 1;
	const action = edit_workout_lift(editType, workoutIndex, liftIndex, amount);
	expect(action).toEqual({
		type: "EDIT_WORKOUT_LIFT",
		editType: editType,
		workoutIndex: workoutIndex,
		liftIndex: liftIndex,
		amount: amount
	});
});

test("save_workout_lift should generate an action object", () => {
	const workoutIndex = 1;
	const liftIndex = 2;
	const action = save_workout_lift(workoutIndex, liftIndex);
	expect(action).toEqual({
		type: "SAVE_WORKOUT_LIFT",
		workoutIndex: workoutIndex,
		liftIndex: liftIndex
	});
});

test("delete_workout_lift should generate an action object", () => {
	const workoutIndex = 1;
	const liftIndex = 2;
	const action = delete_workout_lift(workoutIndex, liftIndex);
	expect(action).toEqual({
		type: "DELETE_WORKOUT_LIFT",
		workoutIndex: workoutIndex,
		liftIndex: liftIndex
	});
});

test("save_lift should generate an action object", () => {
	const liftIndex = 2;
	const action = save_lift(liftIndex);
	expect(action).toEqual({
		type: "SAVE_LIFT",
		liftIndex: liftIndex
	});
});

test("delete_lift should generate an action object", () => {
	const liftIndex = 2;
	const action = delete_lift(liftIndex);
	expect(action).toEqual({
		type: "DELETE_LIFT",
		liftIndex: liftIndex
	});
});

test("edit_workout_lift_name should generate an action object", () => {
	const newName = "dumbell";
	const workoutIndex = 1;
	const liftIndex = 2;
	const action = edit_workout_lift_name(workoutIndex, liftIndex, newName);
	expect(action).toEqual({
		type: "EDIT_WORKOUT_LIFT_NAME",
		newName: newName,
		workoutIndex: workoutIndex,
		liftIndex: liftIndex
	});
});

test("edit_workout_name should generate an action object", () => {
	const newName = "Leg Day";
	const workoutIndex = 1;
	const action = edit_workout_name(workoutIndex, newName);
	expect(action).toEqual({
		type: "EDIT_WORKOUT_NAME",
		newName: newName,
		workoutIndex: workoutIndex
	});
});

test("edit_lift_name should generate an action object", () => {
	const newName = "dumbell";
	const liftIndex = 2;
	const action = edit_lift_name(liftIndex, newName);
	expect(action).toEqual({
		type: "EDIT_LIFT_NAME",
		newName: newName,
		liftIndex: liftIndex
	});
});

test("edit_lift should generate an action object", () => {
	const editType = "add";
	const liftIndex = 2;
	const amount = 2;
	const action = edit_lift(editType, liftIndex, amount);
	expect(action).toEqual({
		type: "EDIT_LIFT",
		editType: editType,
		liftIndex: liftIndex,
		amount: amount
	});
});
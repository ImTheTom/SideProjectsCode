import workoutBase from "../fixtures/workoutBase";
import workoutReducer from "../../reducers/workout";
import axios from "axios";
import Config from "../../config";

jest.mock("axios");

test("Should get the state", () => {
	const action = {
		type: "WORKOUT",
		payload: workoutBase
	};
	const state = workoutReducer(undefined, action);
	expect(state).toEqual(workoutBase);
});


test("Should update id name", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT_NAME",
		newName: "Incline dumbel press",
		workoutIndex: 0,
		liftIndex: 0,
	};
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].name).toEqual("Incline dumbel press");
});

test("Should save lift", () => {
	const action = {
		type: "SAVE_WORKOUT_LIFT",
		liftIndex: 0,
	};
	axios.put.mockImplementationOnce(() => Promise.resolve({OK:"Test response"}));
	const state = workoutReducer(workoutBase, action);

	expect(state).toEqual(workoutBase);
	expect(axios.put).toHaveBeenCalledWith(
		Config.host.NAME + "lift",{
			id: 1,
			user_id: 1,
			name: "Incline dumbel press",
			weight: 20,
			reps: 5,
			sets: 5
		}
	);
	expect(axios.put).toHaveBeenCalledTimes(1);
});

test("Should update lift name", () => {
	const action = {
		type: "EDIT_WORKOUT_NAME",
		newName: "leg day",
		workoutIndex: 0,
	};
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.name).toEqual("leg day");
});

test("Should add weight", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "IncreaseWeight",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].weight;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].weight).toEqual(previous + 1);
});

test("Should decrease weight", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "DecreaseWeight",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].weight;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].weight).toEqual(previous - 1);
});

test("Should add reps", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "IncreaseReps",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].reps;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].reps).toEqual(previous + 1);
});

test("Should decrease reps", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "DecreaseReps",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].reps;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].reps).toEqual(previous - 1);
});

test("Should add sets", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "IncreaseSets",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].sets;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].sets).toEqual(previous + 1);
});

test("Should decrease sets", () => {
	const action = {
		type: "EDIT_WORKOUT_LIFT",
		editType: "DecreaseSets",
		workoutIndex: 0,
		liftIndex: 0,
		amount: 1
	};
	const previous = workoutBase.workouts.lifts[0].sets;
	const state = workoutReducer(workoutBase, action);
	expect(state.workouts.lifts[0].sets).toEqual(previous - 1);
});
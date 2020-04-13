import liftsBase from "../fixtures/liftsBase";
import liftsReducer from "../../reducers/lifts";
import axios from "axios";
import Config from "../../config";

jest.mock("axios");

test("Should create a state", () => {
	const action = {
		type: "LIFTS",
		payload: liftsBase
	};
	const state = liftsReducer(undefined, action);
	expect(state).toEqual(liftsBase);
});

test("Should update id name", () => {
	const action = {
		type: "EDIT_LIFT_NAME",
		newName: "Bench Press",
		liftIndex: 0
	};
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[0].name).toEqual("Bench Press");
});

test("Should save lift", () => {
	const action = {
		type: "SAVE_LIFT",
		liftIndex: 0,
	};
	axios.put.mockImplementationOnce(() => Promise.resolve({OK:"Test response"}));
	const state = liftsReducer(liftsBase, action);

	expect(state).toEqual(liftsBase);
	expect(axios.put).toHaveBeenCalledWith(
		Config.host.NAME + "lift",{
			id: 1,
			user_id: 1,
			name: "Bench Press",
			weight: 20,
			reps: 5,
			sets: 5
		}
	);
	expect(axios.put).toHaveBeenCalledTimes(1);
});

test("Should delete lift", () => {
	const action = {
		type: "DELETE_LIFT",
		liftIndex: 0,
	};
	axios.delete.mockImplementationOnce(() => Promise.resolve({OK:"Test response"}));
	const state = liftsReducer(liftsBase, action);

	expect(state).toEqual(liftsBase);
	expect(axios.delete).toHaveBeenCalledWith(
		Config.host.NAME + "lift/1", {}
	);
	expect(axios.delete).toHaveBeenCalledTimes(1);
});

test("Should add weight", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "IncreaseWeight",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].weight;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].weight).toEqual(previous + 1);
});

test("Should decrease weight", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "DecreaseWeight",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].weight;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].weight).toEqual(previous - 1);
});

test("Should add reps", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "IncreaseReps",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].reps;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].reps).toEqual(previous + 1);
});

test("Should decrease reps", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "DecreaseReps",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].reps;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].reps).toEqual(previous - 1);
});

test("Should add reps", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "IncreaseSets",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].sets;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].sets).toEqual(previous + 1);
});

test("Should decrease reps", () => {
	const action = {
		type: "EDIT_LIFT",
		editType: "DecreaseSets",
		liftIndex: 2,
		amount: 1
	};
	const previous = liftsBase.Lifts[2].sets;
	const state = liftsReducer(liftsBase, action);
	expect(state.Lifts[2].sets).toEqual(previous - 1);
});
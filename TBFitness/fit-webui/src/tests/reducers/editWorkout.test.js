import editWorkoutBase from "../fixtures/editWorkoutBase";
import editWorkoutReducer from "../../reducers/editWorkout";
import axios from "axios";
import Config from "../../config";

jest.mock("axios");

test("Should get the state", () => {
	const action = {
		type: "WORKOUT_EDIT",
		payload: editWorkoutBase
	};
	const state = editWorkoutReducer(undefined, action);
	expect(state).toEqual(editWorkoutBase);
});

test("Should send a post", () => {
	const action = {
		type: "WORKOUT_EDIT_LIFT",
		workoutIndex: 0,
		liftIndex: 1,
		editType: "Add"
	};
	axios.post.mockImplementationOnce(() => Promise.resolve({OK:"Test response"}));
	const state = editWorkoutReducer(editWorkoutBase, action);
	expect(state).toEqual(editWorkoutBase);
	expect(axios.post).toHaveBeenCalledWith(
		Config.host.NAME + "workoutedit",{
			lift_id: action.liftIndex,
			workout_id: action.workoutIndex
		}
	);
	expect(axios.post).toHaveBeenCalledTimes(1);
});

test("Should send a delete call", () => {
	const action = {
		type: "WORKOUT_EDIT_LIFT",
		workoutIndex: 0,
		liftIndex: 1,
		editType: "Remove"
	};
	axios.delete.mockImplementationOnce(() => Promise.resolve({OK:"Test response"}));
	const state = editWorkoutReducer(editWorkoutBase, action);
	expect(state).toEqual(editWorkoutBase);
	expect(axios.delete).toHaveBeenCalledWith(
		Config.host.NAME + "workoutedit", {
			data: { 
				lift_id: action.liftIndex,
				workout_id: action.workoutIndex 
			},
			headers: { }
		}
	);
	expect(axios.delete).toHaveBeenCalledTimes(1);
});
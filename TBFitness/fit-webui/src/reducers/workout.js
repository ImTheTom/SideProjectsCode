import axios from "axios";
import Config from "../config";

const workoutReducerDefaultState = {
};

export default (state = workoutReducerDefaultState, action) => {
	switch (action.type) {
	case "WORKOUT":
		state = action.payload;
		return {
			...state,
		};
	case "SAVE_WORKOUT_LIFT":
		axios.put(Config.host.NAME + "lift", {
			id: state.lifts[action.liftIndex].id,
			user_id: state.lifts[action.liftIndex].user_id,
			name: state.lifts[action.liftIndex].name,
			weight: state.lifts[action.liftIndex].weight,
			reps: state.lifts[action.liftIndex].reps,
			sets: state.lifts[action.liftIndex].sets,
		}).then(function (response) {
			console.log(response);
		}).catch(function (error) {
			console.log(error);
		});
		console.log("SAVED");
		return {
			...state,
		};
	case "DELETE_WORKOUT_LIFT":
		axios.delete(Config.host.NAME+ "lift/" + state.lifts[action.liftIndex].id, {
		}).then(function (response) {
			console.log(response);
		}).catch(function (error) {
			console.log(error);
		});
		return {
			...state,
		};
	case "EDIT_WORKOUT_LIFT_NAME":
		state.lifts[action.liftIndex].name = action.newName;
		return {
			...state,
		};
	case "EDIT_WORKOUT_NAME":
		state.name = action.newName;
		return {
			...state,
		};
	case "EDIT_WORKOUT_LIFT":
		switch(action.editType) {
		case "IncreaseWeight":
			state.lifts[action.liftIndex].weight = state.lifts[action.liftIndex].weight + action.amount;
			break;
		case "DecreaseWeight":
			state.lifts[action.liftIndex].weight = state.lifts[action.liftIndex].weight - action.amount;
			break;
		case "IncreaseReps":
			state.lifts[action.liftIndex].reps = state.lifts[action.liftIndex].reps + action.amount;
			break;
		case "DecreaseReps":
			state.lifts[action.liftIndex].reps = state.lifts[action.liftIndex].reps - action.amount;
			break;
		case "IncreaseSets":
			state.lifts[action.liftIndex].sets = state.lifts[action.liftIndex].sets + action.amount;
			break;
		case "DecreaseSets":
			state.lifts[action.liftIndex].sets = state.lifts[action.liftIndex].sets - action.amount;
			break;
		}
		return {
			...state,
		};
	default:
		return state;
	}
};
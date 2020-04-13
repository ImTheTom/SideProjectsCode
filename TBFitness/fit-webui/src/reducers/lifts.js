import axios from "axios";
import Config from "../config";

const liftsReducerDefaultState = {
};

export default (state = liftsReducerDefaultState, action) => {
	switch (action.type) {
	case "LIFTS":
		state = action.payload;
		return {
			...state,
		};
	case "SAVE_LIFT":
		axios.put(Config.host.NAME + "lift", {
			id: state[action.liftIndex].id,
			user_id: state[action.liftIndex].user_id,
			name: state[action.liftIndex].name,
			weight: state[action.liftIndex].weight,
			reps: state[action.liftIndex].reps,
			sets: state[action.liftIndex].sets,
		}).then(function (response) {
			console.log(response);
		}).catch(function (error) {
			console.log(error);
		});
		return {
			...state,
		};
	case "DELETE_LIFT":
		axios.delete(Config.host.NAME + "lift/" + state[action.liftIndex].id, {
		}).then(function (response) {
			console.log(response);
		}).catch(function (error) {
			console.log(error);
		});
		return {
			...state,
		};
	case "EDIT_LIFT_NAME":
		state[action.liftIndex].name = action.newName;
		return {
			...state,
		};
	case "EDIT_LIFT":
		console.log(state);
		switch(action.editType) {
		case "IncreaseWeight":
			state[action.liftIndex].weight = state[action.liftIndex].weight + action.amount;
			break;
		case "DecreaseWeight":
			state[action.liftIndex].weight = state[action.liftIndex].weight - action.amount;
			break;
		case "IncreaseReps":
			state[action.liftIndex].reps = state[action.liftIndex].reps + action.amount;
			break;
		case "DecreaseReps":
			state[action.liftIndex].reps = state[action.liftIndex].reps - action.amount;
			break;
		case "IncreaseSets":
			state[action.liftIndex].sets = state[action.liftIndex].sets + action.amount;
			break;
		case "DecreaseSets":
			state[action.liftIndex].sets = state[action.liftIndex].sets - action.amount;
			break;
		}
		return {
			...state,
		};
	default:
		return state;
	}
};
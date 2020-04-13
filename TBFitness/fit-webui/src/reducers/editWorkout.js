import axios from "axios";
import Config from "../config";

const editWorkoutDefaultState = {
	current:{},
	available:[]
};

export default (state = editWorkoutDefaultState, action) => {
	switch (action.type) {
	case "WORKOUT_EDIT":
		state = action.payload;
		return {
			...state,
		};
	case "WORKOUT_EDIT_LIFT":
		switch (action.editType){
		case "Add":
			axios.post(Config.host.NAME + "workoutedit", {
				lift_id: action.liftIndex,
				workout_id: action.workoutIndex
			}).then(function (response) {
				console.log(response);
			}).catch(function (error) {
				console.log(error);
			});
			return {
				...state,
			};
		case "Remove":
			axios.delete(
				Config.host.NAME + "workoutedit", {
					data: { 
						lift_id: action.liftIndex,
						workout_id: action.workoutIndex 
					},
					headers: { }
				}
			)
				.then(data => { 
					console.log(data);
				})
				.catch(err => { 
					console.log(err);
				});
			return {
				...state,
			};
		}
		return {
			...state,
		};
	default:
		return state;
	}
};
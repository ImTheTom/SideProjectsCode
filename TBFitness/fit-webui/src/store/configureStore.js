import { createStore, combineReducers, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import workout from "../reducers/workout";
import lifts from "../reducers/lifts";
import editWorkout from "../reducers/editWorkout";

const store = createStore(combineReducers({ workout: workout, lifts: lifts, editWorkout: editWorkout }), applyMiddleware(thunk));

export default store;


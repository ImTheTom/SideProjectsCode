import DisplayWorkout from "../../components/EditWorkout/DisplayWorkout";
import React from "react";
import { shallow } from "enzyme";
import WorkoutBase from "../fixtures/editWorkoutBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay workout component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<DisplayWorkout workout={WorkoutBase.current} workoutIndex = {WorkoutBase.current.id} type="Remove" />
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
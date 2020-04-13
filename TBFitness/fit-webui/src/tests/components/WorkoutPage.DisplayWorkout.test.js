import DisplayWorkout from "../../components/WorkoutPage/DisplayWorkout";
import React from "react";
import { shallow } from "enzyme";
import workoutBase from "../fixtures/workoutBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<DisplayWorkout workout={workoutBase} workoutIndex={0} />
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
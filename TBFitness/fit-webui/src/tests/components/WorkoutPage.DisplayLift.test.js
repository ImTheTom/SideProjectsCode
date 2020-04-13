import DisplayLift from "../../components/WorkoutPage/DisplayLift";
import React from "react";
import { shallow } from "enzyme";
import workoutBase from "../fixtures/workoutBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<DisplayLift lifts={workoutBase.workouts.lifts[0]} liftIndex={0} workoutIndex={0} key={0}/>
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
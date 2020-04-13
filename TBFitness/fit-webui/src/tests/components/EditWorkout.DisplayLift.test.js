import DisplayLift from "../../components/EditWorkout/DisplayLift";
import React from "react";
import { shallow } from "enzyme";
import WorkoutBase from "../fixtures/editWorkoutBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<DisplayLift lifts={WorkoutBase.available[0]} liftIndex={0} workoutIndex={WorkoutBase.current.id} type="Add" key={0}/>
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
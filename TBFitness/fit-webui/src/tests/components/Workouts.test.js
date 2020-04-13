import Workouts from "../../components/Workouts";
import React from "react";
import { shallow } from "enzyme";
import workoutBase from "../fixtures/workoutBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<Workouts workoutData={workoutBase} workout={1}/>
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
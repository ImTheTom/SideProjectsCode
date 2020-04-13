import CreateWorkoutPage from "../../components/CreateWorkoutPage";
import React from "react";
import { shallow } from "enzyme";

test("should render create Lift page correctly", () => {
	const wrapper = shallow(<CreateWorkoutPage />);
	expect(wrapper).toMatchSnapshot();
});
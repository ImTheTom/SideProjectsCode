import CreateLiftPage from "../../components/CreateLiftPage";
import React from "react";
import { shallow } from "enzyme";

test("should render create Lift page correctly", () => {
	const wrapper = shallow(<CreateLiftPage />);
	expect(wrapper).toMatchSnapshot();
});

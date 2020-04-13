import Home from "../../components/Home";
import React from "react";
import { shallow } from "enzyme";

test("should render create Home Page correctly", () => {
	const wrapper = shallow(<Home />);
	expect(wrapper).toMatchSnapshot();
});
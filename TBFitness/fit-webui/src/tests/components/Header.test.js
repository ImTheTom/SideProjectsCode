import Header from "../../components/Header";
import React from "react";
import { shallow } from "enzyme";

test("should render create Header Component correctly", () => {
	const wrapper = shallow(<Header />);
	expect(wrapper).toMatchSnapshot();
});

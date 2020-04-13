import Lifts from "../../components/Lifts";
import React from "react";
import { shallow } from "enzyme";
import liftsBase from "../fixtures/liftsBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<Lifts liftsData={liftsBase}/>
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
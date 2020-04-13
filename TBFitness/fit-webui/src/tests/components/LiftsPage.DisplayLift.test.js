import DisplayLift from "../../components/LiftsPage/DisplayLift";
import React from "react";
import { shallow } from "enzyme";
import LiftsBase from "../fixtures/liftsBase";
import store from "../../store/configureStore";
import { Provider } from "react-redux";

test("should render diplay lift component correctly", () => {
	const wrapper = shallow(
		<Provider store={store}>
			<DisplayLift lifts={LiftsBase[0]} liftIndex={0} key={0}/>
		</Provider>
	);
	expect(wrapper).toMatchSnapshot();
});
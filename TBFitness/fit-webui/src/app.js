import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import AppRouter from "./routers/AppRouter";
import store from "./store/configureStore";
import "normalize.css/normalize.css";
import "antd/dist/antd.css";
import "./styles/styles.scss";
import "react-dates/lib/css/_datepicker.css";


const App = () => (
	<Provider store={store}>
		<AppRouter />
	</Provider>
);

render(<App />, document.getElementById("app"));
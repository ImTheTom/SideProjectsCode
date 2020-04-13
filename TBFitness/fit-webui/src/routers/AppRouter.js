import React from "react";
import { Router, Route, Switch} from "react-router-dom";
import createHistory from "history/createBrowserHistory";
import Home from "../components/Home";
import Lifts from "../components/Lifts";
import WorkoutIndividual from "../components/WorkoutIndividual";
import CreateLiftPage from "../components/CreateLiftPage";
import Workouts from "../components/Workouts";
import WorkoutLiftAddition from "../components/WorkoutLiftAddition";
import CreateWorkoutPage from "../components/CreateWorkoutPage";
import NotFoundPage from "../components/NotFoundPage";

export const history = createHistory();

const AppRouter = () => (
	<Router history={history}>
		<div>
			<Switch>
				<Route path="/lifts" exact={true} component={Lifts} />
				<Route path="/workout/:id" exact={true} component={WorkoutIndividual} />
				<Route path="/workout/:id/edit" exact={true} component={WorkoutLiftAddition} />
				<Route path="/createworkout" exact={true} component={CreateWorkoutPage} />
				<Route path="/workouts" exact={true} component={Workouts} />
				<Route path="/createlift" exact={true} component={CreateLiftPage} />
				<Route path="/" exact={true} component={Home} />
				<Route component={NotFoundPage} />
			</Switch>
		</div>
	</Router>
);

export default AppRouter;

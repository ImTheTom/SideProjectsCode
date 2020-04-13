import React from "react";
import { Link } from "react-router-dom";

const Header = () => (
	<div className="header">
		<div className="header-links">
			<Link to="/">Home</Link>
			<Link to="/lifts">Lifts</Link>
			<Link to="/createlift">Create Lift</Link>
			<Link to="/createworkout">Create Workout</Link>
			<Link to="/workouts">Workouts</Link>
		</div>
	</div>
);

export default Header;
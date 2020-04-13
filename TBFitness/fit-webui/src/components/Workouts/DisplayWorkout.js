import React, { Component } from "react";
import { Link } from "react-router-dom";

class DisplayWorkout extends Component {
	render() {
		const url = "/workout/"+this.props.workout.id;
		const editUrl = "/workout/"+this.props.workout.id+"/edit";
		return (
			<div className="link-box">
				<Link className="workout-name"to={url}>{this.props.workout.name}</Link>
				<Link className="workout-edit"to={editUrl}>edit</Link>
			</div>
		);
	}
}

export default DisplayWorkout;
import React, { Component } from "react";
import { connect } from "react-redux";
import DisplayLift from "./DisplayLift";

class DisplayWorkout extends Component {
	render() {
		let liftData;
		if (this.props.workout.lifts !== undefined) {
			liftData = this.props.workout.lifts.map((lift, index) => (
				<DisplayLift lifts={lift} liftIndex={lift.id} workoutIndex={this.props.workoutIndex} type={this.props.type} key={index}/>
			));
		}
		return (
			<div>
				<div className = "workout-title">{this.props.workout.name}</div>
				{liftData}
			</div>
		);
	}
}


export default connect(undefined, undefined)(DisplayWorkout);
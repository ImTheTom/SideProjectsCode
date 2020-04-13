import React, { Component } from "react";
import { connect } from "react-redux";
import { workout_edit_lift } from "../../actions/fit";
import { Button } from "antd";

class DisplayLift extends Component {
	constructor() {
		super();
		this.onEditLift = this.onEditLift.bind(this);
	}

	onEditLift = () => {
		this.setState(this.props.workoutEditLift(this.props.workoutIndex, this.props.liftIndex, this.props.type));
	}

	render() {
		return (
			<div>
				<div className="edit-option">
					{this.props.lifts.name}
					<Button className="display-lift-button" type="primary"  onClick={() => this.onEditLift()}>{this.props.type}</Button>
				</div>
			</div>
		);
	}
}

const mapDispatchToProps = (dispatch) => ({
	workoutEditLift: (workoutIndex, liftIndex, type) => dispatch(workout_edit_lift(workoutIndex, liftIndex, type)),
});

export default connect(undefined, mapDispatchToProps)(DisplayLift);
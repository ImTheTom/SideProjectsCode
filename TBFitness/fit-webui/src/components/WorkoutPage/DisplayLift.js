import React, { Component } from "react";
import { connect } from "react-redux";
import { edit_workout_lift, save_workout_lift, edit_workout_lift_name, delete_workout_lift } from "../../actions/fit";
import { Button } from "antd";

class DisplayLift extends Component {
	constructor() {
		super();
		this.onEditWorkout = this.onEditWorkout.bind(this);
		this.onSaveWorkout = this.onSaveWorkout.bind(this);
		this.onDeleteWorkout = this.onDeleteWorkout.bind(this);
	}

	onEditWorkout = (editType, amount) => {
		this.setState(this.props.editWorkoutLift(editType, this.props.workoutIndex, this.props.liftIndex, amount));
	}

	onSaveWorkout = () => {
		this.setState(this.props.saveWorkoutLift(this.props.workoutIndex, this.props.liftIndex));
	}

	onDeleteWorkout = () => {
		this.setState(this.props.deleteWorkoutLift(this.props.workoutIndex, this.props.liftIndex));
	}


	render() {
		return (
			<div className ="display-lift">
				<div className = "display-lift-name">{this.props.lifts.name}</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("DecreaseWeight", 1)}>&lt;</Button>
					{this.props.lifts.weight}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("IncreaseWeight", 1)}>&gt;</Button>
				</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("DecreaseReps", 1)}>&lt;</Button>
					{this.props.lifts.reps}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("IncreaseReps", 1)}>&gt;</Button>
				</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("DecreaseSets", 1)}>&lt;</Button>
					{this.props.lifts.sets}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditWorkout("IncreaseSets", 1)}>&gt;</Button>
				</div>
				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onSaveWorkout()}>Save</Button>
					<Button className="display-lift-button" type="primary" onClick={() => this.onDeleteWorkout()}>Delete</Button>
				</div>
			</div>
		);
	}
}

const mapDispatchToProps = (dispatch) => ({
	editWorkoutLiftName: (workoutIndex, liftIndex, newName) => dispatch(edit_workout_lift_name(workoutIndex, liftIndex, newName)),
	editWorkoutLift: (editType, workoutIndex, liftIndex, amount) => dispatch(edit_workout_lift(editType, workoutIndex, liftIndex, amount)),
	saveWorkoutLift: (workoutIndex, liftIndex) => dispatch(save_workout_lift(workoutIndex, liftIndex)),
	deleteWorkoutLift: (workoutIndex, liftIndex) => dispatch(delete_workout_lift(workoutIndex, liftIndex))
});

export default connect(undefined, mapDispatchToProps)(DisplayLift);
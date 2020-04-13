import React, { Component } from "react";
import { connect } from "react-redux";
import { edit_lift_name, edit_lift, save_lift, delete_lift } from "../../actions/fit";
import { Input, Button } from "antd";

class DisplayLift extends Component {
	constructor() {
		super();
		this.onEditLift = this.onEditLift.bind(this);
		this.handleInputChange = this.handleInputChange.bind(this);
		this.onSaveLift = this.onSaveLift.bind(this);
		this.onDeleteLift = this.onDeleteLift.bind(this);
	}

	onEditLift = (editType, amount) => {
		this.setState(this.props.editLift(editType, this.props.liftIndex, amount));
	}

	handleInputChange = (e) => {
		this.setState(this.props.editLiftName(this.props.liftIndex, e.target.value));
	}

	onSaveLift = () => {
		this.setState(this.props.saveLift(this.props.liftIndex));
	}

	onDeleteLift = () => {
		this.setState(this.props.deleteLift(this.props.liftIndex));
	}

	render() {
		return (
			<div className ="display-lift">
				<div className = "display-lift-name">
					<Input value={this.props.lifts.name}  onChange={e => this.handleInputChange(e)}></Input>
				</div>
				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("DecreaseWeight", 1)}>&lt;</Button>
					{this.props.lifts.weight}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("IncreaseWeight", 1)}>&gt;</Button>
				</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("DecreaseReps", 1)}>&lt;</Button>
					{this.props.lifts.reps}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("IncreaseReps", 1)}>&gt;</Button>
				</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("DecreaseSets", 1)}>&lt;</Button>
					{this.props.lifts.sets}
					<Button className="display-lift-button" type="primary" onClick={() => this.onEditLift("IncreaseSets", 1)}>&gt;</Button>
				</div>

				<div className="display-lift-control">
					<Button className="display-lift-button" type="primary" onClick={() => this.onSaveLift()}>Save</Button>
					<Button className="display-lift-button" type="primary" onClick={() => this.onDeleteLift()}>Delete</Button>
				</div>
			</div>
		);
	}
}

const mapDispatchToProps = (dispatch) => ({
	editLiftName: (liftIndex, newName) => dispatch(edit_lift_name(liftIndex, newName)),
	editLift: (editType, liftIndex, amount) => dispatch(edit_lift(editType, liftIndex, amount)),
	saveLift: (liftIndex) => dispatch(save_lift(liftIndex)),
	deleteLift: (liftIndex) => dispatch(delete_lift(liftIndex)),
});

export default connect(undefined, mapDispatchToProps)(DisplayLift);
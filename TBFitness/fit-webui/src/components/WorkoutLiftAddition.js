import React from "react";
import Header from "./Header";
import { connect } from "react-redux";
import { workoutEdit } from "../actions/fit";
import DisplayWorkout from "./EditWorkout/DisplayWorkout";
import DisplayLift from "./EditWorkout/DisplayLift";
import axios from "axios";
import Config from "../config";

class WorkoutLiftAddition extends React.Component {
	render() {
		let liftData;
		if (this.props.editWorkoutData.available !== undefined) {
			liftData = this.props.editWorkoutData.available.map((lift, index) => (
				<DisplayLift lifts={lift} liftIndex={lift.id} workoutIndex={this.props.editWorkoutData.current.id} type="Add" key={index}/>
			));
		}
		return (
			<div>
				<div>
					<Header />
				</div>
				<div className="lifts-box">
					<DisplayWorkout workout={this.props.editWorkoutData.current} workoutIndex = {this.props.editWorkoutData.current.id} type="Remove"/>
					{liftData}
				</div>
			</div>
		);
	}

	componentDidMount() {
		let url = Config.host.NAME + "workoutedit/"+ this.props.match.params.id;
		axios.get(url).then(({ data }) => {
			this.props.workoutEdit(data);
		});
	}
}

const mapStateToProps = state => ({
	editWorkoutData: state.editWorkout
});

const mapDispatchToProps = (dispatch) => ({
	workoutEdit: (data) => dispatch(workoutEdit(data)),
});

export default connect(mapStateToProps, mapDispatchToProps)(WorkoutLiftAddition);

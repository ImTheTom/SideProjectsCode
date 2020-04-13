import React from "react";
import Header from "./Header";
import { connect } from "react-redux";
import { workout } from "../actions/fit";
import DisplayWorkout from "./WorkoutPage/DisplayWorkout";
import axios from "axios";
import Config from "../config";

class WorkoutIndividual extends React.Component {
	render() {
		console.log(this.props);
		let display;
		if (Object.keys(this.props.workoutData).length !== 0) {
			display = <DisplayWorkout workout={this.props.workoutData} workoutIndex = {this.props.workoutData.id}/>;
		}
		return (
			<div>
				<div>
					<Header />
				</div>
				<div className="lifts-box">
					{display}
				</div>
			</div>
		);
	}

	componentDidMount() {
		let url = Config.host.NAME + "workout/" + this.props.match.params.id;
		axios.get(url).then(({ data }) => {
			this.props.workout(data);
		});
	}
}

const mapStateToProps = state => ({
	workoutData: state.workout
});

const mapDispatchToProps = (dispatch) => ({
	workout: (data) => dispatch(workout(data)),
});

export default connect(mapStateToProps, mapDispatchToProps)(WorkoutIndividual);
  
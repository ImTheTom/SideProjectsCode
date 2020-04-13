import React from "react";
import Header from "./Header";
import { connect } from "react-redux";
import { workout } from "../actions/fit";
import DisplayWorkout from "./Workouts/DisplayWorkout";
import axios from "axios";
import Config from "../config";

class Workout extends React.Component {
	render() {
		let display;
		if (Object.keys(this.props.workoutData).length !== 0) {
			display = Object.values(this.props.workoutData).map((workout, index) => (
				<div key={index}>
					<DisplayWorkout workout={workout} workoutIndex = {index} key={index}/>
				</div>
			));
		}
		return (
			<div>
				<div>
					<Header />
				</div>
				<div className="lifts-box">
					<div className="create-lift-title">
					Workouts
					</div>
					{display}
				</div>
			</div>
		);
	}

	componentDidMount() {
		const url = Config.host.NAME + "workouts/1";
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
  
export default connect(mapStateToProps, mapDispatchToProps)(Workout);
  

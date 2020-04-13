import React from "react";
import Header from "./Header";
import { connect } from "react-redux";
import { lifts } from "../actions/fit";
import DisplayLift from "./LiftsPage/DisplayLift";
import axios from "axios";
import Config from "../config";

class Lifts extends React.Component {
	render() {
		let display;
		if (Object.keys(this.props.liftsData).length !== 0) {
			display = Object.values(this.props.liftsData).map((lift, index) => (
				<DisplayLift lifts={lift} liftIndex={index} key={index}/>
			));
		}
		return (
			<div>
				<div>
					<Header />
				</div>
				<div className="lifts-box">
					<div className="create-lift-title">
					Lifts
					</div>
					{display}
				</div>
			</div>
		);
	}

	componentDidMount() {
		axios.get(Config.host.NAME + "lift/1").then(({ data }) => {
			this.props.lifts(data);
		});
	}
}

const mapStateToProps = state => ({
	liftsData: state.lifts
});

const mapDispatchToProps = (dispatch) => ({
	lifts: (data) => dispatch(lifts(data)),
});

export default connect(mapStateToProps, mapDispatchToProps)(Lifts);


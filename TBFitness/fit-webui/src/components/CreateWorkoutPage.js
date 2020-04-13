import React from "react";
import Header from "./Header";
import axios from "axios";
import Config from "../config";
import { Input, Button } from "antd";

class CreateWorkoutPage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			Name: "",
		};
		this.handleInputChange = this.handleInputChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleInputChange(event) {
		const target = event.target;
		const value = target.value;
		const name = target.name;

		this.setState({
			[name]: value
		});
	}

	handleSubmit(event) {
		event.preventDefault();
		axios.post(Config.host.NAME + "workout", {
			id: 0,
			user_id: 1,
			name: this.state.Name,
		}).then(function (response) {
			console.log(response);
		}).catch(function (error) {
			console.log(error);
		});
	}

	render() {
		return (
			<div>
				<div>
					<Header />
				</div>
				<div className="CreateBox">
					<div className="create-lift-title">
		Create Workout
					</div>
					<form onSubmit={this.handleSubmit}>
						<Input className="create-lift-input" placeholder="Name" label="Name" name="Name" value={this.state.Name} onChange={(e) => this.handleInputChange(e)} /> <br></br>
						<Button className="create-lift-input" type="primary">Create Workout</Button>
					</form>
				</div>
			</div>
		);
	}
}


export default CreateWorkoutPage;


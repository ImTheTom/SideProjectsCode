import React from "react";
import Header from "./Header";
import axios from "axios";
import Config from "../config";
import { Input, Button } from "antd";

class CreateLiftPage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			Name: "",
			Weight: "",
			Reps: "",
			Sets: ""
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
		axios.post(Config.host.NAME + "lift", {
			id: 0,
			user_id: 1,
			name: this.state.Name,
			weight: parseInt(this.state.Weight, 10),
			reps: parseInt(this.state.Reps, 10),
			sets: parseInt(this.state.Sets, 10),
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
						Create Lift
					</div>
					<form onSubmit={this.handleSubmit}>
						<Input className="create-lift-input" placeholder="Name" label="Name" name="Name" value={this.state.Name} onChange={(e) => this.handleInputChange(e)} /> <br></br>
						<Input className="create-lift-input" placeholder="Weight" label="Weight" name="Weight" value={this.state.Weight} onChange={(e) => this.handleInputChange(e)} /> <br></br>
						<Input className="create-lift-input" placeholder="Reps" label="Reps" name="Reps" value={this.state.Reps} onChange={(e) => this.handleInputChange(e)} /> <br></br>
						<Input className="create-lift-input" placeholder="Sets" label="Sets" name="Sets" value={this.state.Sets} onChange={(e) => this.handleInputChange(e)} /> <br></br>
						<Button className="create-lift-input" type="primary">Create Lift</Button>
					</form>
				</div>
			</div>
		);
	}
}


export default CreateLiftPage;

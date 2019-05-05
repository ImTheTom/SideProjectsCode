import React from 'react';
import axios from 'axios';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

class SignInForm extends React.Component {
	constructor () {
		super();
		this.state = {
			email: '',
			password: '',
			error: ''
		};
	
		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleChange (evt) {
		this.setState({ [evt.target.name]: evt.target.value });
	}

	handleSubmit(event) {
		event.preventDefault();
		var self = this;
		axios.post('http://localhost:4000/user', {
			email: this.state.email,
			password: this.state.password
		})
		.then(function (response) {
			cookies.set('jwt', response.data, { path: '/' });
			window.location = "http://localhost:3000/";
		})
		.catch(function () {
			self.setState({
				email:self.state.email,
				password:self.state.password,
				error:"Invalid username or password",
			});
		});
	}
	
	render() {
		return (
			<div id="signinForm">
			<div className="error">
			{this.state.error}
		</div>
			<form onSubmit={this.handleSubmit}>
				<label>Email</label><br></br>
				<input type="email" name="email" onChange={this.handleChange} required/> <br></br>
				<label>Password</label><br></br>
				<input type="password" name="password" onChange={this.handleChange} required/> <br></br>
				<input id="submitButton" type="submit" value="Submit" />
			</form>
			</div>
		);
	}
}

export default SignInForm;
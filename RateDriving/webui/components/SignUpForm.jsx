import React from 'react';
import axios from 'axios';
import CryptoJS from "crypto-js";

class SignUpForm extends React.Component {
	constructor () {
		super();
		this.state = {
			username:'',
			email: '',
			password: '',
			error:''
		};

		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleChange (evt) {
		this.setState({ [evt.target.name]: evt.target.value });
	}

	handleSubmit(event) {
		var self = this;
		var nonce = [...Array(32)].map(i=>(~~(Math.random()*36)).toString(36)).join('');
		var hash = CryptoJS.HmacSHA256(this.state.password, nonce);
		var hashInBase64 = CryptoJS.enc.Base64.stringify(hash);
		event.preventDefault();
		axios.post('http://localhost:4000/signup', {
			username: this.state.username,
			email: this.state.email,
			salt: nonce,
			password: hashInBase64
		})
		.then(function () {
			window.location = "http://localhost:3000/signin";
		})
		.catch(function () {
			self.setState({
				username:self.state.username,
				email:self.state.email,
				password:self.state.password,
				error:"User already exists",
			});
		});
	}
	
	render() {
		return (
			<div>
			<div className="error">
			{this.state.error}
		</div>
			<form onSubmit={this.handleSubmit}>
				<label>Username</label><br></br>
				<input type="text" name="username" onChange={this.handleChange} required/><br></br>
				<label>Email</label><br></br>
				<input type="email" name="email" onChange={this.handleChange} required/><br></br>
				<label>Password</label><br></br>
				<input type="password" name="password" onChange={this.handleChange} required/><br></br>
				<input id="submitButton" type="submit" value="Submit" /><br></br>
			</form>
			</div>
		);
	}
}

export default SignUpForm;
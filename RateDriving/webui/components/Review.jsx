import React from 'react';
import axios from 'axios';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

class Review extends React.Component {
	constructor () {
		super();
		this.state = {
			numberplate: '',
			type: 'driving',
			review: '',
			error: ""
		};
	
		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	componentDidMount() {
		var pl = this.props.plate;
		var format = /[ !@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/;
		if (format.test(pl)) {
			window.location = "http://localhost:3000/";
		} else if(pl.length > 6 || pl.length == 0 || pl == undefined) {
			window.location = "http://localhost:3000/";
		}
	}

	handleChange (evt) {
		this.setState({ [evt.target.name]: evt.target.value });
	}

	handleSubmit(event) {
		event.preventDefault();
		var self = this;
		var jwt = cookies.get('jwt');
		if(jwt === undefined) {
			self.setState({
				numberplate: self.props.plate,
				type: self.state.type,
				review: self.state.review,
				error:"Sign in to write a review"
			});
			return;
		}
		axios.post('http://localhost:4000/review', {
			jwt: cookies.get('jwt'),
			numberplate: this.props.plate,
			type: this.state.type,
			review: this.state.review
		})
		.catch(function () {
			self.setState({
				numberplate: self.props.plate,
				type: self.state.type,
				review: self.state.review,
				error:"Something went wrong please sign in again"
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
			<label>Enter Your Review</label>
			<select name = "type" onChange={this.handleChange}>
					<option value="driving">Driving</option>
					<option value="parking">Parking</option>
					<option value="car">Car</option>
				</select> <br></br>
				<textarea rows="10" cols="70" name="review" onChange={this.handleChange} /> <br></br>
				<input id="submitButton" type="submit" value="Submit" />
			</form>
			</div>
		);
	}
}

export default Review;
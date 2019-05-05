import React from 'react';
import axios from 'axios';
import PropTypes from 'prop-types';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

class Reviews extends React.Component {
	constructor () {
		super();
		this.state = {
			reviews: [],
		};
	}

	componentDidMount() {
		var self = this;
		var pl = this.props.plate;
		var format = /[ !@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/;
		if (format.test(pl)) {
			window.location = "http://localhost:3000/";
		}
		var url = 'http://localhost:4000/review?plate=' + pl;
		axios.get(url)
		.then(function (response) {
			var s = [];
			for (var i = 0; i < response.data.length; i++) {
				var b = JSON.parse(response.data[i]);
				s.push(b);
			}
			self.setState(
				{reviews:s}
			);
			console.log(self.state);
		})
		.catch(function (error) {
			console.log(error);
		});
	}
	
	render() {
		return (
     <table className='reviewsTable'> 

            {this.state.reviews.map(({username, type, text, created}) => {
              return (
				<div key={created}>
                <tr>
					<td width="40%">{username}</td> 
                    <td width="20%"> { type } </td>
					<td width="40%"> { created } </td>   
				</tr>
				<tr>
                    <td colSpan="3"> { text } </td>           
              </tr>
			</div>
            );})}
        </table>
		);
	}
}

export default Reviews;
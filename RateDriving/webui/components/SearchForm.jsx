import React from 'react';

class SearchForm extends React.Component {
	constructor () {
		super();
		this.state = {
			plate:''
		};
		this.handleChange = this.handleChange.bind(this);
	}

	handleChange (evt) {
		this.setState({ [evt.target.name]: evt.target.value });
	}
	
	render() {
		return (
			<form action="/view">
				<input className="searchText" type="text" name="plate" onChange={this.handleChange} />
				<input className="searchButton" type="submit" value="Submit" />
			</form>
		);
	}
}

export default SearchForm;
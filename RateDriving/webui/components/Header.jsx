import React, { Component } from 'react';
import Link from 'next/link';
import PropTypes from 'prop-types';

class Header extends Component {

	static get propTypes() { 
		return { 
			isLoggedIn: PropTypes.bool
		}; 
	}

	render() {
		let option;
		let option2;
		let option3;
			option = <Link href="/signin"><a>Sign In</a></Link>;
			option2 = <a href='/signup'>Sign Up</a>;
			option3 = <a href='/logout'>Log Out</a>;
		return (
			<div>
				<div className="left">
					<nav>
						<Link href="/">
							<a>Home</a>
						</Link>
						<Link href="/about">
							<a>About</a>
						</Link>
					</nav>
				</div>
				<div className="right">
					<nav>
						{option}
						{option2}
						{option3}
					</nav>
				</div>
			</div>
		);
	}
}

export default Header;

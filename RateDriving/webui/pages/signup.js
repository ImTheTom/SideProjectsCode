import SignUpForm from '../components/SignUpForm';
import Header from '../components/Header';
import Footer from '../components/Footer';
import React from 'react';
import Head from 'next/head';

const SignUpComponent = () => (
	<div>
		<Head>
			<title>Rate my Drive</title>
			<link href="/static/signup.css" rel="stylesheet" />
		</Head>
		<div id="container">
			<div id="header">
				<Header isLoggedIn={false} />
			</div>
			<div id="main">
				<p id="title">Sign Up</p>
			</div>
			<div id="signInForm">
				<SignUpForm />
			</div>
			<div id="footer">
				<Footer />
			</div>
		</div>
	</div>
);

SignUpComponent.displayName = "SignUpComponent";

export default SignUpComponent;
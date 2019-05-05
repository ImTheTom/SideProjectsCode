import Header from '../components/Header';
import Footer from '../components/Footer';
import React from 'react';
import Head from 'next/head';
import SignInForm from '../components/SignInForm';

const SignInComponent = () => (
	<div>
		<Head>
			<title>Rate my Drive</title>
			<link href="/static/signin.css" rel="stylesheet" />
		</Head>
		<div id="container">
			<div id="header">
				<Header isLoggedIn={false} />
			</div>
			<div id="main">
				<p id="title">Sign In</p>
			</div>
			<div id="signInForm">
				<SignInForm />
			</div>
			<div id="footer">
				<Footer />
			</div>
		</div>
	</div>
);

SignInComponent.displayName = "SignInComponent";

export default SignInComponent;

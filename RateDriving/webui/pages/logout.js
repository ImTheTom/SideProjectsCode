import Header from '../components/Header';
import Footer from '../components/Footer';
import React from 'react';
import Head from 'next/head';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

cookies.remove('jwt');

const LogoutComponent = () => (
	<div>
		<Head>
			<title>Rate my Drive</title>
			<link href="/static/index.css" rel="stylesheet" />
		</Head>
		<div id="container">
			<div id="header">
				<Header isLoggedIn={false} />
			</div>
			<div id="main">
				<p id="title">Logged Out</p>
			</div>
			<div id="footer">
				<Footer />
			</div>
		</div>
	</div>
);

LogoutComponent.displayName = "LogoutComponent";

export default LogoutComponent;
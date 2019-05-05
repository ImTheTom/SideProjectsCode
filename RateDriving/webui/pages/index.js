import SearchForm from '../components/SearchForm';
import Header from '../components/Header';
import Footer from '../components/Footer';
import React from 'react';
import Head from 'next/head';

const IndexComponent = () => (
	<div>
		<Head>
			<title>Rate my Driver</title>
			<link href="/static/index.css" rel="stylesheet" />
		</Head>
		<div id="container">
			<div id="header">
				<Header isLoggedIn={false} />
			</div>
			<div id="main">
				<p id="title">Rate my Drive</p>
				<div id="searchForm">
					<SearchForm/>
				</div>
			</div>
			<div id="footer">
				<Footer />
			</div>
		</div>
	</div>
);

IndexComponent.displayName = "IndexComponent";

export default IndexComponent;

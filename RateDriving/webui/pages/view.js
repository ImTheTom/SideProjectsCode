import { withRouter } from 'next/router';
import Header from '../components/Header';
import Footer from '../components/Footer';
import Head from 'next/head';
import Review from '../components/Review';
import Reviews from '../components/Reviews';
import React from 'react';

const Page = withRouter(props => (

<div>
<Head>
	<title>Rate my Drive</title>
	<link href="/static/view.css" rel="stylesheet" />
</Head>
<div id="container">
	<div id="header">
		<Header isLoggedIn={false} />
	</div>
	<div id="main">
	<h1>{props.router.query.plate}</h1>
	</div>
	<div id="reviewTable">
					<Reviews plate={props.router.query.plate}/>
				</div>
	<div id="reviewForm">
					<Review plate={props.router.query.plate}/>
				</div>
	<div id="footer">
		<Footer />
	</div>
</div>
</div>
));

export default Page;

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import PropTypes from 'prop-types';
// import { fetchPage } from '../../actions/pagesAction';
import { Spinner, Heading, Text, Image, Box, Alert } from 'pikcha-frame';

const Quote = props => {
	return (
		<Box>
			<cite>{props.content.quote}</cite>
			<br />
			<span>- {props.content.author}</span>
		</Box>
	);
};

class Page extends Component {
	constructor(props) {
		super(props);
		this.state = {
			variantId: null
		};
	}

	componentDidMount = () => {
		// this.props.dispatch(fetchPage(this.props.match.params.pageSlug));
	};

	componentDidUpdate(prevProps) {
		if (this.props.match.params.pageSlug !== prevProps.match.params.pageSlug) {
			// this.props.dispatch(fetchPage(this.props.match.params.pageSlug));
		}
	}

	createMatrix = () => {
		let body = this.props.pages.page.body;
		let matrix = [];

		Object.keys(body).map((key, index) => {
			if (body[key].type === 'text') {
				matrix.push(<Text key={index} content={body[key]} />);
			} else if (body[key].type === 'heading') {
				matrix.push(<Heading key={index} content={body[key]} />);
			} else if (body[key].type === 'image') {
				matrix.push(<Image key={index} content={body[key]} />);
			} else if (body[key].type === 'quote') {
				matrix.push(<Quote key={index} content={body[key]} />);
			}
			return true;
		});
		return matrix;
	};

	render() {
		const { error, isFetchingPage, page } = this.props.pages;

		if (error) {
			return (
				<Alert kind="danger">
					<Text>This order was archived on March 7, 2017 at 3:12pm EDT.</Text>
				</Alert>
			);
		}

		if (isFetchingPage) {
			return <Spinner />;
		}

		return (
			<article>
				<div>
					<h1>{page.title}</h1>
					{this.createMatrix()}
				</div>
			</article>
		);
	}
}

Page.propTypes = {
	pages: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Page));

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import PropTypes from 'prop-types';
import { fetchPage } from '../../actions/pagesAction';
import Loading from '../Base/Loading';

const Text = props => {
	return <div className="text-lg mb-4 leading-normal" dangerouslySetInnerHTML={{ __html: props.content.text }} />;
};

const Heading = props => {
	const HeadingTag = `${props.content.size}`;

	return <HeadingTag className="mb-4 mt-3">{props.content.heading}</HeadingTag>;
};

const Image = props => {
	return (
		<figure className="text-center mb-4">
			<img src={props.content.image.url} alt={props.content.image.title} />
			<figcaption className="mt-4 italic text-grey-dark">{props.content.caption}</figcaption>
		</figure>
	);
};

const Quote = props => {
	return (
		<div className="mb-4">
			<cite className="text-2xl italic block">{props.content.quote}</cite>
			<br />
			<span className="text-grey-dark mt-4">- {props.content.author}</span>
		</div>
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
		this.props.dispatch(fetchPage(this.props.match.params.pageSlug));
	};

	componentDidUpdate(prevProps) {
		if (this.props.match.params.pageSlug !== prevProps.match.params.pageSlug) {
			this.props.dispatch(fetchPage(this.props.match.params.pageSlug));
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
			return <div>Error!</div>;
		}

		if (isFetchingPage) {
			return <Loading />;
		}

		return (
			<article className="flex justify-center -mx-4">
				<div className="w-full md:w-2/3 px-4 mb-6 md:mb-0">
					<h1 className="text-4xl mb-4">{page.title}</h1>

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

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
// import { fetchProducts } from '../../actions/productsAction';
import ProductItem from '../Products/ProductItem';
import { Spinner, Alert, Heading } from 'pikcha-frame';
import Masonry from 'react-masonry-component';

const imagesLoadedOptions = { backgroundColor: '#eee' }

class Home extends Component {
	componentDidMount() {
		// this.props.dispatch(fetchProducts());
	}

	render() {
		const { error, isFetchingProducts, products } = this.props.products;

		if (error) {
			return (<Alert type="danger">Error: {error}</Alert>);
		}

		if (isFetchingProducts) {
			return <Spinner />;
		}

		return (
			<div>
				<Heading as="h2">Shop</Heading>

				<Masonry
					elementType={'div'}
					updateOnEachImageLoad={true}
					imagesLoadedOptions={imagesLoadedOptions}
				>
					{products.map((product, i) => (
						<ProductItem key={i} product={product} />
					))}
				</Masonry>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Home));

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { fetchProducts } from '../../actions/productsAction';
import ProductItem from '../Products/ProductItem';
import Pagination from '../Base/Pagination';
import Loading from '../Base/Loading';
import Alert from '../Base/Alert';
import Masonry from 'react-masonry-component';

const imagesLoadedOptions = { backgroundColor: '#eee' }

class Home extends Component {
	componentDidMount() {
		this.props.dispatch(fetchProducts());
	}

	render() {
		const { error, isFetchingProducts, products } = this.props.products;

		if (error) {
			return (<Alert type="danger">Error: {error}</Alert>);
		}

		if (isFetchingProducts) {
			return <Loading />;
		}

		return (
			<div>
				<h2 className="text-4xl mb-4">Shop</h2>

				<Masonry
					className={'flex flex-wrap -mx-2'} // default ''
					elementType={'div'} // default 'div'
					updateOnEachImageLoad={true} // default false and works only if disableImagesLoaded is false
					imagesLoadedOptions={imagesLoadedOptions} // default {}
				>
					{products.map((product, i) => (
						<ProductItem key={i} product={product} />
					))}
				</Masonry>

				<Pagination />
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Home));

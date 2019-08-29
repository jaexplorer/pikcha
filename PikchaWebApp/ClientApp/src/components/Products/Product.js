import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import accounting from 'accounting';
// import { fetchProduct } from '../../actions/productsAction';
import AddToCart from './AddToCart';
import { Spinner, Image, Heading } from 'pikcha-frame';
import ChevronDown from 'react-feather/dist/icons/chevron-down';

class Product extends Component {
	constructor(props) {
		super(props);
		this.state = {
			variantId: null,
			variant: null
		};
	}

	componentDidMount = () => {
		// this.props.dispatch(fetchProduct(this.props.match.params.productSlug));
	};

	componentDidUpdate(prevProps) {
		if (this.props.match.params.productSlug !== prevProps.match.params.productSlug) {
			// this.props.dispatch(fetchProduct(this.props.match.params.productSlug));
		}
	}

	componentWillReceiveProps = nextProps => {
		const { product } = nextProps.products;

		if (product.defaultId !== null && this.state.variantId == null) {
			let variant = product.variants.filter(obj => {
				return obj.id === product.defaultId;
			});

			this.setState({
				variantId: product.defaultId,
				variant: variant
			});
		}

	};

	handleChange = event => {
		const { product } = this.props.products;

		if (product.defaultId !== null) {
			let variant = product.variants.filter(obj => {
				return obj.id === event.target.value;
			});

			this.setState({
				variantId: event.target.value,
				variant: variant
			});
		}
	};

	render() {
		const { errorProduct, isFetchingProduct, product } = this.props.products;
		const { variant, variantId } = this.state;

		if (errorProduct) {
			return <div>Error!</div>;
		}

		if (isFetchingProduct) {
			return <Spinner />;
		}

		let price = (
			<div>
				<Heading as="h1">{accounting.formatMoney(product.defaultPrice)}</Heading>
			</div>
		);

		if (variant !== null && variant[0].price !== undefined && variant[0].stock !== undefined) {
			price = (
				<div>
					<Heading as="h4">{accounting.formatMoney(variant[0].price)}</Heading>
					<p>{variant[0].stock} items left in stock</p>
				</div>
			);
		}

		if (variant !== null && variant[0].onSale && variant[0].price !== undefined) {
			price = (
				<div>
					<Heading as="h4">{accounting.formatMoney(variant[0].salePrice)}</Heading>
					<Heading as="h4">
						{accounting.formatMoney(variant[0].price)}
					</Heading>
					<p>{variant[0].stock} items left in stock</p>
				</div>
			);
		}

		return (
			<div itemScope="itemscope" itemType="http://schema.org/Product">
				<meta content={product.title} itemProp="name" />
				<meta content={`/products/${product.slug}`} itemProp="url" />
				<meta content={product.featuredImage.url} itemProp="image" />

				<div>
					<div>
						<Image img={product.featuredImage} />
					</div>
				</div>

				<div>
					<div>
						{product.authors.map((author, id) => (
							<Link key={id} to={`/@${author.id}`}>{author.fullName}</Link>
						))}

						<Heading as="h1">{product.title}</Heading>

						{price}

						<label htmlFor="purchasableId">Choose your frame</label>

						<div>
							<select
								name="purchasableId"
								onChange={this.handleChange}
							>
								{product.variants.map((purchasable, id) => (
									<option
										key={id}
										value={purchasable.id}
										defaultValue={purchasable.id === variantId}
										disabled={purchasable.stock <= 0 && !purchasable.hasUnlimitedStock}
									>
										{purchasable.sku} {purchasable.title}{' '}
										{accounting.formatMoney(purchasable.onSale ? purchasable.salePrice : purchasable.price)}
									</option>
								))}
							</select>

							<div>
								<ChevronDown size={16} />
							</div>
						</div>

						<AddToCart id={variantId} />

						<div>{product.shortDescription }</div>
					</div>
				</div>
			</div>
		);
	}
}

Product.propTypes = {
	product: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Product));

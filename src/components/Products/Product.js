import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import accounting from 'accounting';
import { fetchProduct } from '../../actions/productsAction';
import AddToCart from './AddToCart';
import Loading from '../Base/Loading';
import Image from '../Base/Image';
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
		this.props.dispatch(fetchProduct(this.props.match.params.productSlug));
	};

	componentDidUpdate(prevProps) {
		if (this.props.match.params.productSlug !== prevProps.match.params.productSlug) {
			this.props.dispatch(fetchProduct(this.props.match.params.productSlug));
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
			return <Loading />;
		}

		let price = (
			<div>
				<h4 className="text-2xl md:text-4xl mb-4 font-normal inline-block">{accounting.formatMoney(product.defaultPrice)}</h4>
			</div>
		);

		if (variant !== null && variant[0].price !== undefined && variant[0].stock !== undefined) {
			price = (
				<div>
					<h4 className="text-2xl md:text-4xl mb-4 font-normal inline-block">{accounting.formatMoney(variant[0].price)}</h4>
					<p className="inline-block ml-4 text-base">{variant[0].stock} items left in stock</p>
				</div>
			);
		}

		if (variant !== null && variant[0].onSale && variant[0].price !== undefined) {
			price = (
				<div>
					<h4 className="text-2xl md:text-4xl mb-4 font-normal inline-block mr-2">{accounting.formatMoney(variant[0].salePrice)}</h4>
					<h4 className="text-2xl md:text-4xl mb-4 font-normal line-through inline-block text-grey-dark">
						{accounting.formatMoney(variant[0].price)}
					</h4>
					<p className="inline-block ml-4 text-base">{variant[0].stock} items left in stock</p>
				</div>
			);
		}

		return (
			<div className="flex flex-wrap -mx-6" itemScope="itemscope" itemType="http://schema.org/Product">
				<meta content={product.title} itemProp="name" />
				<meta content={`/products/${product.slug}`} itemProp="url" />
				<meta content={product.featuredImage.url} itemProp="image" />

				<div className="w-full md:w-1/2 px-6">
					<div className="rounded shadow p-2 bg-white">
						<Image img={product.featuredImage} className="rounded" />
					</div>
				</div>

				<div className="w-full md:w-1/2 px-6 relative">
					<div className="sticky pin-t pt-8">

						{product.authors.map((author, id) => (
							<Link key={id} to={`/@${author.id}`} className="mb-4 block text-sm anchor">{author.fullName}</Link>
						))}

						<h1 className="text-2xl mb-4 ">{product.title}</h1>

						{price}

						<label htmlFor="purchasableId" className="form-label">Choose your frame</label>

						<div className="relative">
							<select
								name="purchasableId"
								className="form-input form-select mb-4"
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

							<div className="pointer-events-none absolute pin-y pin-r flex items-center px-2 text-grey-darker">
								<ChevronDown size={16} />
							</div>
						</div>

						<AddToCart id={variantId} />

						<div className="text-base mt-4 leading-normal">{product.shortDescription }</div>
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

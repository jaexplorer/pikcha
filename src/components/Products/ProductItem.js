import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import accounting from 'accounting';
import Image from '../Base/Image';

export default class ProductItem extends Component {
	render() {
		const { product } = this.props;

		let variant = product.variants.filter(obj => {
			return obj.id === product.defaultId;
		});

		let price = <h4 className="font-normal mb-0">{accounting.formatMoney(product.defaultPrice)}</h4>;

		if (variant.length && variant[0].onSale) {
			price = (
				<div>
					<h4 className="font-normal inline-block mr-2 mb-0">{accounting.formatMoney(variant[0].salePrice)}</h4>
					<h4 className="font-normal line-through inline-block text-grey-dark mb-0">
						{accounting.formatMoney(product.defaultPrice)}
					</h4>
				</div>
			);
		}

		return (
			<div className="w-full sm:w-1/2 md:w-1/3 px-2 mb-3">
				<div className="overflow-hidden rounded hover:bg-white hover:shadow p-2">
					<Link to={`/products/${product.slug}`} className="mb-4">
						<Image img={product.featuredImage} className="rounded" />
					</Link>

					<div className="pt-2 px-3">
						<Link to={`/products/${product.slug}`} className="text-black no-underline">
							<h3 className="text-lg mb-2 font-semibold">{product.title}</h3>
							{price}
						</Link>
					</div>
				</div>
			</div>
		);
	}
}

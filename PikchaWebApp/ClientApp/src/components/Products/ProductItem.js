import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import accounting from 'accounting';
import { Heading, Image } from 'pikcha-frame';

export default class ProductItem extends Component {
	render() {
		const { product } = this.props;

		let variant = product.variants.filter(obj => {
			return obj.id === product.defaultId;
		});

		let price = <Heading as="h4">{accounting.formatMoney(product.defaultPrice)}</Heading>;

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
			<div>
				<div>
					<Link to={`/products/${product.slug}`}>
						<Image img={product.featuredImage} />
					</Link>

					<div>
						<Link to={`/products/${product.slug}`}>
							<Heading as="h3">{product.title}</Heading>
							{price}
						</Link>
					</div>
				</div>
			</div>
		);
	}
}

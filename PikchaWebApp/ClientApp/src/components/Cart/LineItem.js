import React, { Component } from 'react';
import PropTypes from 'prop-types';
import accounting from 'accounting';
import RemoveItem from './RemoveItem';
import QtyInput from './QtyInput';
import { Link } from 'react-router-dom';
import { Image } from 'pikcha-frame';

class LineItem extends Component {
	render() {
		const { lineItem } = this.props;

		return (
			<div className="px-4 py-2">
				<div className="flex flex-wrap items-center">
					<div className="flex flex-grow">
						<div className="mr-4">
							<Link to={`products/${lineItem.snapshot.slug}`} className="block">
							<Image img={lineItem.snapshot.fields.featuredImage} className="rounded w-16" />
							</Link>
						</div>

						<div className="flex-grow mt-2">
							<h3 className="text-base md:text-lg">
								<Link
									to={`products/${lineItem.snapshot.slug}`}
									className="text-black no-underline hover:text-blue mb-2 block break-words"
								>
									{lineItem.snapshot.title}
								</Link>

								<div className="font-normal text-base hidden md:block">{accounting.formatMoney(lineItem.price)}</div>
								<div className="font-normal text-base inline-block md:hidden">{accounting.formatMoney(lineItem.total)}</div>
							</h3>
						</div>
					</div>

					<div className="flex ml-0 md:ml-4 mt-4 md:mt-0 items-center">
						<div className="font-semibold mr-4 hidden md:inline-block">{accounting.formatMoney(lineItem.total)}</div>

						<QtyInput lineItem={lineItem} className="mr-4" />
						<RemoveItem id={lineItem.id} />
					</div>

				</div>
			</div>
		);
	}
}

LineItem.propTypes = {
	lineItem: PropTypes.object.isRequired
};

export default LineItem;

import React, { Component } from 'react';
import PropTypes from 'prop-types';
import accounting from 'accounting';

class LineItem extends Component {
	render() {
		const { lineItem } = this.props;

		return (
			<div className="py-2 border-t border-grey-light">
				<div className="flex items-center">
					<div className="relative mr-4">
						<img
							className="rounded w-16"
							src={lineItem.snapshot.fields.featuredImage.url}
							alt={lineItem.snapshot.title}
						/>

						<span className="absolute pin-t pin-r text-white bg-blue text-xs rounded-full flex items-center px-1 text-center justify-center font-semibold -mt-1 -mr-1">{lineItem.qty}</span>
					</div>

					<h3 className="text-base flex-grow">{lineItem.snapshot.title}</h3>

					<div className="ml-4 text-right">
						<div>{accounting.formatMoney(lineItem.total)}</div>
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

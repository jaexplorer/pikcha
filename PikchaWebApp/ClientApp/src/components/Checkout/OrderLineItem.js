import React, { Component } from 'react';
import PropTypes from 'prop-types';
import accounting from 'accounting';

class LineItem extends Component {
	render() {
		const { lineItem } = this.props;

		return (
			<div>
				<div>
					<div>
						<img
							src={lineItem.snapshot.fields.featuredImage.url}
							alt={lineItem.snapshot.title}
						/>

						<span>{lineItem.qty}</span>
					</div>

					<h3>{lineItem.snapshot.title}</h3>

					<div>
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

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
			<div>
				<div>
					<div>
						<div>
							<Link to={`products/${lineItem.snapshot.slug}`}>
							<Image img={lineItem.snapshot.fields.featuredImage} />
							</Link>
						</div>

						<div>
							<h3>
								<Link
									to={`products/${lineItem.snapshot.slug}`}
								>
									{lineItem.snapshot.title}
								</Link>

								<div>{accounting.formatMoney(lineItem.price)}</div>
								<div>{accounting.formatMoney(lineItem.total)}</div>
							</h3>
						</div>
					</div>

					<div>
						<div>{accounting.formatMoney(lineItem.total)}</div>
						<QtyInput lineItem={lineItem} />
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

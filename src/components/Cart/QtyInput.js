import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { updateCart } from '../../actions/cartAction';
import { Button } from 'pikcha-frame';
import PlusIcon from 'react-feather/dist/icons/plus';
import MinusIcon from 'react-feather/dist/icons/minus';

class QtyInput extends Component {
	render() {
		const { lineItem, className } = this.props;

		return (
			<div className={`flex items-center ${className}`}>
				<Button
					onClick={() => {
						this.props.dispatch(updateCart(lineItem.id, parseInt(lineItem.qty) - 1, true));
					}}
					ghost
					size="sm"
				>
					<MinusIcon size={16} />
					<div className="sr-only">Decrease item</div>
				</Button>

				<div className="w-8 text-center">{lineItem.qty}</div>

				<Button
					onClick={() => {
						this.props.dispatch(updateCart(lineItem.id, parseInt(lineItem.qty) + 1, true));
					}}
					ghost
					size="sm"
				>
					<PlusIcon size={16} />
					<div className="sr-only">Increase item</div>
				</Button>
			</div>
		);
	}
}

QtyInput.propTypes = {
	lineItem: PropTypes.object.isRequired,
	className: PropTypes.string
};

export default withRouter(connect()(QtyInput));

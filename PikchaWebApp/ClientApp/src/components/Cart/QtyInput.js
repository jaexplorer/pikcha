import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
// import { updateCart } from '../../actions/cartAction';
import { Button } from 'pikcha-frame';
import PlusIcon from 'react-feather/dist/icons/plus';
import MinusIcon from 'react-feather/dist/icons/minus';

class QtyInput extends Component {
	render() {
		const { lineItem } = this.props;

		return (
			<div>
				<Button
					// onClick={() => {
					// 	this.props.dispatch(updateCart(lineItem.id, parseInt(lineItem.qty) - 1, true));
					// }}
					ghost
					size="sm"
				>
					<MinusIcon size={16} />
					<div>Decrease item</div>
				</Button>

				<div>{lineItem.qty}</div>

				<Button
					// onClick={() => {
					// 	this.props.dispatch(updateCart(lineItem.id, parseInt(lineItem.qty) + 1, true));
					// }}
					ghost
					size="sm"
				>
					<PlusIcon size={16} />
					<div>Increase item</div>
				</Button>
			</div>
		);
	}
}

QtyInput.propTypes = {
	lineItem: PropTypes.object.isRequired,
};

export default withRouter(connect()(QtyInput));

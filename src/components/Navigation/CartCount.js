import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'

const CartCount = props => {
	let count = 0;

	if (props.count === undefined) {
		count = 0;
	}

	count = props.count.totalQty;

	return (
		<span className="absolute pin-t pin-r text-white bg-blue text-xs rounded-full flex items-center px-1 text-center justify-center font-semibold">
			{count}
		</span>
	);
};

const mapStateToProps = state => ({
	count: state.cart.cart
});

export default withRouter(connect(mapStateToProps)(CartCount));

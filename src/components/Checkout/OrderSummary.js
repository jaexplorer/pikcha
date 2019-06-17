import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
import accounting from 'accounting';
import OrderLineItem from './OrderLineItem';
import Adjustment from './Adjustment';
import { Spinner, Alert } from 'pikcha-frame';

class OrderSummary extends Component {
	render() {
		const { error, isFetchingCart, cart } = this.props.cart;

		if (error) {
			return (<Alert type="danger">Error: {error}</Alert>);
		}

		if (isFetchingCart) {
			return <Spinner />;
		}

		let cartContent = (
			<p>
				You have no items in your cart, add some on the <Link to="/">products</Link> page.
			</p>
		);

		if (Object.keys(cart.lineItems).length > 0) {
			cartContent = (
				<div className="shadow bg-white p-4 rounded">
					<h3 className="mb-4">Order summary</h3>

					{Object.keys(cart.lineItems).map((key, index) => (
						<OrderLineItem key={index} lineItem={cart.lineItems[key]} />
					))}

					<hr className="hr mt-0" />

					<div className="mt-4">
						<div className="flex justify-between mb-4">
							<div>Subtotal</div>

							<div>{accounting.formatMoney(cart.itemSubtotal)}</div>
						</div>

						{Object.keys(cart.adjustments).map((key, index) => (
							<Adjustment key={index} adjustment={cart.adjustments[key]} />
						))}

						<div className="flex justify-between">
							<div className="text-lg font-semibold">Total: </div>
							<div className="text-lg">{accounting.formatMoney(cart.totalPrice)}</div>
						</div>
					</div>
				</div>
			);
		}

		return <div>{cartContent}</div>;
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(OrderSummary));

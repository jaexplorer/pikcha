import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
import accounting from 'accounting';
import LineItem from './LineItem';
import Adjustment from '../Checkout/Adjustment';
import { Button, Alert, Spinner } from 'pikcha-frame';

class Cart extends Component {
	render() {
		const { error, isFetchingCart, cart } = this.props.cart;

		if (error) {
			return (<Alert type="danger">Error: {error}</Alert>);
		}

		if (isFetchingCart && cart.length === 0) {
			return <Spinner />;
		}

		let cartContent = (
			<p>
				You have no items in your cart, add some on the <Link to="/" className="anchor">shop</Link> page.
			</p>
		);

		if (Object.keys(cart.lineItems).length > 0) {
			cartContent = (
				<div>
					<div className="bg-white rounded shadow mb-6 py-2">
						{Object.keys(cart.lineItems).map((key, index) => (
							<LineItem key={index} lineItem={cart.lineItems[key]} />
						))}
					</div>

					<div className="flex items-center">
						<div className="flex-grow">
							<div>
								<span className="text-lg font-semibold">Subtotal: </span>
								<span className="text-lg">{accounting.formatMoney(cart.itemSubtotal)}</span>
							</div>

							{Object.keys(cart.adjustments).map((key, index) => (
								<Adjustment key={index} adjustment={cart.adjustments[key]} />
							))}
						</div>

						<div>
							<Button to="/checkout-email" size="large" type="primary">
								Checkout
							</Button>
						</div>
					</div>
				</div>
			);
		}

		return (
			<div style={{ opacity: isFetchingCart ? 0.5 : 1 }} className="max-w-md mx-auto">
				<h1 className="text-2xl mb-5">Shopping cart</h1>

				{cartContent}
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Cart));

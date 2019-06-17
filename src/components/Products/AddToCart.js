import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { updateCart } from '../../actions/cartAction';
import { Button } from 'pikcha-frame';

class AddToCart extends Component {
	state = {
		isLoading: false
		// active: false
	};

	_addToCart = () => {
		if (!this.props.cart.updateLoading) {
			this.props.dispatch(updateCart(this.props.id, 1));
		}

		this._toggleModal();
	};

	_toggleModal = () => {
		this.setState(({ active }) => ({ active: !active }));
	};

	render() {
		const { error, isFetchingCart } = this.props.cart;
		const { price } = this.props;
		// const { active } = this.state;

		let text = 'Add To Cart';

		if (error) {
			text = "Error!"
		}

		if (price !== undefined) {
			text = price + ' - ' + text;
		}

		return (
			<Button onClick={this._addToCart} loading={isFetchingCart} type="primary" size="large" block>
				{text}
			</Button>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(AddToCart));

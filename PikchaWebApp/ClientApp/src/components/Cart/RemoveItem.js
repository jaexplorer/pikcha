import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import PropTypes from 'prop-types';
// import { updateCart } from '../../actions/cartAction';
import { Button } from 'pikcha-frame';
import X from 'react-feather/dist/icons/x';

class RemoveItem extends Component {
	render() {
		return (
			<div>
				<Button
					// onClick={() => {
					// 	this.props.dispatch(updateCart(this.props.id, undefined, true));
					// }}
					ghost
					size="sm"
				>
					<X size={16} />
					<div className="sr-only">Remove item</div>
				</Button>
			</div>
		);
	}
}

RemoveItem.propTypes = {
	id: PropTypes.string.isRequired
};

export default withRouter(connect()(RemoveItem));

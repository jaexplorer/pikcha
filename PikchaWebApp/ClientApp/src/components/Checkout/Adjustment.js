import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class Adjustment extends Component {
	render() {
		const { adjustment } = this.props;

		return (
			<div>
				<div>{adjustment.name}</div>
				<div>{adjustment.amount}</div>
			</div>
		);
	}
}

Adjustment.propTypes = {
	adjustment: PropTypes.array.isRequired
};

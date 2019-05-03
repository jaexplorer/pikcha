import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class TextField extends Component {
	render() {
		const { disabled, id, label, placeholder, value, onChange, type } = this.props;

		return (
			<div className="mb-4">
				<label className="block text-sm font-bold mb-2">{label}</label>
				<input
					type={type}
					placeholder={placeholder}
					value={value}
					onChange={onChange}
					className="appearance-none border rounded w-full py-2 px-3 focus:outline-none"
					disabled={disabled}
					id={id}
				/>
			</div>
		);
	}
}

TextField.propTypes = {
	disabled: PropTypes.bool,
	id: PropTypes.string,
	label: PropTypes.string.isRequired,
	placeholder: PropTypes.string,
	value: PropTypes.string,
	onChange: PropTypes.func,
	type: PropTypes.string
};

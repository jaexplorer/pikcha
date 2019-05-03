import React, { Component } from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { Link } from 'react-router-dom';

export default class Button extends Component {
	render() {
		const { to, type, htmlType, disabled, size, className, children, loading, block, onClick } = this.props;

		let content = children;

		var btnClass = classNames({
			btn: true,
			'btn-block': block,
			[`btn-${type}`]: type !== undefined,
			[`btn-${size}`]: size !== undefined,
			'spinner': loading,
			[className]: className !== undefined
		});

		if (to !== undefined) {
			return (
				<Link to={to} className={btnClass}>
					{content}
				</Link>
			);
		}

		return (
			<button onClick={onClick} type={htmlType} disabled={disabled} className={btnClass}>
				{content}
			</button>
		);
	}
}

Button.propTypes = {
	type: PropTypes.string,
	shape: PropTypes.string,
	size: PropTypes.string,
	className: PropTypes.string,
	icon: PropTypes.string,
	ghost: PropTypes.bool,
	loading: PropTypes.bool,
	block: PropTypes.bool,
	onClick: PropTypes.func
};

import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

export default class SubNav extends Component {
	render() {
		return (
			<div>
				<ul>
					<li>
						<NavLink to="/checkout-email" activeClassName="active">
							Email
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-info" activeClassName="active">
							Account
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-shipping" activeClassName="active">
							Address
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-payment" activeClassName="active">
							Payment Method
						</NavLink>
					</li>
				</ul>
			</div>
		);
	}
}

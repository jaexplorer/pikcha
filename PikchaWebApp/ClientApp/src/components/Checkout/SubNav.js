import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

export default class SubNav extends Component {
	render() {
		return (
			<div>
				<ul>
					<li>
						<NavLink to="/checkout-email" >
							Email
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-info" >
							Account
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-shipping" >
							Address
						</NavLink>
					</li>
					<li>
						<NavLink to="/checkout-payment" >
							Payment Method
						</NavLink>
					</li>
				</ul>
			</div>
		);
	}
}

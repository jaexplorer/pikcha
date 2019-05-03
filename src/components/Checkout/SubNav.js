import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

export default class SubNav extends Component {
	render() {
		return (
			<div className="w-full mb-6">
				<ul className="list-reset flex sub-nav">
					<li className="sub-nav-item">
						<NavLink to="/checkout-email" className="sub-nav-link" activeClassName="active">
							Email
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/checkout-info" className="sub-nav-link" activeClassName="active">
							Account
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/checkout-shipping" className="sub-nav-link" activeClassName="active">
							Address
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/checkout-payment" className="sub-nav-link" activeClassName="active">
							Payment Method
						</NavLink>
					</li>
				</ul>
			</div>
		);
	}
}

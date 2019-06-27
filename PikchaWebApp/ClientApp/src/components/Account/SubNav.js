import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
// import { logout } from '../../actions/usersAction';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'

class SubNav extends Component {
	constructor(props) {
		super(props)

		this.handleClick = this.handleClick.bind(this)
	}

	handleClick() {
		// this.props.dispatch(logout());
	}

	render() {
		return (
			<div className="w-full mb-6">
				<ul className="list-reset flex sub-nav">
					<li className="sub-nav-item">
						<NavLink to="/account" className="sub-nav-link" activeClassName="active">
							General
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/account-orders" className="sub-nav-link" activeClassName="active">
							Orders
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/account-address" className="sub-nav-link" activeClassName="active">
							Address
						</NavLink>
					</li>
					<li className="sub-nav-item">
						<NavLink to="/account-payment" className="sub-nav-link" activeClassName="active">
							Payment
						</NavLink>
					</li>

					<li className="sub-nav-item">
						<span onClick={this.handleClick.bind(this)} className="sub-nav-link cursor-pointer">
							Sign out
						</span>
					</li>
				</ul>
			</div>
		);
	}
}

export default withRouter(connect()(SubNav));

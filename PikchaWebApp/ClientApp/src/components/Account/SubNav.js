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
			<div>
				<ul>
					<li>
						<NavLink to="/account">
							General
						</NavLink>
					</li>
					<li>
						<NavLink to="/account-orders">
							Orders
						</NavLink>
					</li>
					<li>
						<NavLink to="/account-address">
							Address
						</NavLink>
					</li>
					<li>
						<NavLink to="/account-payment">
							Payment
						</NavLink>
					</li>

					<li>
						<span onClick={this.handleClick.bind(this)}>
							Sign out
						</span>
					</li>
				</ul>
			</div>
		);
	}
}

export default withRouter(connect()(SubNav));

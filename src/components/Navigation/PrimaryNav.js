import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { NavLink, Link } from 'react-router-dom';
import classNames from 'classnames';
import Search from './Search';
import CartCount from './CartCount';
import CartIcon from 'react-feather/dist/icons/shopping-cart';
import UserIcon from 'react-feather/dist/icons/user';
import SearchIcon from 'react-feather/dist/icons/search';
import MenuIcon from 'react-feather/dist/icons/menu';

class PrimaryNav extends Component {
	constructor(props) {
		super(props)

		this.state = {
			isToggled: false
		};

		this.handleToggle = this.handleToggle.bind(this)
	}

	handleToggle = () => {
		this.setState({ isToggled: !this.state.isToggled });
	}

	componentDidUpdate(prevProps) {
		if (this.props.location !== prevProps.location) {
			this.setState({ isToggled: false });
		}
	}

	render() {
		const { isToggled } = this.state;

		const NavList = (
			<ul className="list-reset flex primary-nav flex-col lg:flex-row">
				<li className="primary-nav-item">
					<NavLink exact to="/" className="primary-nav-link mr-6" activeClassName="active">
						Shop
					</NavLink>
				</li>
				<li className="primary-nav-item">
					<NavLink to="/pages/about" className="primary-nav-link mr-6" activeClassName="active">
						About
					</NavLink>
				</li>
				<li className="primary-nav-item">
					<NavLink to="/pages/contact" className="primary-nav-link mr-6" activeClassName="active">
						Contact
					</NavLink>
				</li>
				<li className="primary-nav-item">
					<NavLink exact to="/upload" className="primary-nav-link" activeClassName="active">
						Upload
					</NavLink>
				</li>
			</ul>
		)

		const smallNavList = classNames({
			'block lg:hidden w-full mt-2': true,
			'hidden': !isToggled,
		});


		return (
			<div className="w-full bg-white shadow">
				<nav className="container flex items-center justify-between flex-wrap py-3">
					<div className="flex items-center">
						<div className="flex items-baseline">
							<h1 className="text-lg my-0 leading-normal" itemScope="" itemType="http://schema.org/Organization">
								<Link to="/" className="text-black no-underline text-lg mr-6">
									Pikcha
							</Link>
							</h1>

							<div className="hidden lg:block">
								{NavList}
							</div>
						</div>
					</div>

					<div className="flex items-center">
						<div className="hidden lg:block">
							<Search />
						</div>

						<NavLink to="/search" className="primary-nav-link ml-6 inline-block lg:hidden" activeClassName="active">
							<SearchIcon />
							<div className="sr-only">Search</div>
						</NavLink>

						<NavLink to="/account" className="primary-nav-link ml-6" activeClassName="active">
							<UserIcon />
							<div className="sr-only">Account</div>
						</NavLink>

						<NavLink to="/cart" className="primary-nav-link ml-6" activeClassName="active">
							<div className="relative pr-2">
								<CartIcon />
								<div className="sr-only">Cart</div>
								<CartCount />
							</div>
						</NavLink>

						<span className="primary-nav-link cursor-pointer ml-4 block lg:hidden" onClick={this.handleToggle.bind(this)}>
							<MenuIcon />
							<div className="sr-only">Menu</div>
						</span>
					</div>

					<div className={smallNavList}>
						{NavList}
					</div>
				</nav>
			</div>
		)
	}
}

export default withRouter(connect()(PrimaryNav));

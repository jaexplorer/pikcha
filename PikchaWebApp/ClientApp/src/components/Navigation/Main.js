import React from 'react';
import { Route, Switch, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { Spinner } from 'pikcha-frame';
import Home from '../Pages/Home';
import Page from '../Pages/Page';
import Search from '../Pages/Search';
import Product from '../Products/Product';
import Cart from '../Cart/Cart';
import CheckoutEmail from '../Checkout/CheckoutEmail';
import CheckoutInfo from '../Checkout/CheckoutInfo';
import CheckoutShipping from '../Checkout/CheckoutShipping';
import CheckoutPayment from '../Checkout/CheckoutPayment';
import User from '../User/User';
import Upload from '../Account/Upload';
import Account from '../Account/Account';
import AccountOrders from '../Account/AccountOrders';
import AccountAddress from '../Account/AccountAddress';
import AccountPayment from '../Account/AccountPayment';
import SignIn from '../Auth/SignIn';
import SignUp from '../Auth/SignUp';
import ForgotPassword from '../Auth/ForgotPassword';
import SetPassword from '../Auth/SetPassword';
import NoMatch from './NoMatch';

const PrivateRoute = ({ component: Component, authed, ...rest }) => (
	<Route
		{...rest}
		render={props =>
			authed === true ? (
				<Component {...props} />
			) : (
				<Redirect
					to={{
						pathname: '/signin',
						state: { from: props.location }
					}}
				/>
			)
		}
	/>
);

const CheckoutRoute = ({ component: Component, authed, ...rest }) => (
	<Route
		{...rest}
		render={props =>
			authed === false ? (
				<Component {...props} />
			) : (
				<Redirect
					to={{
						pathname: '/checkout-info',
						state: { from: props.location }
					}}
				/>
			)
		}
	/>
);

const Main = props => {
	if (props.signedIn === undefined) {
		return (
			<main>
				<Spinner />
			</main>
		);
	}

	if (props.isFetching) {
		return (
			<main>
				<Spinner />
			</main>
		);
	}

	return (
		<main>
			<Switch>
				<Route exact path="/" component={Home} />
				<Route path="/pages/:pageSlug" component={Page} />
				<Route path="/search" component={Search} />

				<Route path="/products/:productSlug" component={Product} />

				<Route path="/cart" component={Cart} />

				<CheckoutRoute path="/checkout-email" component={CheckoutEmail} authed={props.signedIn} />
				<Route path="/checkout-info" component={CheckoutInfo} />
				<Route path="/checkout-shipping" component={CheckoutShipping} />
				<Route path="/checkout-payment" component={CheckoutPayment} />

				<Route path="/@:userId" component={User} />

				<PrivateRoute path="/upload" component={Upload} authed={props.signedIn} />

				<PrivateRoute path="/account" component={Account} authed={props.signedIn} />
				<PrivateRoute path="/account-orders" component={AccountOrders} authed={props.signedIn} />
				<PrivateRoute path="/account-address" component={AccountAddress} authed={props.signedIn} />
				<PrivateRoute path="/account-payment" component={AccountPayment} authed={props.signedIn} />

				<Route path="/signin" component={SignIn} />
				<Route path="/signup" component={SignUp} />
				<Route path="/forgot-password" component={ForgotPassword} />
				<Route path="/set-password" component={SetPassword} />

				<Route component={NoMatch} />
			</Switch>
		</main>
	);
};

const mapStateToProps = state => ({
	signedIn: state.users.signedIn,
	isFetching: state.users.isFetchingCurrent
});

export default withRouter(connect(mapStateToProps)(Main));

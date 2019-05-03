import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import PrimaryNav from './Navigation/PrimaryNav';
import Main from './Navigation/Main';
import Footer from './Navigation/Footer';
import { getCart } from '../actions/cartAction';
import { getIdentity } from '../actions/usersAction';

class App extends Component {
	componentDidMount() {
		this.props.dispatch(getIdentity());
		this.props.dispatch(getCart());
	}

	render() {
		return (
			<Fragment>
				<div className="main-content">
					<PrimaryNav />
					<Main />
				</div>

				<Footer />
			</Fragment>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(App));

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import styled from 'styled-components'
import TopBar from './Navigation/TopBar';
import Main from './Navigation/Main';
import Footer from './Navigation/Footer';
// import { getCart } from '../actions/cartAction';
// import { getIdentity } from '../actions/usersAction';

const AppWrap = styled.div`
    background-color: #f9fafb;
`

const MainSection = styled.section`
    /* padding: 12px 0 16px; */
`

class App extends Component {
	componentDidMount() {
		// this.props.dispatch(getIdentity());
		// this.props.dispatch(getCart());
	}

	render() {
		return (
			<AppWrap>
				<MainSection>
					<TopBar />
					<Main />
				</MainSection>

				<Footer />
			</AppWrap>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(App));

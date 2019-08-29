import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import PropTypes from 'prop-types';
// import { fetchUser } from '../../actions/usersAction';
import { Spinner, Heading } from 'pikcha-frame';

class User extends Component {
	componentDidMount() {
		// this.props.dispatch(fetchUser(this.props.match.params.userId));
	}

	componentDidUpdate(prevProps) {
		if (this.props.match.params.userId !== prevProps.match.params.userId) {
			// this.props.dispatch(fetchUser(this.props.match.params.userId));
		}
	}

	render() {
		const { errorUser, isFetchingUser, user } = this.props.users;

		if (errorUser) {
			return <div>Error!</div>;
		}

		if (isFetchingUser) {
			return <Spinner />;
		}

		return (
			<div>
				<Heading as="h1">{user.fullName}</Heading>
			</div>
		);
	}
}

User.propTypes = {
	user: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(User));

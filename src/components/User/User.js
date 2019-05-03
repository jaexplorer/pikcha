import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import PropTypes from 'prop-types';
import { fetchUser } from '../../actions/usersAction';
import Loading from '../Base/Loading';

class User extends Component {
	componentDidMount() {
		this.props.dispatch(fetchUser(this.props.match.params.userId));
	}

	componentDidUpdate(prevProps) {
		if (this.props.match.params.userId !== prevProps.match.params.userId) {
			this.props.dispatch(fetchUser(this.props.match.params.userId));
		}
	}

	render() {
		const { errorUser, isFetchingUser, user } = this.props.users;

		if (errorUser) {
			return <div>Error!</div>;
		}

		if (isFetchingUser) {
			return <Loading />;
		}

		return (
			<div>
				<h1 className="text-2xl">{user.fullName}</h1>
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

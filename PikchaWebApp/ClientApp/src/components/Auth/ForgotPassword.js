import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
// import { forgotPassword } from '../../actions/usersAction';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { Heading, Anchor } from 'pikcha-frame';

class ForgotPassword extends Component {
	render() {
		return (
			<div>
				<Formol>
					<Heading>Forgot your password?</Heading>

					<Field required type="email" name="email" placeholder="e.g., john.doe@gmail.com">
						Email address
					</Field>

					<Anchor as={Link} to="/signin">Return to sign in</Anchor>
				</Formol>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(ForgotPassword));

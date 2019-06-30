import React, { Component } from 'react';
import { connect } from 'react-redux';
// import { signIn } from '../../actions/usersAction';
import { withRouter } from 'react-router-dom'
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { Link } from 'react-router-dom';
import { Heading, Anchor, Text } from 'pikcha-frame';

class SignIn extends Component {
	render() {
		return (
			<div>
				<Formol>
					<div>
						<Heading as="h1">Sign in</Heading>
						<Text>or <Anchor as={Link} to="/signup">create an account</Anchor></Text>
					</div>

					<Field required type="email" name="email" placeholder="e.g., john.doe@gmail.com">
						Email address
					</Field>

					<Field type="password" required placeholder="e.g., ••••••••••••">
						Password
					</Field>

					<Anchor as={Link} to="forgot-password">
						Forgot password?
					</Anchor>
				</Formol>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(SignIn));

import React, { Component } from 'react';
import { connect } from 'react-redux';
// import { saveUser } from '../../actions/usersAction';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
import { Heading, Text, Anchor } from 'pikcha-frame';

class SignUp extends Component {
	render() {
		return (
			<div>
				<Formol>
					<div>
						<Heading as="h1">Sign up</Heading>
						<Text>or <Anchor as={Link} to="/signin">sign in to your account</Anchor></Text>
					</div>

					<Field autoFocus required name="firstname" title="Your first name" placeholder="e.g., Jane" >
						First name
					</Field>

					<Field autoFocus required name="lastname" title="Your last name" placeholder="e.g., Doe" >
						Last name
					</Field>

					<Field required type="email" name="email" placeholder="e.g., john.doe@gmail.com">
						Email address
					</Field>

					<Field type="password" required placeholder="e.g., ••••••••••••">
						Password
					</Field>
				</Formol>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(SignUp));

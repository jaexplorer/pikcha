import React, { Component } from 'react';
import { connect } from 'react-redux';
// import { saveUser } from '../../actions/usersAction';
import { withRouter } from 'react-router-dom'
import PropTypes from 'prop-types';
import { Heading, Button, Alert, Divider } from 'pikcha-frame';
import SubNav from './SubNav';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'

class Account extends Component {
	render() {
		const { error, isFetchingCurrent, currentUser } = this.props.users;

		return (
			<div>
				<div>
					<SubNav />

					<Heading>Account settings</Heading>

					<Formol
						initialValues={{
							firstName: currentUser.firstName,
							lastName: currentUser.lastName,
							email: currentUser.email,
							password: '',
							newPassword: ''
						}}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							// this.props.dispatch(saveUser(values, currentUser.id));
						}}
					>
						<Divider />

						<Heading>Edit your details</Heading>

						{error &&
							<Alert type="danger">Error: {error}</Alert>
						}

						<div>
							<div>
								<label>First name</label>
								<Field name="firstName" placeholder="e.g., Jane" />
							</div>

							<div>
								<label>Last name</label>
								<Field name="lastName" placeholder="e.g., Doe" />
							</div>
						</div>

						<div>
							<label>Email</label>
							<Field name="email" placeholder="e.g., john.doe@gmail.com" />
						</div>

						<Divider />

						<h3>Update password</h3>

						<div>
							<div>
								<label>Current password</label>
								<Field name="password" placeholder="e.g., ••••••••••••" />
								<p>Required when changing password or email.</p>
							</div>

							<div>
								<label>New password</label>
								<Field name="newPassword" placeholder="e.g., ••••••••••••" />
								<p>Leave blank if you don't want to change.</p>
							</div>
						</div>

						<div>
							{/* <Button htmlType="submit" type="primary" disabled={isSubmitting}>
								Update info
							</Button> */}
						</div>
					</Formol>
				</div>
			</div>
		);
	}
}

Account.propTypes = {
	currentUser: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Account));

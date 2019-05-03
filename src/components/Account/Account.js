import React, { Component } from 'react';
import { connect } from 'react-redux';
import { saveUser } from '../../actions/usersAction';
import { withRouter } from 'react-router-dom'
import PropTypes from 'prop-types';
import Button from '../Base/Button';
import SubNav from './SubNav';
import Alert from '../Base/Alert';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';

const AccountSchema = Yup.object().shape({
	firstName: Yup.string()
		.min(2, 'Too Short!')
		.max(50, 'Too Long!')
		.required('Required'),
	lastName: Yup.string()
		.min(2, 'Too Short!')
		.max(50, 'Too Long!')
		.required('Required'),
	email: Yup.string()
		.email('Invalid email')
		.required('No email provided.'),
});

class Account extends Component {
	render() {
		const { error, isFetchingCurrent, currentUser } = this.props.users;

		return (
			<div className="flex flex-wrap -mx-2" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-full px-2">
					<SubNav />

					<h1 className="text-2xl mb-4">Account settings</h1>

					<Formik
						initialValues={{
							firstName: currentUser.firstName,
							lastName: currentUser.lastName,
							email: currentUser.email,
							password: '',
							newPassword: ''
						}}
						validationSchema={AccountSchema}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							this.props.dispatch(saveUser(values, currentUser.id));
						}}
						render={({ isSubmitting }) => (
							<Form>
								<hr className="hr"/>

								<h3 className="text-xl mb-4">Edit your details</h3>

								{error &&
									<Alert type="danger">Error: {error}</Alert>
								}

								<div className="flex -mx-2">
									<div className="w-1/2 px-2 form-group">
										<label className="form-label">First name</label>
										<Field name="firstName" placeholder="e.g., Jane" className="form-input" />
										<ErrorMessage name="firstName" component="div" className="invalid-feedback" />
									</div>

									<div className="w-1/2 px-2 form-group">
										<label className="form-label">Last name</label>
										<Field name="lastName" placeholder="e.g., Doe" className="form-input" />
										<ErrorMessage name="lastName" component="div" className="invalid-feedback" />
									</div>
								</div>

								<div className="form-group">
									<label className="form-label">Email</label>
									<Field name="email" placeholder="e.g., john.doe@gmail.com" className="form-input" />
									<ErrorMessage name="email" component="div" className="invalid-feedback" />
								</div>

								<hr className="hr"/>

								<h3 className="text-xl mb-4">Update password</h3>

								<div className="flex -mx-2">
									<div className="w-1/2 px-2 form-group">
										<label className="form-label">Current password</label>
										<Field name="password" placeholder="e.g., ••••••••••••" className="form-input" />
										<ErrorMessage name="password" component="div" className="invalid-feedback" />
										<p className="form-text">Required when changing password or email.</p>
									</div>

									<div className="w-1/2 px-2 form-group">
										<label className="form-label">New password</label>
										<Field name="newPassword" placeholder="e.g., ••••••••••••" className="form-input" />
										<ErrorMessage name="newPassword" component="div" className="invalid-feedback" />
										<p className="form-text">Leave blank if you don't want to change.</p>
									</div>
								</div>

								<div className="flex items-center justify-between">
									<Button htmlType="submit" type="primary" disabled={isSubmitting}>
										Update info
									</Button>
								</div>
							</Form>
						)}
					/>
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

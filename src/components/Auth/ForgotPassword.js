import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
import { forgotPassword } from '../../actions/usersAction';
import Formol, { Field } from 'formol'
import * as Yup from 'yup';
import { Button, Alert } from 'pikcha-frame';

const ForgotPasswordSchema = Yup.object().shape({
	email: Yup.string()
		.email('Invalid email')
		.required('No email provided.')
});

class ForgotPassword extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		return (
			<div className="flex -mx-2 justify-center md:mt-6" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-full md:w-2/3 lg:w-1/2 px-2">
					<Formik
						initialValues={{ email: '' }}
						validationSchema={ForgotPasswordSchema}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							this.props.dispatch(forgotPassword(values));
						}}
						render={({ isSubmitting }) => (
							<Form>
								<h1 className="text-2xl mb-6">Forgot your password?</h1>

								{error &&
									<Alert type="danger">Error: {error}</Alert>
								}

								<div className="form-group">
									<label className="form-label">Email address</label>
									<Field
										type="email"
										name="email"
										placeholder="e.g., john.doe@gmail.com"
										className="form-input"
									/>
									<ErrorMessage name="email" component="div" className="invalid-feedback" />
								</div>

								<Button htmlType="submit" type="primary" disabled={isSubmitting}>
									Reset password
								</Button>

								<p className="mt-4">
									<Link to="/signin" className="anchor">Return to sign in</Link>
								</p>
							</Form>
						)}
					/>
				</div>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(ForgotPassword));

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { signIn } from '../../actions/usersAction';
import { withRouter } from 'react-router-dom'
import Formol, { Field } from 'formol'
import * as Yup from 'yup';
import { Link } from 'react-router-dom';
import { Button, Alert } from 'pikcha-frame';

const SignInSchema = Yup.object().shape({
	email: Yup.string()
		.email('Invalid email')
		.required('No email provided.'),
	password: Yup.string()
		.required('No password provided.')
});

class SignIn extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		return (
			<div className="flex -mx-2 justify-center md:mt-6" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-full md:w-2/3 lg:w-1/2 px-2">
					<Formik
						initialValues={{ email: '', password: '' }}
						validationSchema={SignInSchema}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							this.props.dispatch(signIn(values));
						}}
						render={({ isSubmitting }) => (
							<Form>
								<div className="flex justify-between items-baseline mb-6">
									<h1 className="text-2xl">Sign in</h1>
									<p>or <Link to="/signup" className="anchor">create an account</Link></p>
								</div>

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

								<div className="form-group">
									<label className="form-label">Password</label>
									<Field
										type="password"
										name="password"
										placeholder="e.g., ••••••••••••"
										className="form-input"
									/>
									<ErrorMessage name="password" component="div" className="invalid-feedback" />
								</div>

								<Button htmlType="submit" type="primary" disabled={isSubmitting}>
									Sign in
								</Button>

								<Link to="forgot-password" className="block mt-4 anchor">
									Forgot password?
								</Link>
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

export default withRouter(connect(mapStateToProps)(SignIn));

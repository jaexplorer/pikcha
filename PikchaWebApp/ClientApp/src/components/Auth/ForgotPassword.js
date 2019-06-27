import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
// import { forgotPassword } from '../../actions/usersAction';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { Button, Alert } from 'pikcha-frame';

class ForgotPassword extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		return (
			<div className="flex -mx-2 justify-center md:mt-6" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-full md:w-2/3 lg:w-1/2 px-2">
					<Formol
						initialValues={{ email: '' }}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							// this.props.dispatch(forgotPassword(values));
						}}
					>
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
						</div>

						{/* <Button htmlType="submit" type="primary" disabled={isSubmitting}>
							Reset password
						</Button> */}

						<p className="mt-4">
							<Link to="/signin" className="anchor">Return to sign in</Link>
						</p>
					</Formol>
				</div>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(ForgotPassword));

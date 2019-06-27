import React, { Component } from 'react';
import { connect } from 'react-redux';
// import { saveUser } from '../../actions/usersAction';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { withRouter } from 'react-router-dom'
import { Link } from 'react-router-dom';
import { Button, Alert } from 'pikcha-frame';

class SignUp extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		return (
			<div className="flex -mx-2 justify-center md:mt-6" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-full md:w-2/3 lg:w-1/2 px-2">
					<Formol
						initialValues={{
							firstName: '',
							lastName: '',
							email: '',
							password: ''
						}}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							// this.props.dispatch(saveUser(values));
						}}
					>
						<div className="flex justify-between items-baseline mb-6">
							<h1 className="text-2xl">Sign up</h1>
							<p>or <Link to="/signin" className="anchor">sign in to your account</Link></p>
						</div>

						{error &&
							<Alert type="danger">Error: {error}</Alert>
						}

						<div className="form-group">
							<label className="form-label">First name</label>
							<Field name="firstName" placeholder="e.g., Jane" className="form-input" />
						</div>

						<div className="form-group">
							<label className="form-label">Last name</label>
							<Field name="lastName" placeholder="e.g., Doe" className="form-input" />
						</div>

						<div className="form-group">
							<label className="form-label">Email address</label>
							<Field
								type="email"
								name="email"
								placeholder="e.g., john.doe@gmail.com"
								className="form-input"
							/>
						</div>

						<div className="form-group">
							<label className="form-label">Password</label>
							<Field
								type="password"
								name="password"
								placeholder="e.g., ••••••••••••"
								className="form-input"
							/>
						</div>

						{/* <Button htmlType="submit" type="primary" disabled={isSubmitting}>
							Sign up
						</Button> */}
					</Formol>
				</div>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(SignUp));

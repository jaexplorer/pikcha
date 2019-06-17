import React, { Component } from 'react';
import { connect } from 'react-redux';
import { setPassword } from '../../actions/usersAction';
import Formol, { Field } from 'formol'
import { withRouter } from 'react-router-dom'
import * as Yup from 'yup';
import { Button, Alert } from 'pikcha-frame';

const SetPasswordSchema = Yup.object().shape({
	newPassword: Yup.string()
		.required('No password provided.')
});

class SetPassword extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		let code = null;
		let id = null;

		return (
			<div className="flex -mx-2" style={{ opacity: isFetchingCurrent ? 0.5 : 1 }}>
				<div className="w-1/2 px-2">
					<Formik
						initialValues={{ newPassword: '' }}
						validationSchema={SetPasswordSchema}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							this.props.dispatch(setPassword(values, id, code));
						}}
						render={({ isSubmitting }) => (
							<Form>
								<h1 className="text-2xl mb-4">Set password</h1>

								{error &&
									<Alert type="danger">Error: {error}</Alert>
								}

								<div className="form-group">
									<label className="form-label">New password</label>
									<Field
										type="password"
										name="newPassword"
										placeholder="e.g., ••••••••••••"
										className="form-input"
									/>
									<ErrorMessage name="newPassword" component="div" className="invalid-feedback" />
								</div>

								<Button htmlType="submit" disabled={isSubmitting}>
									Set password
								</Button>
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

export default withRouter(connect(mapStateToProps)(SetPassword));

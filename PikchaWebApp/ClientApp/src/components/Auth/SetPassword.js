import React, { Component } from 'react';
import { connect } from 'react-redux';
// import { setPassword } from '../../actions/usersAction';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import { withRouter } from 'react-router-dom'
import { Button, Alert } from 'pikcha-frame';

class SetPassword extends Component {
	render() {
		const { error, isFetchingCurrent } = this.props.users;

		let code = null;
		let id = null;

		return (
			<div>
				<div>
					<Formol
						initialValues={{ newPassword: '' }}
						onSubmit={(values, actions) => {
							actions.setSubmitting(false);
							// this.props.dispatch(setPassword(values, id, code));
						}}
					>
						<h1 >Set password</h1>

						{error &&
							<Alert type="danger">Error: {error}</Alert>
						}

						<div>
							<label>New password</label>
							<Field
								type="password"
								name="newPassword"
								placeholder="e.g., ••••••••••••"

							/>
						</div>

						{/* <Button htmlType="submit" disabled={isSubmitting}>
							Set password
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

export default withRouter(connect(mapStateToProps)(SetPassword));

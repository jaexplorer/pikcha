import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { updateCart } from '../../actions/cartAction';
import { Link } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import OrderSummary from './OrderSummary';
import SubNav from './SubNav';
import Button from '../Base/Button';
import Alert from '../Base/Alert';

const CheckoutInfoSchema = Yup.object().shape({
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
	password: Yup.string()
		.required('No password provided.')
});

class CheckoutInfo extends Component {
	render() {
		const { error, isFetchingCurrent, currentUser } = this.props.users;

		if (isFetchingCurrent) {
			return <div>Loading...</div>;
		}

		return (
			<div>
				<SubNav />

				<div className="flex flex-wrap -mx-4">
					<div className="w-full md:w-2/3 px-4 mb-6 md:mb-0">
						<Formik
							initialValues={{
								firstName: currentUser.firstName,
								lastName: currentUser.lastName,
								address: '',
								apt: '',
								city: '',
								postcode: '',
								mobileNumber: '',
								notes: ''
							}}
							validationSchema={CheckoutInfoSchema}
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								this.props.dispatch(updateCart(values));
							}}
							render={({ isSubmitting }) => (
								<Form>
									<h1 className="text-2xl mb-4">Checkout information</h1>

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
										<label className="form-label">Address</label>
										<Field name="address" placeholder="e.g., 123 Fake Street" className="form-input" />
										<ErrorMessage name="address" component="div" className="invalid-feedback" />
									</div>

									<div className="form-group">
										<label className="form-label">Apartment</label>
										<Field
											name="address"
											placeholder="e.g., Apartment, Suite, etc."
											className="form-input"
										/>
										<ErrorMessage name="address" component="div" className="invalid-feedback" />
									</div>

									<div className="form-group">
										<label className="form-label">City</label>
										<Field name="city" placeholder="e.g., Melbourne" className="form-input" />
										<ErrorMessage name="city" component="div" className="invalid-feedback" />
									</div>

									<div className="flex -mx-2">
										<div className="w-1/3 px-2 form-group">
											<label className="form-label">Country</label>
											<Field name="country" placeholder="e.g., Australia" className="form-input" />
											<ErrorMessage name="country" component="div" className="invalid-feedback" />
										</div>

										<div className="w-1/3 px-2 form-group">
											<label className="form-label">State/territory</label>
											<Field name="country" placeholder="e.g., VIC" className="form-input" />
											<ErrorMessage name="country" component="div" className="invalid-feedback" />
										</div>

										<div className="w-1/3 px-2 form-group">
											<label className="form-label">Postcode</label>
											<Field name="postcode" placeholder="e.g., 3161" className="form-input" />
											<ErrorMessage name="postcode" component="div" className="invalid-feedback" />
										</div>
									</div>

									<div className="flex items-center justify-between">
										<Link to="/cart" className="anchor">Return to cart</Link>

										<Button htmlType="submit" type="primary" disabled={isSubmitting}>
											Continue
										</Button>
									</div>
								</Form>
							)}
						/>
					</div>

					<div className="w-full md:w-1/3 px-4">
						<OrderSummary />
					</div>
				</div>
			</div>
		);
	}
}

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(CheckoutInfo));

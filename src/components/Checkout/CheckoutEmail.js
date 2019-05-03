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

const CheckoutEmailSchema = Yup.object().shape({
	email: Yup.string()
		.email('Invalid email')
		.required('No email provided.'),
});

class CheckoutEmail extends Component {
	render() {
		return (
			<div>
				<SubNav />

				<div className="flex flex-wrap -mx-4">
					<div className="w-full md:w-2/3 px-4 mb-6 md:mb-0">
						<Formik
							initialValues={{
								email: '',
							}}
							validationSchema={CheckoutEmailSchema}
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								this.props.dispatch(updateCart(values));
							}}
							render={({ isSubmitting }) => (
								<Form>
									<h1 className="text-2xl mb-4">Let’s grab your email to get started</h1>

									<div className="form-group">
										<label className="form-label">Email</label>
										<Field name="email" placeholder="e.g., john.doe@gmail.com" className="form-input" />
										<ErrorMessage name="email" component="div" className="invalid-feedback" />
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

export default withRouter(connect()(CheckoutEmail));

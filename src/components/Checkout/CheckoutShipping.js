import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
import { updateCart } from '../../actions/cartAction';
import { Link } from 'react-router-dom';
import Formol, { Field } from 'formol'
import OrderSummary from './OrderSummary';
import SubNav from './SubNav';
import { Button } from 'pikcha-frame';

class CheckoutShipping extends Component {
	render() {
		return (
			<div>
				<SubNav />

				<div className="flex flex-wrap -mx-4">
					<div className="w-full md:w-2/3 px-4 mb-6 md:mb-0">
						<Formik
							initialValues={{
								firstName: '',
							}}
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								this.props.dispatch(updateCart(values));
							}}
							render={({ isSubmitting }) => (
								<Form>
									<h1 className="text-2xl mb-4">Shipping method</h1>

									<div className="form-group">
										<label className="form-label">Address</label>
										<Field name="address" placeholder="e.g., 123 Fake Street" className="form-input" />
										<ErrorMessage name="address" component="div" className="invalid-feedback" />
									</div>

									<div className="flex items-center justify-between">
										<Link to="/cart" className="anchor">Return to cart</Link>

										<Button htmlType="submit" type="primary" disabled={isSubmitting}>
											Select shipping method
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

export default withRouter(connect()(CheckoutShipping));

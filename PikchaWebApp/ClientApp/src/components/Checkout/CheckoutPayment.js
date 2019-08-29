import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
// import { updateCart } from '../../actions/cartAction';
import { Link } from 'react-router-dom';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import OrderSummary from './OrderSummary';
import SubNav from './SubNav';
import { Button } from 'pikcha-frame';

class CheckoutPayment extends Component {
	render() {
		return (
			<div>
				<SubNav />

				<div>
					<div>
						<Formol
							initialValues={{
								firstName: '',
							}}
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								// this.props.dispatch(updateCart(values));
							}}
						>
							<h1 >Payment method</h1>

							<div>
								<label>Address</label>
								<Field name="address" placeholder="e.g., 123 Fake Street"  />
							</div>

							<div>
								<Link to="/cart" >Return to cart</Link>

								{/* <Button htmlType="submit" type="primary" disabled={isSubmitting}>
									Select shipping method
								</Button> */}
							</div>
						</Formol>
					</div>

					<div>
						<OrderSummary />
					</div>
				</div>
			</div>
		);
	}
}

export default withRouter(connect()(CheckoutPayment));

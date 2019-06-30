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

class CheckoutEmail extends Component {
	render() {
		return (
			<div>
				<SubNav />

				<div>
					<div>
						<Formol
							initialValues={{
								email: '',
							}}
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								// this.props.dispatch(updateCart(values));
							}}
						>
							<h1 >Let’s grab your email to get started</h1>

							<div>
								<label>Email</label>
								<Field name="email" placeholder="e.g., john.doe@gmail.com"  />
							</div>

							<div>
								<Link to="/cart" >Return to cart</Link>

								{/* <Button htmlType="submit" type="primary" disabled={isSubmitting}>
									Continue
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

export default withRouter(connect()(CheckoutEmail));

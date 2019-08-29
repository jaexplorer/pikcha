import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom'
// import { updateCart } from '../../actions/cartAction';
import { Link } from 'react-router-dom';
import 'formol/lib/default.css'
import Formol, { Field } from 'formol'
import OrderSummary from './OrderSummary';
import SubNav from './SubNav';
import { Button, Alert } from 'pikcha-frame';

class CheckoutInfo extends Component {
	render() {
		const { error, isFetchingCurrent, currentUser } = this.props.users;

		if (isFetchingCurrent) {
			return <div>Loading...</div>;
		}

		return (
			<div>
				<SubNav />

				<div>
					<div>
						<Formol
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
							onSubmit={(values, actions) => {
								actions.setSubmitting(false);
								// this.props.dispatch(updateCart(values));
							}}
						>
							<h1 >Checkout information</h1>

							{error &&
								<Alert type="danger">Error: {error}</Alert>
							}

							<div>
								<div>
									<label>First name</label>
									<Field name="firstName" placeholder="e.g., Jane"  />
								</div>

								<div>
									<label>Last name</label>
									<Field name="lastName" placeholder="e.g., Doe"  />
								</div>
							</div>

							<div>
								<label>Address</label>
								<Field name="address" placeholder="e.g., 123 Fake Street"  />
							</div>

							<div>
								<label>Apartment</label>
								<Field
									name="address"
									placeholder="e.g., Apartment, Suite, etc."

								/>
							</div>

							<div>
								<label>City</label>
								<Field name="city" placeholder="e.g., Melbourne"  />
							</div>

							<div>
								<div>
									<label>Country</label>
									<Field name="country" placeholder="e.g., Australia"  />
								</div>

								<div>
									<label>State/territory</label>
									<Field name="country" placeholder="e.g., VIC"  />
								</div>

								<div>
									<label>Postcode</label>
									<Field name="postcode" placeholder="e.g., 3161"  />
								</div>
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

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(CheckoutInfo));

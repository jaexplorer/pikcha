// import React, { Component } from 'react';
// import { connect } from 'react-redux';
// import { logoutUser, saveUser } from '../../actions/usersAction';
// import { withRouter } from 'react-router-dom'
// import PropTypes from 'prop-types';
// import { Button, Spinner } from 'pikcha-frame';
// import SubNav from './SubNav';
// import { Link } from 'react-router-dom';
// import Formol, { Field } from 'formol'
// import * as Yup from 'yup';

// class AccountPayment extends Component {
// 	constructor(props) {
// 		super(props)

// 		this.handleClick = this.handleClick.bind(this)
// 	}

// 	handleDelete() {
// 		this.props.dispatch(logoutUser());
// 	}

// 	handleDelete() {
// 		this.props.dispatch(logoutUser());
// 	}

// 	render() {
// 		const { errorProduct, isFetchingProduct, product, gateways } = this.props.users;
// 		const { variant, variantId } = this.state;

// 		return (
// 			<div className="flex flex-wrap -mx-2">
// 				<div className="w-full px-2">
// 					<SubNav />

// 					<h1 className="text-2xl">Manage Cards</h1>

// 					{% set paymentSources = craft.commerce.paymentSources.getAllPaymentSourcesByUserId(currentUser.id) %}

// 					{% if paymentSources|length %}
// 						{% for paymentSource in paymentSources %}
// 							<div className="card flex justify-between">
// 								<div>
// 									<strong>{{ paymentSource.description }}</strong>
// 									<div className="text-grey-dark">{{ paymentSource.gateway.name }}</div>
// 								</div>

// 								<div>
// 									<form method="POST">
// 										{{ csrfInput() }}
// 										{{ redirectInput('/shop/customer/cards') }}
// 										<input type="hidden" name="action" value="commerce/payment-sources/delete">
// 										<input type="hidden" name="id" value="{{ paymentSource.id }}"/>
// 										<input type="submit" value="Delete"/>
// 									</form>
// 								</div>
// 							</div>
// 						{% endfor %}
// 					{% endif %}

// 					<h1 className="text-2xl">Add Card</h1>

// 					<div className="w-1/2 px-6">
// 						<div>
// 							<label htmlFor="gatewayId" className="form-label">Choose a payment type</label>

// 							<div className="relative">
// 								<select id="gatewayId" className="form-input form-select mb-4">
// 									<option value="">---</option>

// 									{/* {% set gateways = craft.commerce.gateways.allCustomerEnabledGateways %} */}

// 									{gateways.map((gateway, id) => (
// 										<option
// 											key={id}
// 											value={gateway.id}
// 										>
// 											{gateway.name}
// 										</option>
// 									))}
// 								</select>

// 								<div className="pointer-events-none absolute pin-y pin-r flex items-center px-2 text-grey-darker">
// 									<ChevronDown size={16} />
// 								</div>
// 							</div>
// 						</div>

// 						<div>
// 							<Formik
// 								initialValues={{
// 									firstName: currentUser.firstName,
// 									lastName: currentUser.lastName,
// 									email: currentUser.email,
// 									password: '',
// 									newPassword: ''
// 								}}
// 								validationSchema={AccountSchema}
// 								onSubmit={(values, actions) => {
// 									actions.setSubmitting(false);
// 									this.props.dispatch(saveUser(values, currentUser.id));
// 								}}
// 								render={({ isSubmitting }) => (
// 									<Form>
// 										<h3>Edit your details</h3>

// 										<div className="flex -mx-2">
// 											<div className="w-1/2 mb-4 px-2">
// 												<label className="form-label">First name</label>
// 												<Field name="firstName" placeholder="e.g., Jane" className="form-input" />
// 												<ErrorMessage name="firstName" component="div" className="invalid-feedback" />
// 											</div>

// 											<div className="w-1/2 mb-4 px-2">
// 												<label className="form-label">Last name</label>
// 												<Field name="lastName" placeholder="e.g., Doe" className="form-input" />
// 												<ErrorMessage name="lastName" component="div" className="invalid-feedback" />
// 											</div>
// 										</div>

// 										<div className="mb-4">
// 											<label className="form-label">Email</label>
// 											<Field name="email" placeholder="e.g., john.doe@gmail.com" className="form-input" />
// 											<ErrorMessage name="email" component="div" className="invalid-feedback" />
// 										</div>

// 										<h3>Update password</h3>

// 										<div className="flex -mx-2">
// 											<div className="w-1/2 mb-4 px-2">
// 												<label className="form-label">Current password</label>
// 												<Field name="password" placeholder="e.g., ••••••••••••" className="form-input" />
// 												<ErrorMessage name="password" component="div" className="invalid-feedback" />
// 												<p>Required when changing password or email.</p>
// 											</div>

// 											<div className="w-1/2 mb-4 px-2">
// 												<label className="form-label">New password</label>
// 												<Field name="newPassword" placeholder="e.g., ••••••••••••" className="form-input" />
// 												<ErrorMessage name="newPassword" component="div" className="invalid-feedback" />
// 												<p>Leave blank if you don't want to change.</p>
// 											</div>
// 										</div>

// 										<div className="flex items-center justify-between">
// 											<Button htmlType="submit" type="primary" disabled={isSubmitting}>
// 												Update info
// 											</Button>
// 										</div>
// 									</Form>
// 								)}
// 							/>

// 							{/* {% for gateway in gateways %}
// 								{% if gateway.supportsPaymentSources() %}
// 									<div id="fields-{{ gateway.id }}" className="gateway-fields hidden">
// 										<form method="POST" className="form-horizontal paymentSource-form max-w-md" id="gateway-{{ gateway.id }}">
// 											<input type="hidden" name="action" value="commerce/payment-sources/add"/>
// 											<input type="hidden" name="gatewayId" value="{{ gateway.id }}"/>
// 											<input type="hidden" name="cancelUrl" value="{{ '/shop/customer/cards'|hash }}"/>
// 											{{ redirectInput('/shop/customer/cards') }}
// 											{{ csrfInput() }}

// 											{{ gateway.getPaymentFormHtml({})|raw }}

// 											<div className="field" data-colspan="1">
// 												<input className="text description w-full" type="text" name="description" value="" maxlength="70" autocomplete="off" placeholder="Card description">
// 											</div>

// 											<div className="buttons">
// 												<button className="button button-primary" type="submit">Add card</button>
// 											</div>
// 										</form>
// 									</div>
// 								{% endif %}
// 							{% endfor %} */}
// 						</div>
// 					</div>
// 				</div>
// 			</div>
// 		);
// 	}
// }

// AccountPayment.propTypes = {
// 	currentUser: PropTypes.object
// };

// const mapStateToProps = state => ({
// 	...state
// });

// export default withRouter(connect(mapStateToProps)(AccountPayment));

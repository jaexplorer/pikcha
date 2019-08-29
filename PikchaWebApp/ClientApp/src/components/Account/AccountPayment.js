// import React, { Component } from 'react';
// import { connect } from 'react-redux';
// import { logoutUser, saveUser } from '../../actions/usersAction';
// import { withRouter } from 'react-router-dom'
// import PropTypes from 'prop-types';
// import { Button, Spinner } from 'pikcha-frame';
// import SubNav from './SubNav';
// import { Link } from 'react-router-dom';
// import 'formol/lib/default.css'
// import Formol, { Field } from 'formol'

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
// 			<div>
// 				<div>
// 					<SubNav />

// 					<h1>Manage Cards</h1>

// 					{% set paymentSources = craft.commerce.paymentSources.getAllPaymentSourcesByUserId(currentUser.id) %}

// 					{% if paymentSources|length %}
// 						{% for paymentSource in paymentSources %}
// 							<div>
// 								<div>
// 									<strong>{{ paymentSource.description }}</strong>
// 									<div>{{ paymentSource.gateway.name }}</div>
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

// 					<h1>Add Card</h1>

// 					<div>
// 						<div>
// 							<label htmlFor="gatewayId" >Choose a payment type</label>

// 							<div>
// 								<select id="gatewayId">
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

// 								<div>
// 									<ChevronDown size={16} />
// 								</div>
// 							</div>
// 						</div>

// 						<div>
// 							<Formol
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

// 										<divs>
// 											<div>
// 												<label>First name</label>
// 												<Field name="firstName" placeholder="e.g., Jane"  />
// 												<ErrorMessage name="firstName" component="div" />
// 											</div>

// 											<div>
// 												<label>Last name</label>
// 												<Field name="lastName" placeholder="e.g., Doe"  />
// 												<ErrorMessage name="lastName" component="div" />
// 											</div>
// 										</div>

// 										<div>
// 											<label>Email</label>
// 											<Field name="email" placeholder="e.g., john.doe@gmail.com"  />
// 											<ErrorMessage name="email" component="div" />
// 										</div>

// 										<h3>Update password</h3>

// 										<divs>
// 											<div>
// 												<label>Current password</label>
// 												<Field name="password" placeholder="e.g., ••••••••••••"  />
// 												<ErrorMessage name="password" component="div" />
// 												<p>Required when changing password or email.</p>
// 											</div>

// 											<div>
// 												<label>New password</label>
// 												<Field name="newPassword" placeholder="e.g., ••••••••••••"  />
// 												<ErrorMessage name="newPassword" component="div" />
// 												<p>Leave blank if you don't want to change.</p>
// 											</div>
// 										</div>

// 										<div>
// 											<Button htmlType="submit" type="primary" disabled={isSubmitting}>
// 												Update info
// 											</Button>
// 										</div>
// 									</Form>
// 								)}
// 							/>

// 							{/* {% for gateway in gateways %}
// 								{% if gateway.supportsPaymentSources() %}
// 									<div id="fields-{{ gateway.id }}">
// 										<form method="POST" id="gateway-{{ gateway.id }}">
// 											<input type="hidden" name="action" value="commerce/payment-sources/add"/>
// 											<input type="hidden" name="gatewayId" value="{{ gateway.id }}"/>
// 											<input type="hidden" name="cancelUrl" value="{{ '/shop/customer/cards'|hash }}"/>
// 											{{ redirectInput('/shop/customer/cards') }}
// 											{{ csrfInput() }}

// 											{{ gateway.getPaymentFormHtml({})|raw }}

// 											<div data-colspan="1">
// 												<input type="text" name="description" value="" maxlength="70" autocomplete="off" placeholder="Card description">
// 											</div>

// 											<div>
// 												<button type="submit">Add card</button>
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

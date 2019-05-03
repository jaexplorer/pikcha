// import React, { Component } from 'react';
// import SubNav from './SubNav';
// import Button from '../Base/Button';

// export default class AccountAddress extends Component {
// 	render() {
// 		return (
// 			<div className="flex flex-wrap -mx-2">
// 				<div className="w-full px-2">
// 					<SubNav />
// 					<h1 className="text-2xl">Manage your addresses</h1>
// 					<Button to="/ac">Add new address</Button>

// 					{% set customer = craft.commerce.customers.customer %}
// 					{% set primaryBillingAddress = customer.getPrimaryBillingAddress() %}
// 					{% set primaryShippingAddress = customer.getPrimaryShippingAddress() %}

// 					{% if customer.addresses|length %}
// 						<div className="flex justify-between items-center">
// 							<h1>Manage your addresses</h1>
// 							<a className="button button-primary right" href="{{ url('shop/customer/addresses/edit') }}">Add New Address</a>
// 						</div>

// 						<div className="grid-addresses mt-6">
// 							{% for address in customer.addresses %}
// 								<div className="address-card">
// 									{% include 'shop/_includes/addresses/address' with {'address': address} %}

// 									<div className="buttons mt-4">
// 										<a className="button button-primary" href="{{ url('shop/customer/addresses/edit') }}?addressId={{ address.id }}">Edit</a>
// 										<form method="POST">
// 											<input type="hidden" name="action" value="commerce/customer-addresses/delete">
// 											{{ csrfInput() }}
// 											<input type="hidden" name="id" value="{{ address.id }}"/>
// 											<input type="submit" value="Delete"/>
// 										</form>
// 									</div>

// 									<div>
// 										<strong>{% if primaryBillingAddress and primaryBillingAddress.id == address.id %}<i className="fa fa-check text-green"></i> Primary Billing{% endif %}</strong>
// 									</div>
// 									<div>
// 										<strong>{% if primaryShippingAddress and primaryShippingAddress.id == address.id %}<i className="fa fa-check text-green"></i> Primary Shipping{% endif %}</strong>
// 									</div>
// 								</div>
// 							{% endfor %}
// 						</div>
// 					{% else %}
// 						<h1 className="text-center">Manage your addresses</h1>

// 						<hr>

// 						<div className="empty-index">
// 							<i className="fas fa-address-book"></i>
// 							<div>
// 								You don’t have any addresses yet.
// 								<div className="mt-6">
// 									<a className="button button-primary" href="{{ url('shop/customer/addresses/edit') }}">Add a new address</a>
// 								</div>
// 							</div>
// 						</div>
// 					{% endif %}
// 				</div>
// 			</div>
// 		);
// 	}
// }

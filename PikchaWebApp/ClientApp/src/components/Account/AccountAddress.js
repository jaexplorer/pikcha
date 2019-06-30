// import React, { Component } from 'react';
// import SubNav from './SubNav';
// import { Button } from 'pikcha-frame';

// export default class AccountAddress extends Component {
// 	render() {
// 		return (
// 			<div>
// 				<div>
// 					<SubNav />
// 					<h1>Manage your addresses</h1>
// 					<Button to="/ac">Add new address</Button>

// 					{% set customer = craft.commerce.customers.customer %}
// 					{% set primaryBillingAddress = customer.getPrimaryBillingAddress() %}
// 					{% set primaryShippingAddress = customer.getPrimaryShippingAddress() %}

// 					{% if customer.addresses|length %}
// 						<div>
// 							<h1>Manage your addresses</h1>
// 							<a href="{{ url('shop/customer/addresses/edit') }}">Add New Address</a>
// 						</div>

// 						<div>
// 							{% for address in customer.addresses %}
// 								<div>
// 									{% include 'shop/_includes/addresses/address' with {'address': address} %}

// 									<div>
// 										<a href="{{ url('shop/customer/addresses/edit') }}?addressId={{ address.id }}">Edit</a>
// 										<form method="POST">
// 											<input type="hidden" name="action" value="commerce/customer-addresses/delete">
// 											{{ csrfInput() }}
// 											<input type="hidden" name="id" value="{{ address.id }}"/>
// 											<input type="submit" value="Delete"/>
// 										</form>
// 									</div>

// 									<div>
// 										<strong>{% if primaryBillingAddress and primaryBillingAddress.id == address.id %}<i></i> Primary Billing{% endif %}</strong>
// 									</div>
// 									<div>
// 										<strong>{% if primaryShippingAddress and primaryShippingAddress.id == address.id %}<i></i> Primary Shipping{% endif %}</strong>
// 									</div>
// 								</div>
// 							{% endfor %}
// 						</div>
// 					{% else %}
// 						<h1>Manage your addresses</h1>

// 						<hr>

// 						<div>
// 							<i></i>
// 							<div>
// 								You don’t have any addresses yet.
// 								<div>
// 									<a href="{{ url('shop/customer/addresses/edit') }}">Add a new address</a>
// 								</div>
// 							</div>
// 						</div>
// 					{% endif %}
// 				</div>
// 			</div>
// 		);
// 	}
// }

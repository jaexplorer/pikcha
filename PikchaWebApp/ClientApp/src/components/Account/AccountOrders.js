// import React, { Component } from 'react';
// import SubNav from './SubNav';

// export default class AccountOrders extends Component {
// 	render() {
// 		return (
// 			<div>
// 				<div>
// 					<SubNav />
// 					<h1>Account orders</h1>

// 					<div>
// 						<div>
// 							<h3>Details</h3>

// 							<ul>
// 								<li><strong>{{ "Customer"|t }}:</strong> {{ order.customer.email }}<br></li>
// 								<li><strong>{{ "Total"|t }}:</strong> {{ order.totalPrice|commerceaccounting.formatMoney(cart.accounting) }}<br></li>
// 								<li><strong>{{ "Amount Paid"|t }}:</strong> {{ order.totalPaid|commerceaccounting.formatMoney(cart.accounting) }}<br></li>
// 								<li><strong>{{ "Date"|t }}:</strong> {{ order.dateOrdered|date('D jS M Y') }}<br></li>
// 								{% if order.pdfUrl %}
// 									<li><strong>Receipt:</strong> <a href="{{ order.getPdfUrl('receipt') }}">Download</a></li>
// 								{% endif %}
// 							</ul>
// 						</div>
// 						<div>
// 							<h3>Shipping Address</h3>

// 							{% if order.shippingAddress %}
// 							{% include 'shop/_includes/addresses/address' with { address: order.shippingAddress } %}
// 							{% endif %}
// 						</div>
// 						<div>
// 							<h3>Billing Address</h3>
// 							{% if order.billingAddress %}
// 							{% include 'shop/_includes/addresses/address' with { address: order.billingAddress } %}
// 							{% endif %}
// 						</div>
// 					</div>

// 					<table>
// 						<thead>
// 						<tr>
// 							<th>Item</th>
// 							<th>Unit Price</th>
// 							<th>Quantity</th>
// 							<th>Total</th>
// 						</tr>
// 						</thead>
// 						<tbody>
// 						{% for item in order.lineItems %}
// 							<tr>
// 								<td>
// 									<strong>{{ item.description }}</strong><br>
// 									SKU: {{ item.sku }}
// 								</td>
// 								<td>
// 									{% if item.onSale %}
// 										<strike>{{ item.price|commerceaccounting.formatMoney(cart.accounting) }}</strike>   {{ item.salePrice|commerceaccounting.formatMoney(cart.accounting) }}
// 									{% else %}
// 										{{ item.price|commerceaccounting.formatMoney(cart.accounting) }}
// 									{% endif %}
// 								</td>
// 								<td>{{ item.qty }}</td>
// 								<td>{{ item.subtotal|commerceaccounting.formatMoney(cart.accounting) }}</td>
// 							</tr>
// 						{% endfor %}

// 						{% for adjustment in order.adjustments %}
// 							<tr>
// 								<td>{{ adjustment.type }}</td>
// 								<td colspan="2"><strong>{{ adjustment.name }}</strong><br>({{ adjustment.description }})</td>
// 								<td>{{ adjustment.amount|commerceaccounting.formatMoney(cart.accounting) }}</td>
// 							</tr>
// 						{% endfor %}

// 						<tr>
// 							<td colspan="4">
// 								Item Total: {{ order.itemTotal|commerceaccounting.formatMoney(cart.accounting) }}<br>
// 								<h4>Total: {{ order.totalPrice|commerceaccounting.formatMoney(cart.accounting) }}</h4>
// 							</td>
// 						</tr>

// 						</tbody>
// 					</table>
// 				</div>
// 			</div>
// 		);
// 	}
// }

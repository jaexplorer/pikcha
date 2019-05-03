import React from 'react';
import { Link } from 'react-router-dom';

const Footer = () => {
	return (
		<footer className="container">
			<div className="flex flex-wrap justify-between border-t border-grey-light py-6">

				<p className="p-3 pl-0 text-sm">&copy; {new Date().getFullYear()} Pikcha. All rights reserved.</p>

				<ul className="list-reset flex flex-wrap py-1">
					<li className="mr-6 py-2">
						<Link to="/pages/refund-policy" className="text-black no-underline hover:underline text-sm">
							Refund policy
						</Link>
					</li>

					<li className="mr-6 py-2">
						<Link to="/pages/privacy-policy" className="text-black no-underline hover:underline text-sm">
							Privacy policy
						</Link>
					</li>

					<li className="py-2">
						<Link to="/pages/terms-and-conditions" className="text-black no-underline hover:underline text-sm">
							Terms and conditions
						</Link>
					</li>
				</ul>
			</div>
		</footer>
	);
};

export default Footer;

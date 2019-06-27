import React from 'react';
import { Link } from 'react-router-dom';

const Footer = () => {
	return (
		<footer>
			<div>
				<p>&copy; {new Date().getFullYear()} Pikcha. All rights reserved.</p>

				<ul>
					<li>
						<Link to="/pages/refund-policy">
							Refund policy
						</Link>
					</li>

					<li>
						<Link to="/pages/privacy-policy">
							Privacy policy
						</Link>
					</li>

					<li>
						<Link to="/pages/terms-and-conditions">
							Terms and conditions
						</Link>
					</li>
				</ul>
			</div>
		</footer>
	);
};

export default Footer;

import React from 'react';
import styled from 'styled-components'
import { Link } from 'react-router-dom';
import { List, ListItem } from 'pikcha-frame';

const FooterWrap = styled.footer`
    display: flex;
`

const Footer = () => {
	return (
		<FooterWrap>
			<List inline unstyled>
				<ListItem>
					&copy; {new Date().getFullYear()} Pikcha
				</ListItem>
				<ListItem>
					<Link to="/pages/privacy-policy">
						Privacy
					</Link>
				</ListItem>
				<ListItem>
					<Link to="/pages/terms-and-conditions">
						Terms
					</Link>
				</ListItem>
			</List>
		</FooterWrap>
	);
};

export default Footer;

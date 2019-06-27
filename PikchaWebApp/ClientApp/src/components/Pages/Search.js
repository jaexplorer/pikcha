import React, { Component } from 'react';
import Search from '../Navigation/Search';
import { Heading, Box } from 'pikcha-frame';

export default class Shipping extends Component {
	render() {
		return (
			<Box>
				<Box>
					<Heading as="h1">Search</Heading>
					<Search />
				</Box>
			</Box>
		);
	}
}

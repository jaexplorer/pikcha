import React, { Component } from 'react';
import SearchField from '../Navigation/SearchField';
import { Heading, Box } from 'pikcha-frame';

export default class Shipping extends Component {
	render() {
		return (
			<Box>
				<Box>
					<Heading as="h1">Search</Heading>
					<SearchField />
				</Box>
			</Box>
		);
	}
}

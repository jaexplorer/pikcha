import React, { Component } from 'react';
import { Heading, Box } from 'pikcha-frame';

export default class NoMatch extends Component {
	render() {
		return (
			<Box>
				<Heading as="h3">No match for <code>{this.props.location.pathname}</code></Heading>
			</Box>
		);
	}
}

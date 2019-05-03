import React, { Component } from 'react';
import Search from '../Navigation/Search';

export default class Shipping extends Component {
	render() {
		return (
			<div className="flex -mx-2 justify-center md:mt-6">
				<div className="w-full md:w-2/3 lg:w-1/2 px-2">
					<h1 className="text-2xl mb-4">Search</h1>

					<Search />
				</div>
			</div>
		);
	}
}

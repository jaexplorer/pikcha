import React from 'react';
import spinner from '../../img/spinner.svg'

const Loading = props => {
	return (
		<div className="flex items-center justify-center pt-24">
			<img src={spinner} alt="Loading..." className="w-12" />
		</div>
	);
};

export default Loading;

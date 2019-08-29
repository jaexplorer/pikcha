import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import Downshift from 'downshift';
import { withRouter } from 'react-router-dom';
import { Search } from 'styled-icons/material/Search';

class SearchField extends Component {
	constructor(props) {
		super(props);

		this.inputOnChange = this.inputOnChange.bind(this);
	}

	inputOnChange(event) {
		if (!event.target.value) {
			return;
		}

		// this.props.dispastch(fetchSearch(event.target.value));
	}

	routeToItem = item => {
		const { history } = this.props;
		const link = item ? `/products/${item.slug}` : '/';
		history.push(link);
	};

	render() {
		const { error, search } = this.props.search;

		return (
			<Downshift onChange={this.routeToItem} itemToString={item => (item ? item.title : '')}>
				{({
					getInputProps,
					getItemProps,
					getLabelProps,
					getMenuProps,
					isOpen,
					inputValue,
					highlightedIndex,
					selectedItem
				}) => (
						<div>
							<div>
								<input
									type="search"
									{...getInputProps({
										placeholder: 'Search',
										onChange: this.inputOnChange
									})}
								/>
							</div>
							<div {...getMenuProps({
								style: {
									border: isOpen ? null : 'none',
								}
							})}>
								{isOpen && search.length !== 0 && !error
									? search
										.filter(
											item =>
												!inputValue ||
												item.title.toLowerCase().includes(inputValue.toLowerCase())
										)
										.map((item, index) => (
											<div
												key={item.id}
												{...getItemProps({
													key: item.id,
													index,
													item,
													style: {
														backgroundColor:
															(highlightedIndex === index || selectedItem === item) ? '#f1f5f8' : 'white',
														fontWeight: selectedItem === item ? '500' : 'normal',
													}
												})}
											>
												{item.title}
											</div>
										))
									: null}

								{isOpen && search.length === 0 && !error
									? (
										<div>
											No results for query "{inputValue}"
										</div>
									) : null}
							</div>
						</div>
					)}
			</Downshift>
		);
	}
}

SearchField.propTypes = {
	search: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(SearchField));

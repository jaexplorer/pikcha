import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import Downshift from 'downshift';
import { withRouter } from 'react-router-dom';
import { fetchSearch } from '../../actions/searchAction';
import SearchIcon from 'react-feather/dist/icons/search';

class Search extends Component {
	constructor(props) {
		super(props);

		this.inputOnChange = this.inputOnChange.bind(this);
	}

	inputOnChange(event) {
		if (!event.target.value) {
			return;
		}

		this.props.dispatch(fetchSearch(event.target.value));
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
						<div className="relative">
							<div className="relative">
								<label
									{...getLabelProps()}
									className="pointer-events-none absolute pin-y pin-l flex items-center px-2 text-grey-darker"
								>
									<SearchIcon size={16} />
									<div className="sr-only">Search</div>
								</label>

								<input
									type="search"
									className="form-input form-search w-full"
									{...getInputProps({
										placeholder: 'Search for products...',
										onChange: this.inputOnChange
									})}
								/>
							</div>
							<div {...getMenuProps({
								style: {
									border: isOpen ? null : 'none',
								}
							})} className="absolute list-reset shadow-lg border border-grey-light overflow-y-auto overflow-x-hidden rounded w-full cursor-pointer z-50 mt-3">
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
												className="relative w-full px-3 py-2 truncate bg-white text-sm"
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
										<div className="relative w-full px-3 py-2 truncate bg-white text-sm">
											No results for query "{inputValue}"
										</div>
									): null}
							</div>
						</div>
					)}
			</Downshift>
		);
	}
}

Search.propTypes = {
	search: PropTypes.object
};

const mapStateToProps = state => ({
	...state
});

export default withRouter(connect(mapStateToProps)(Search));

import React, { Component } from 'react';
import { connect } from 'react-redux';
import { fetchProducts } from '../../actions/productsAction';
import { withRouter } from 'react-router-dom'
import Button from '../Base/Button';

class Pagination extends Component {
    constructor(props) {
        super(props)

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick = (page) => {
        this.props.dispatch(fetchProducts(page));
    }

    createPagination = () => {
        let pagination = this.props.pagination;
        let paginate = [];

        if (!isNaN(pagination.current_page)) {
            for (let i = 1; i <= pagination.total_pages; i++) {
                if (i >= pagination.current_page - 3 && i < pagination.current_page + 3) {
                    paginate.push(
                        <li key={i}>
                            <button className={pagination.current_page === i ? 'btn btn-primary' : 'btn btn-default'} onClick={this.handleClick.bind(this, i)}>
                                {i}
                            </button>
                        </li>
                    );
                }
            };
        }

        return paginate;
    }

    render() {
        const { pagination } = this.props;

        if (!isNaN(pagination.current_page)) {
            let next = pagination.current_page;
            next--;

            let prev = pagination.current_page;
            prev++;

            return (
                <div className="mt-6">
                    <ul className="flex list-reset rounded w-auto item-center justify-center">
                        { next > 0 &&
                            <li>
                                <Button className="btn btn-default" onClick={this.handleClick.bind(this, next)}>
                                    Previous
                                </Button>
                            </li>
                        }

                        {this.createPagination()}

                        { prev <= pagination.total_pages &&
                            <li>
                                <Button className="btn btn-default" onClick={this.handleClick.bind(this, prev)}>
                                    Next
                                </Button>
                            </li>
                        }
                    </ul>
                </div>
            )
        } else {
            return <div></div>
        }
    }
}
const mapStateToProps = (state) => ({
    pagination: state.products.pagination,
});

export default withRouter(connect(mapStateToProps)(Pagination));

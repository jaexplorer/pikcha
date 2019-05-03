import { createActions } from "redux-actions";
import elementApi from "../helpers/elementApi";

export const {
    fetchProductsSuccess,
    fetchProductsFailure,
    fetchProductsStarted,
    fetchProductSuccess,
    fetchProductFailure,
    fetchProductStarted
} = createActions(
    {
        FETCH_PRODUCTS_SUCCESS: (products, pagination) => ({ products, pagination }),
        FETCH_PRODUCTS_FAILURE: error => ({ error }),
        FETCH_PRODUCT_SUCCESS: product => ({ product }),
        FETCH_PRODUCT_FAILURE: errorProduct => ({ errorProduct })
    },
    "FETCH_PRODUCTS_STARTED",
    "FETCH_PRODUCT_STARTED"
);

export const fetchProducts = (page) => {
    if (page === undefined) {
        page = 1;
    }

    return dispatch => {
        dispatch(fetchProductsStarted());

        elementApi.get(`products?page=${page}`)
            .then(response => {
                dispatch(fetchProductsSuccess(response.data.data, response.data.meta.pagination));
            })
            .catch(err => {
                dispatch(fetchProductsFailure(err));
            });
    };
};

export const fetchProduct = slug => {
    return dispatch => {
        dispatch(fetchProductStarted());

        elementApi.get(`products/${slug}`)
            .then(response => {
                dispatch(fetchProductSuccess(response.data));
            })
            .catch(err => {
                dispatch(fetchProductsFailure(err));
            });
    };
};

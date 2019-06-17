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

    return async dispatch => {
        dispatch(fetchProductsStarted());

        try {
            const response = await elementApi.get(`products?page=${page}`)
            dispatch(fetchProductsSuccess(response.data.data, response.data.meta.pagination));
        } catch (error) {
            dispatch(fetchProductsFailure(error));
        };
    };
};

export const fetchProduct = slug => {
    return async dispatch => {
        dispatch(fetchProductStarted());

        try {
            const response = await elementApi.get(`products/${slug}`)
            dispatch(fetchProductSuccess(response.data));
        } catch (error) {
            dispatch(fetchProductsFailure(error));
        };
    };
};

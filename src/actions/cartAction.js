import { createActions } from "redux-actions";
import clientApi from "../helpers/clientApi";

export const {
    getCartSuccess,
    getCartFailure,
    getCartStarted,
} = createActions(
    {
        GET_CART_SUCCESS: cart => ({ cart }),
        GET_CART_FAILURE: error => ({ error }),
    },
    "GET_CART_STARTED",
);

export const getCart = () => {
    let data = JSON.stringify({});

    return dispatch => {
        dispatch(getCartStarted());

        clientApi.post('cart/get-cart', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(getCartSuccess(response.data.cart));
                } else {
                    dispatch(getCartFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(getCartFailure(err));
            });
    };
};

export const updateCart = (id, qty, isLineItem) => {
    let data = {};

    if (isLineItem) {
        if (qty !== undefined) {
            data["lineItems[" + id + "][qty]"] = qty;
        } else {
            data["lineItems[" + id + "][remove]"] = 1;
        }
    } else {
        data["purchasableId"] = id;
        data["qty"] = qty;
    }

    data = JSON.stringify(data);

    return dispatch => {
        dispatch(getCartStarted());

        clientApi.post('cart/update-cart', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(getCartSuccess(response.data.cart));
                } else {
                    dispatch(getCartFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(getCartFailure(err));
            });
    };
};

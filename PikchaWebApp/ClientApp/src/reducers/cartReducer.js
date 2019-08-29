import { handleActions } from "redux-actions";

export default handleActions(
    {
        GET_CART_SUCCESS: (state, action) => ({
            ...state,
            isFetchingCart: false,
            cart: action.payload.cart
        }),
        GET_CART_FAILURE: (state, action) => ({
            ...state,
            isFetchingCart: false,
            error: action.payload.error
        }),
        GET_CART_STARTED: (state, action) => ({
            ...state,
            isFetchingCart: true
        })
    },
    {
        isFetchingCart: false,
        cart: {
            lineItems: {},
            adjustments: {}
        },
        error: null
    }
);

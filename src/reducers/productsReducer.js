import { handleActions } from "redux-actions";

export default handleActions(
    {
        FETCH_PRODUCTS_SUCCESS: (state, action) => ({
            ...state,
            isFetchingProducts: false,
            products: action.payload.products,
            pagination: action.payload.pagination,
        }),
        FETCH_PRODUCTS_FAILURE: (state, action) => ({
            ...state,
            isFetchingProducts: false,
            error: action.payload.error
        }),
        FETCH_PRODUCTS_STARTED: (state, action) => ({
            ...state,
            isFetchingProducts: true
        }),
        FETCH_PRODUCT_SUCCESS: (state, action) => ({
            ...state,
            isFetchingProduct: false,
            product: action.payload.product
        }),
        FETCH_PRODUCT_FAILURE: (state, action) => ({
            ...state,
            isFetchingProduct: false,
            errorProduct: action.payload.errorProduct
        }),
        FETCH_PRODUCT_STARTED: (state, action) => ({
            ...state,
            isFetchingProduct: true
        })
    },
    {
        isFetchingProducts: false,
        isFetchingProduct: false,
        products: [{ variants: [], authors: [], featuredImage: {} }],
        product: { variants: [], authors: [], featuredImage: {} },
        pagination: { links: {} },
        error: null,
        errorProduct: null
    }
);

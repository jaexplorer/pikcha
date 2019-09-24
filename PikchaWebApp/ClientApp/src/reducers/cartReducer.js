import { PRODUCT_ADDED, PRODUCT_REMOVED, CLEAR_CART } from "../actions/types";

const initialState = {
  products: [],
  total: 0
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case PRODUCT_ADDED:
      return {
        ...state,
        products: [...state.products, payload],
        total: state.total + payload.productPrice
      };

    case PRODUCT_REMOVED:
      return {
        ...state,
        products: state.products.filter(product => product !== payload),
        total: state.total - payload.productPrice
      };
    case CLEAR_CART:
      return {
        state: initialState
      };
    default:
      return state;
  }
};

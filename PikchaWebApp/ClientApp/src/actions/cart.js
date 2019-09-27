import { PRODUCT_ADDED, PRODUCT_REMOVED, CLEAR_CART } from "./types";

// Add Product to Cart
export const addToCart = product => {
  return dispatch => {
    dispatch({
      type: PRODUCT_ADDED,
      payload: product
    });
  };
};

// Remove Product from Cart
export const removeFromCart = product => {
  return dispatch => {
    dispatch({
      type: PRODUCT_REMOVED,
      payload: product
    });
  };
};

// Clear Cart
export const clearCart = () => {
  return dispatch => {
    dispatch({
      type: CLEAR_CART
    });
  };
};

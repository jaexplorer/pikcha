import axios from "axios";

import {
  GET_PRODUCT,
  PRODUCT_ERROR,
  PRODUCT_LOADING,
  SET_SIZE,
  SET_MATERIAL,
  SET_FRAME,
  SET_BORDER,
  SET_FINISH,
  CLEAR_CUSTOMISATIONS
} from "./types";

// Get Product
export const getProduct = id => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(`api/image/${id}`);
      dispatch({
        type: GET_PRODUCT,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: PRODUCT_ERROR });
    }
  };
};

// Set Size
export const setSize = size => {
  return dispatch => {
    dispatch({
      type: SET_SIZE,
      payload: size
    });
  };
};

// Set Material
export const setMaterial = material => {
  return dispatch => {
    dispatch({
      type: SET_MATERIAL,
      payload: material
    });
  };
};

// Set Frame
export const setFrame = frame => {
  return dispatch => {
    dispatch({
      type: SET_FRAME,
      payload: frame
    });
  };
};

// Set Border
export const setBorder = border => {
  return dispatch => {
    dispatch({
      type: SET_BORDER,
      payload: border
    });
  };
};

// Set Finish
export const setFinish = finish => {
  return dispatch => {
    dispatch({
      type: SET_FINISH,
      payload: finish
    });
  };
};

// Clear Customisations
export const clearCustomisations = () => {
  return dispatch => {
    dispatch({
      type: CLEAR_CUSTOMISATIONS
    });
  };
};

// SET LOADING
export const setLoading = () => {
  return { type: PRODUCT_LOADING };
};

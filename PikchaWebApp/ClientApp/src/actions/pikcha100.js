import axios from "axios";

import {
  PIKCHA100_LOADED,
  PIKCHA100_LOADING,
  PIKCHA100_ERROR,
  PIKCHA100_RESET
} from "./types";

// Get Pikcha100
export const getPikcha100 = (count, start) => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(
        `api/filter/images?type=pikcha100&count=${count}&start=${start}`
      );
      dispatch({
        type: PIKCHA100_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: PIKCHA100_ERROR,
        payload: err.response
      });
    }
  };
};

// Set Loading
export const setLoading = () => {
  return { type: PIKCHA100_LOADING };
};

// Reset Pikcha Top100
export const resetPikcha100 = () => {
  return dispatch =>
    dispatch({
      type: PIKCHA100_RESET
    });
};

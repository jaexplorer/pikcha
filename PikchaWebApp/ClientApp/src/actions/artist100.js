import axios from "axios";

import {
  ARTIST100_LOADED,
  ARTIST100_LOADING,
  ARTIST100_ERROR,
  ARTIST100_RESET
} from "./types";

// Get Artist 100
export const getArtist100 = (count, start) => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(
        `api/filter/images?type=artist100&count=${count}&start=${start}`
      );
      dispatch({
        type: ARTIST100_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: ARTIST100_ERROR,
        payload: err.response
      });
    }
  };
};

// Set Loading
export const setLoading = () => {
  return { type: ARTIST100_LOADING };
};

// Reset Artist 100
export const resetArtist100 = () => {
  return dispatch =>
    dispatch({
      type: ARTIST100_RESET
    });
};

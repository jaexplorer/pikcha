import axios from "axios";

import {
  PIKCHA100_LOADED,
  ARTIST100_LOADED,
  PIKCHA100_LOADING,
  ARTIST100_LOADING,
  TOP100_ERROR
} from "./types";

// Get Pikcha100
export const getPikcha100 = () => {
  return async dispatch => {
    try {
      dispatch(setPikchaLoading());
      const res = await axios.get(
        `api/filter/images?type=pikcha100&count=100&start=1`
      );
      dispatch({
        type: PIKCHA100_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: TOP100_ERROR,
        payload: err.response
      });
    }
  };
};

// Get Artist100
export const getArtist100 = () => {
  return async dispatch => {
    try {
      dispatch(setArtistLoading());
      const res = await axios.get(
        `api/filter/images?type=artist100&count=100&start=1`
      );
      dispatch({
        type: ARTIST100_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: TOP100_ERROR,
        payload: err.response
      });
    }
  };
};

// Set Pikcha Loading
export const setPikchaLoading = () => {
  return { type: PIKCHA100_LOADING };
};

// Set Artist Loading
export const setArtistLoading = () => {
  return { type: ARTIST100_LOADING };
};

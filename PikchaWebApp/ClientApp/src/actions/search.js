import {
  SUGGESTIONS_LOADED,
  SUGGESTIONS_LOADING,
  SUGGESTIONS_RESET,
  SUGGESTIONS_ERROR
} from "./types";
import axios from "axios";

// Get Suggestions
export const getSuggestions = query => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      // const res = await axios.get(
      //   `api/filter/images?count=${count}&start=${start}`
      // );

      const res = [
        "Mountains by the ocean",
        "Snowy Mountains",
        "Mount Dong",
        "Mountain Sunset",
        "Tall Mount"
      ];

      dispatch({
        type: SUGGESTIONS_LOADED,
        payload: res
      });
    } catch (err) {
      dispatch({
        type: SUGGESTIONS_ERROR,
        payload: err.response
      });
    }
  };
};

// Set Loading
export const setLoading = () => {
  return { type: SUGGESTIONS_LOADING };
};

// Reset Gallery
export const resetSuggestions = () => {
  return dispatch =>
    dispatch({
      type: SUGGESTIONS_RESET
    });
};

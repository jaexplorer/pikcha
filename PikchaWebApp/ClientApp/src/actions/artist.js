import axios from "axios";
import { setAlert } from "./alert";

import { ARTIST_LOADED, ARTIST_LOADING, ARTIST_ERROR } from "./types";

// Get Artist
export const getArtist = id => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(`api/profile/${id}`);
      dispatch({
        type: ARTIST_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: ARTIST_ERROR, payload: err.response.data });
      dispatch(setAlert(err.response.data, "danger"));
    }
  };
};

// SET LOADING
export const setLoading = () => {
  return { type: ARTIST_LOADING };
};

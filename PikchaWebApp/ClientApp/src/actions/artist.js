import axios from "axios";

import { GET_ARTIST, ARTIST_LOADING, ARTIST_ERROR } from "./types";

// Get Artist
export const getArtist = id => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(`http://localhost:8000/api/profile/${id}`);
      dispatch({
        type: GET_ARTIST,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: ARTIST_ERROR });
    }
  };
};

// SET LOADING
export const setLoading = () => {
  return { type: ARTIST_LOADING };
};

import axios from "axios";

import {
  PROFILE_LOADED,
  PROFILE_LOADING,
  PROFILE_ERROR,
  PROFILE_RESET,
  ARTIST_PHOTOS_LOADED,
  ARTIST_PHOTOS_LOADING,
  ARTIST_PHOTOS_ERROR,
  ARTIST_PHOTOS_RESET
} from "./types";

// Get Profile
export const getProfile = id => {
  return async dispatch => {
    try {
      dispatch(setProfileLoading());
      const res = await axios.get(`api/profile/${id}`);
      dispatch({
        type: PROFILE_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: PROFILE_ERROR, payload: err.response.data });
    }
  };
};

// Get Artist Photos
export const getArtistPhotos = (artistId, count, start) => {
  return async dispatch => {
    try {
      dispatch(setArtistPhotosLoading());
      const res = await axios.get(
        `api/filter/images?type=artistId&count=${count}&start=${start}&artistId=${artistId}`
      );
      dispatch({
        type: ARTIST_PHOTOS_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: ARTIST_PHOTOS_ERROR,
        payload: err.response
      });
    }
  };
};

// Set Profile Loading
export const setProfileLoading = () => {
  return { type: PROFILE_LOADING };
};

// Set Profile Loading
export const setArtistPhotosLoading = () => {
  return { type: ARTIST_PHOTOS_LOADING };
};

// Reset Profile
export const resetProfile = () => {
  return dispatch =>
    dispatch({
      type: PROFILE_RESET
    });
};

// Reset Artist Photos
export const resetArtistPhotos = () => {
  return dispatch =>
    dispatch({
      type: ARTIST_PHOTOS_RESET
    });
};

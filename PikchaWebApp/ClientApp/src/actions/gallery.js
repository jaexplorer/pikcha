import axios from "axios";

import {
  GALLERY_LOADED,
  GALLERY_LOADING,
  GALLERY_ERROR,
  GALLERY_RESET
} from "./types";

// Get Photos
export const getPhotos = (count, start) => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get(
        `api/filter/images?count=${count}&start=${start}`
      );
      dispatch({
        type: GALLERY_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: GALLERY_ERROR,
        payload: err.response
      });
    }
  };
};

// Add Photo View
export const addView = imageId => {
  return async dispatch => {
    try {
      const res = await axios.post(`api/image/${imageId}/view`);
      console.log("View added", res);
    } catch (err) {
      console.log("View add error", err.response);
    }
  };
};

// Set Loading
export const setLoading = () => {
  return { type: GALLERY_LOADING };
};

// Reset Gallery
export const resetGallery = () => {
  return dispatch =>
    dispatch({
      type: GALLERY_RESET
    });
};

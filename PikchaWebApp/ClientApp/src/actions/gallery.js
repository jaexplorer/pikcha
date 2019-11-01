import axios from "axios";

import {
  PHOTOS_LOADED,
  PHOTO_SELECTED,
  PHOTO_DESELECTED,
  PHOTOS_LOADING,
  PHOTOS_ERROR
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
        type: PHOTOS_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({
        type: PHOTOS_ERROR,
        payload: err.response
      });
    }
  };
};

// Select Photo
export const selectPhoto = photo => {
  return dispatch =>
    dispatch({
      type: PHOTO_SELECTED,
      payload: photo
    });
};

// Deselect Photo
export const deselectPhoto = () => {
  return dispatch =>
    dispatch({
      type: PHOTO_DESELECTED
    });
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
  return { type: PHOTOS_LOADING };
};

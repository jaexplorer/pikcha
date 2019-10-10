import axios from "axios";

import {
  GET_PHOTOS,
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
        `http://localhost:8000/api/filter/images?count=${count}&start=${start}`
      );
      dispatch({
        type: GET_PHOTOS,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: PHOTOS_ERROR });
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

// SET LOADING
export const setLoading = () => {
  return { type: PHOTOS_LOADING };
};

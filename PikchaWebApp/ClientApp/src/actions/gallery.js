import { PHOTO_SELECTED, PHOTO_DESELECTED } from "./types";

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

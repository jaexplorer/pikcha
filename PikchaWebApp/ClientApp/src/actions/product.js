import {
  SET_SIZE,
  SET_MATERIAL,
  SET_FRAME,
  SET_BORDER,
  SET_FINISH,
  CLEAR_CUSTOMISATIONS
} from "./types";

// Set Size
export const setSize = size => {
  return dispatch => {
    dispatch({
      type: SET_SIZE,
      payload: size
    });
  };
};

// Set Material
export const setMaterial = material => {
  return dispatch => {
    dispatch({
      type: SET_MATERIAL,
      payload: material
    });
  };
};

// Set Frame
export const setFrame = frame => {
  return dispatch => {
    dispatch({
      type: SET_FRAME,
      payload: frame
    });
  };
};

// Set Border
export const setBorder = border => {
  return dispatch => {
    dispatch({
      type: SET_BORDER,
      payload: border
    });
  };
};

// Set Finish
export const setFinish = finish => {
  return dispatch => {
    dispatch({
      type: SET_FINISH,
      payload: finish
    });
  };
};

// Clear Customisations
export const clearCustomisations = () => {
  return dispatch => {
    dispatch({
      type: CLEAR_CUSTOMISATIONS
    });
  };
};

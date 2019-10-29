import { CREATE_MODAL, REMOVE_MODAL } from "./types";

// Create modal based on type
export const createModal = type => {
  return dispatch => {
    dispatch({
      type: CREATE_MODAL,
      payload: type
    });
  };
};

// Remove modal
export const removeModal = () => {
  return dispatch => {
    dispatch({
      type: REMOVE_MODAL
    });
  };
};

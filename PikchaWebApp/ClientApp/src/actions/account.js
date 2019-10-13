import { CREATE_DP_MODAL, REMOVE_DP_MODAL } from "./types";

// Create upload display picture model
export const createModal = () => {
  return dispatch => {
    dispatch({
      type: CREATE_DP_MODAL
    });
  };
};

// Remove upload display picture model
export const removeModal = () => {
  return dispatch => {
    dispatch({
      type: REMOVE_DP_MODAL
    });
  };
};

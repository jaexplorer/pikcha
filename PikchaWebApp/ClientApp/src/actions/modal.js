import { MODAL_CREATED, MODAL_REMOVED } from "./types";

export const createDPModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: "DP"
    });
  };
};

export const createPromoteModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: "Promote"
    });
  };
};

export const createUploadImageModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: "UploadImage"
    });
  };
};

// Remove modal
export const removeModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_REMOVED
    });
  };
};

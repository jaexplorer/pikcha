import { MODAL_CREATED, MODAL_REMOVED } from "./types";

export const createDPModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: { type: "DP" }
    });
  };
};

export const createCoverModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: { type: "Cover" }
    });
  };
};

export const createFullscreenModal = image => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: { type: "Fullscreen", data: image }
    });
  };
};

export const createPromoteModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: { type: "Promote" }
    });
  };
};

export const createUploadImageModal = () => {
  return dispatch => {
    dispatch({
      type: MODAL_CREATED,
      payload: { type: "UploadImage" }
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

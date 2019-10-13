import { CREATE_DP_MODAL, REMOVE_DP_MODAL } from "../actions/types";

const initialState = {
  DPModal: false
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case CREATE_DP_MODAL:
      return {
        ...state,
        DPModal: true
      };

    case REMOVE_DP_MODAL:
      return {
        ...state,
        DPModal: false
      };

    default:
      return state;
  }
};

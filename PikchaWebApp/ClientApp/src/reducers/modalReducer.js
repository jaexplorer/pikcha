import { MODAL_CREATED, MODAL_REMOVED } from "../actions/types";

const initialState = {
  type: false
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case MODAL_CREATED:
      return {
        ...state,
        type: payload
      };

    case MODAL_REMOVED:
      return initialState;

    default:
      return state;
  }
};

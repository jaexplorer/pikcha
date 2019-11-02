import { CREATE_MODAL, REMOVE_MODAL } from "../actions/types";

const initialState = {
  type: null,
  active: false
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case CREATE_MODAL:
      return {
        ...state,
        active: true,
        type: payload
      };

    case REMOVE_MODAL:
      return initialState;

    default:
      return state;
  }
};

import { CREATE_DROPDOWN, REMOVE_DROPDOWN } from "../actions/types";

const initialState = {};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case CREATE_DROPDOWN:
      return { ...state, ...payload };

    case REMOVE_DROPDOWN:
      return { ...state, ...payload };

    default:
      return state;
  }
};

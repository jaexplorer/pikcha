import { CREATE_DROPDOWN, REMOVE_DROPDOWN } from "../actions/types";

const initialState = {
  dropDown: false
};

export default (state = initialState, { type }) => {
  switch (type) {
    case CREATE_DROPDOWN:
      return {
        ...state,
        dropDown: true
      };

    case REMOVE_DROPDOWN:
      return {
        ...state,
        dropDown: false
      };

    default:
      return state;
  }
};

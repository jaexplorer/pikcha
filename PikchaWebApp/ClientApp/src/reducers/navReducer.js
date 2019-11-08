import { DROPDOWN_OPEN, DROPDOWN_CLOSED } from "../actions/types";

const initialState = {
  dropDown: false
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case DROPDOWN_OPEN:
      return {
        ...state,
        dropDown: payload
      };

    case DROPDOWN_CLOSED:
      return {
        ...state,
        dropDown: false
      };

    default:
      return state;
  }
};

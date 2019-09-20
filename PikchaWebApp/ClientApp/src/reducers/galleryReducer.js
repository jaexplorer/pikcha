import { PHOTO_SELECTED, PHOTO_DESELECTED } from "../actions/types";

const initialState = {
  selected: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case PHOTO_SELECTED:
      return {
        ...state,
        selected: payload
      };
    case PHOTO_DESELECTED:
      return {
        ...state,
        selected: null
      };

    default:
      return state;
  }
};

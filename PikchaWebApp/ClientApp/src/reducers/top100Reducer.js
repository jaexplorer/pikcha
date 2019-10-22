import {
  PIKCHA100_LOADED,
  ARTIST100_LOADED,
  PIKCHA100_LOADING,
  ARTIST100_LOADING,
  TOP100_ERROR
} from "../actions/types";

const initialState = {
  pikcha100: null,
  artist100: null,
  pikchaloading: true,
  artistloading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case ARTIST100_LOADED:
      return {
        ...state,
        artist100: payload,
        artistloading: false
      };
    case PIKCHA100_LOADED:
      return {
        ...state,
        pikcha100: payload,
        pikchaloading: false
      };
    case TOP100_ERROR:
      console.error(payload);
      return {
        ...state,
        error: payload,
        artistloading: false,
        pikchaloading: false
      };
    case PIKCHA100_LOADING:
      return {
        ...state,
        pikchaloading: true
      };
    case ARTIST100_LOADING:
      return {
        ...state,
        artistloading: true
      };

    default:
      return state;
  }
};

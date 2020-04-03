import {
  SUGGESTIONS_LOADED,
  SUGGESTIONS_LOADING,
  SUGGESTIONS_RESET,
  SUGGESTIONS_ERROR
} from "../actions/types";

const initialState = {
  suggestions: false,
  loading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case SUGGESTIONS_LOADED:
      return {
        ...state,
        suggestions: payload,
        loading: false
      };
    case SUGGESTIONS_LOADING:
      return {
        ...state,
        loading: true
      };
    case SUGGESTIONS_ERROR:
      return {
        ...state,
        error: payload
      };
    case SUGGESTIONS_RESET:
      return initialState;
    default:
      return state;
  }
};

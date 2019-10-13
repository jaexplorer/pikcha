import {
  LOGGED_IN,
  AUTH_ERROR,
  LOGGED_OUT,
  CLEAR_ERRORS,
  AUTH_LOADING
} from "../actions/types";

const initialState = {
  isAuthenticated: false,
  loading: true,
  user: null,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case LOGGED_IN:
      return {
        ...state,
        isAuthenticated: true,
        loading: false,
        user: payload
      };
    case AUTH_ERROR:
    case LOGGED_OUT:
      return {
        ...state,
        isAuthenticated: false,
        loading: false,
        user: null,
        error: payload
      };
    case AUTH_LOADING:
      return {
        ...state,
        loading: true
      };
    case CLEAR_ERRORS:
      return {
        ...state,
        error: null
      };
    default:
      return state;
  }
};

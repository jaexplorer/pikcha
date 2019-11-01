import {
  DEAUTHENTICATE,
  AUTH_LOADING,
  AUTH_FAILED,
  AUTH_SUCCESS
} from "../actions/types";

const initialState = {
  isAuthenticated: false,
  loading: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case AUTH_SUCCESS:
      return {
        ...state,
        isAuthenticated: true,
        loading: false
      };
    case AUTH_FAILED:
    case DEAUTHENTICATE:
      return {
        ...state,
        isAuthenticated: false,
        loading: false
      };
    case AUTH_LOADING:
      return {
        ...state,
        loading: true
      };
    default:
      return state;
  }
};

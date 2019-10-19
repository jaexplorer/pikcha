import {
  USER_LOADING,
  USER_LOADED,
  LOGGED_OUT,
  USER_ERROR
} from "../actions/types";

const initialState = {
  user: null,
  loading: true,
  errors: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case USER_LOADED:
      return {
        ...state,
        user: payload,
        loading: false
      };
    case LOGGED_OUT:
      return {
        ...state,
        user: null,
        loading: false
      };
    case USER_ERROR:
      console.log(payload);
      return {
        ...state,
        errors: payload,
        loading: false
      };
    case USER_LOADING:
      return {
        ...state,
        loading: true
      };
    default:
      return state;
  }
};

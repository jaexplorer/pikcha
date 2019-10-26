import {
  USER_LOADING,
  USER_LOADED,
  USER_UPDATED,
  LOGGED_OUT,
  USER_ERROR,
  SIGNATURE_LOADED,
  SIGNATURE_LOADING,
  ARTIST_FOLLOWED,
  ARTIST_UNFOLLOWED
} from "../actions/types";

const initialState = {
  user: null,
  loadingUser: true,
  loadingSignature: true,
  errors: null,
  signature: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case USER_LOADED:
      return {
        ...state,
        user: payload,
        loadingUser: false
      };
    case USER_UPDATED:
      return {
        ...state,
        user: payload,
        loadingUser: false
      };
    case LOGGED_OUT:
      return {
        ...state,
        user: null,
        loadingUser: false
      };
    case USER_ERROR:
      console.log(payload);
      return {
        ...state,
        errors: payload,
        loadingUser: false
      };
    case SIGNATURE_LOADED:
      return {
        ...state,
        signature: payload,
        loadingSignature: false
      };
    case USER_LOADING:
      return {
        ...state,
        loadingUser: true
      };
    case SIGNATURE_LOADING:
      return {
        ...state,
        loadingSignature: true
      };
    default:
      return state;
  }
};

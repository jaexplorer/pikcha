import AuthorizeService from "../auth/AuthorizeService";
import { loadUser, logout } from "./account";

import {
  DEAUTHENTICATE,
  AUTH_LOADING,
  AUTH_FAILED,
  AUTH_SUCCESS
} from "./types";

// Authenticate User
export const authenticate = () => {
  return async dispatch => {
    dispatch(setLoading());

    const isAuthenticated = await AuthorizeService.isAuthenticated();
    if (isAuthenticated) {
      dispatch(loadUser());
      dispatch({
        type: AUTH_SUCCESS
      });
    } else {
      dispatch({
        type: AUTH_FAILED
      });
    }
  };
};

// Deauthenticate user
export const deauthenticate = () => {
  return async dispatch => {
    dispatch(logout());
    dispatch({ type: DEAUTHENTICATE });
  };
};

// Set Loading
export const setLoading = () => {
  return { type: AUTH_LOADING };
};

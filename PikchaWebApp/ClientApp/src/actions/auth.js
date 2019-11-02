import AuthorizeService from "../components/auth/AuthorizeService";
import { loadUser, logout } from "./account";
import { setAlert } from "./alert";

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
      dispatch({
        type: AUTH_SUCCESS
      });
      dispatch(loadUser());
      dispatch(setAlert("Successfully authenticated", "info"));
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
    dispatch(setAlert("Logged out", "info"));
  };
};

// Set Loading
export const setLoading = () => {
  return { type: AUTH_LOADING };
};

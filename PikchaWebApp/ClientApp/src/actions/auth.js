// TODO: Clean up Dispatch for consistency, link to backend
import AuthorizeService from "../components/auth/AuthorizeService";

import {
  LOGGED_IN,
  AUTH_ERROR,
  LOGGED_OUT,
  CLEAR_ERRORS,
  AUTH_LOADING
} from "./types";

// LOAD USER

export const loaduser = () => {
  return async dispatch => {
    dispatch(setLoading());

    try {
      const isAuthenticated = await AuthorizeService.isAuthenticated();
      if (isAuthenticated) {
        dispatch(login());
      } else {
        dispatch(logout());
      }
    } catch (err) {
      dispatch({ type: AUTH_ERROR });
    }
  };
};

export const login = () => {
  return async dispatch => {
    dispatch(setLoading());

    try {
      const user = await AuthorizeService.getUser();
      dispatch({
        type: LOGGED_IN,
        payload: user
      });
    } catch (err) {
      dispatch({ type: AUTH_ERROR });
    }
  };
};

// LOGOUT USER

export const logout = () => {
  return async dispatch => {
    dispatch({ type: LOGGED_OUT });
  };
};

// CLEAR ERRORS

export const clearErrors = () => {
  return dispatch => {
    dispatch({ type: CLEAR_ERRORS });
  };
};

// SET LOADING

export const setLoading = () => {
  return { type: AUTH_LOADING };
};

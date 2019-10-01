// TODO: Clean up Dispatch for consistency, link to backend
import setAuthToken from "../utils/setAuthToken";
import axios from "axios";

import {
  REGISTER_SUCCESS,
  REGISTER_FAIL,
  USER_LOADED,
  AUTH_ERROR,
  LOGIN_SUCCESS,
  LOGIN_FAIL,
  LOGGED_OUT,
  CLEAR_ERRORS,
  SET_LOADING
} from "./types";

// LOAD USER
export const loadUser = () => {
  if (localStorage.token) {
    setAuthToken(localStorage.token);
  }

  return async dispatch => {
    try {
      dispatch(setLoading());
      const res = await axios.get("http://localhost:5000/api/auth");

      dispatch({
        type: USER_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: AUTH_ERROR });
    }
  };
};

// REGISTER USER
export const register = formData => async dispatch => {
  try {
    dispatch(setLoading());
    const config = {
      headers: {
        "Content-Type": "application/json"
      }
    };
    const res = await axios.post(
      "http://localhost:5000/api/users",
      formData,
      config
    );

    dispatch({
      type: REGISTER_SUCCESS,
      payload: res.data
    });
    dispatch(loadUser());
  } catch (err) {
    dispatch({
      type: REGISTER_FAIL,
      payload: err.response.data.msg
    });
  }
};

// LOGIN USER
export const login = formData => async dispatch => {
  try {
    dispatch(setLoading());
    const config = {
      headers: {
        "Content-Type": "application/json"
      }
    };

    const res = await axios.post(
      "http://localhost:5000/api/auth",
      formData,
      config
    );

    dispatch({
      type: LOGIN_SUCCESS,
      payload: res.data
    });
    dispatch(loadUser());
  } catch (err) {
    dispatch({
      type: LOGIN_FAIL,
      payload: err.response.data.msg
    });
  }
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
  return { type: SET_LOADING };
};

import AuthorizeService from "../components/auth/AuthorizeService";
import axios from "axios";

import { USER_LOADING, USER_LOADED, LOGGED_OUT, USER_ERROR } from "./types";

export const loadUser = () => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.get(`api/profile/myinfo/${user.sub}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      dispatch({
        type: USER_LOADED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: USER_ERROR, payload: err.response });
    }
  };
};

// Deauthenticate user
export const logout = () => {
  return async dispatch => {
    dispatch({ type: LOGGED_OUT });
  };
};

// Set Loading
export const setLoading = () => {
  return { type: USER_LOADING };
};

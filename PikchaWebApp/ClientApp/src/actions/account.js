import AuthorizeService from "../components/auth/AuthorizeService";
import axios from "axios";

import {
  USER_LOADING,
  USER_UPDATED,
  USER_LOADED,
  LOGGED_OUT,
  USER_ERROR
} from "./types";

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

export const updateUserDetails = formData => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.put(`api/profile/${user.sub}`, formData, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json"
        }
      });

      dispatch({
        type: USER_UPDATED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: USER_ERROR, payload: err.response });
    }
  };
};

export const updateProfilePicture = formData => {
  return async dispatch => {
    try {
      dispatch(setLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.post(`api/profile/${user.sub}`, formData, {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json"
        }
      });

      dispatch({
        type: USER_UPDATED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: USER_ERROR, payload: err.response });
    }
  };
};

// ### Upload user avatar
// - ENDPOINT : api/profile/avatar/{userId}
// - METHOD : post
// - AUTHENTICATED : true
// - PARAMS : imageFile [file]
// - RESULTS : loggedinuserinfo
// - ERROR_CODES : 200, 404, 500

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

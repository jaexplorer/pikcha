import AuthorizeService from "../components/auth/AuthorizeService";
import axios from "axios";

import {
  USER_LOADING,
  USER_UPDATED,
  USER_LOADED,
  LOGGED_OUT,
  USER_ERROR,
  SIGNATURE_LOADED,
  SIGNATURE_LOADING
} from "./types";

export const loadUser = () => {
  return async dispatch => {
    try {
      dispatch(setUserLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.get(`api/profile/${user.sub}/myinfo`, {
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
      dispatch(setUserLoading());
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
      dispatch(setUserLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.post(`api/profile/${user.sub}/avatar`, formData, {
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

export const promoteToArtist = formData => {
  return async dispatch => {
    try {
      dispatch(setUserLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.post(
        `api/profile/${user.sub}/promote`,
        formData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json"
          }
        }
      );

      dispatch({
        type: USER_UPDATED,
        payload: res.data
      });
    } catch (err) {
      dispatch({ type: USER_ERROR, payload: err.response });
    }
  };
};

export const loadSignature = () => {
  return async dispatch => {
    try {
      dispatch(setSignatureLoading());
      const token = await AuthorizeService.getAccessToken();
      const user = await AuthorizeService.getUser();
      const res = await axios.get(`api/profile/${user.sub}/signature`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      dispatch({
        type: SIGNATURE_LOADED,
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

// Set User Loading
export const setUserLoading = () => {
  return { type: USER_LOADING };
};

// Set Signature Loading
export const setSignatureLoading = () => {
  return { type: SIGNATURE_LOADING };
};

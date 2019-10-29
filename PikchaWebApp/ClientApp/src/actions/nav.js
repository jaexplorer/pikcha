import {
  CREATE_PROFILE_DROPDOWN,
  REMOVE_PROFILE_DROPDOWN,
  CREATE_CART_DROPDOWN,
  REMOVE_CART_DROPDOWN
} from "./types";

// Create Profile Dropdown
export const createProfileDropdown = () => {
  return dispatch => {
    dispatch({
      type: CREATE_PROFILE_DROPDOWN
    });
  };
};

// Remove Profile Dropdown
export const removeProfileDropdown = () => {
  return dispatch => {
    dispatch({
      type: REMOVE_PROFILE_DROPDOWN
    });
  };
};

// Create Cart Dropdown
export const createCartDropdown = () => {
  return dispatch => {
    dispatch({
      type: CREATE_CART_DROPDOWN
    });
  };
};

// Remove Cart Dropdown
export const removeCartDropdown = () => {
  return dispatch => {
    dispatch({
      type: REMOVE_CART_DROPDOWN
    });
  };
};

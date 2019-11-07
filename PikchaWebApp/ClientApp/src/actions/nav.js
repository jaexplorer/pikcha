import { DROPDOWN_OPEN, DROPDOWN_CLOSED } from "./types";

// Open Menu
export const openMenu = () => {
  return dispatch => {
    dispatch({
      type: DROPDOWN_OPEN,
      payload: "Menu"
    });
  };
};

// Open Cart
export const openCart = () => {
  return dispatch => {
    dispatch({
      type: DROPDOWN_OPEN,
      payload: "Cart"
    });
  };
};

// Open Profile
export const openProfile = () => {
  return dispatch => {
    dispatch({
      type: DROPDOWN_OPEN,
      payload: "Profile"
    });
  };
};

// Open Following
export const openFollowing = () => {
  return dispatch => {
    dispatch({
      type: DROPDOWN_OPEN,
      payload: "Following"
    });
  };
};

// Close Dropdown
export const closeDropdown = () => {
  return dispatch => {
    dispatch({
      type: DROPDOWN_CLOSED
    });
  };
};

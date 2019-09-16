import { CREATE_DROPDOWN, REMOVE_DROPDOWN } from "./types";

// Create Dropdown
export const createDropdown = () => {
  return dispatch => {
    dispatch({
      type: CREATE_DROPDOWN
    });
  };
};

// Remove Dropdown
export const removeDropdown = () => {
  return dispatch => {
    dispatch({
      type: REMOVE_DROPDOWN
    });
  };
};

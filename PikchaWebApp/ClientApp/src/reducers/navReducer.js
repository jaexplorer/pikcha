import {
  CREATE_PROFILE_DROPDOWN,
  REMOVE_PROFILE_DROPDOWN,
  CREATE_CART_DROPDOWN,
  REMOVE_CART_DROPDOWN
} from "../actions/types";

const initialState = {
  profileDropdown: false,
  cartDropdown: false
};

export default (state = initialState, { type }) => {
  switch (type) {
    case CREATE_PROFILE_DROPDOWN:
      return {
        ...state,
        profileDropdown: true
      };

    case REMOVE_PROFILE_DROPDOWN:
      return {
        ...state,
        profileDropdown: false
      };

    case CREATE_CART_DROPDOWN:
      return {
        ...state,
        cartDropdown: true
      };

    case REMOVE_CART_DROPDOWN:
      return {
        ...state,
        cartDropdown: false
      };

    default:
      return state;
  }
};

import {
  GET_PRODUCT,
  PRODUCT_ERROR,
  PRODUCT_LOADING,
  SET_SIZE,
  SET_MATERIAL,
  SET_FRAME,
  SET_BORDER,
  SET_FINISH,
  CLEAR_CUSTOMISATIONS
} from "../actions/types";

const initialState = {
  product: null,
  size: "medium",
  material: "paper",
  frame: "none",
  border: "none",
  finish: "none",
  loading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case GET_PRODUCT:
      return {
        ...state,
        product: payload,
        loading: false
      };
    case SET_SIZE:
      return {
        ...state,
        size: payload
      };
    case SET_MATERIAL:
      return {
        ...state,
        material: payload
      };
    case SET_FRAME:
      return {
        ...state,
        frame: payload
      };
    case SET_BORDER:
      return {
        ...state,
        border: payload
      };
    case SET_FINISH:
      return {
        ...state,
        finish: payload
      };
    case PRODUCT_ERROR:
      console.error(payload);
      return {
        ...state,
        error: payload,
        loading: false
      };
    case CLEAR_CUSTOMISATIONS:
      return {
        ...state,
        size: "medium",
        material: "paper",
        frame: "none",
        border: "none",
        finish: "none"
      };
    case PRODUCT_LOADING:
      return {
        ...state,
        loading: true
      };

    default:
      return state;
  }
};

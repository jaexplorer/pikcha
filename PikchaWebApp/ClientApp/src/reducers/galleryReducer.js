import {
  GET_PHOTOS,
  PHOTO_SELECTED,
  PHOTO_DESELECTED,
  PHOTOS_LOADING,
  PHOTOS_ERROR
} from "../actions/types";

const initialState = {
  photos: null,
  selected: null,
  loading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case GET_PHOTOS:
      return {
        ...state,
        photos: payload,
        loading: false
      };
    case PHOTO_SELECTED:
      return {
        ...state,
        selected: payload
      };
    case PHOTO_DESELECTED:
      return {
        ...state,
        selected: null
      };
    case PHOTOS_ERROR:
      console.error(payload);
      return {
        ...state,
        error: payload,
        loading: false
      };
    case PHOTOS_LOADING:
      return {
        ...state,
        loading: true
      };

    default:
      return state;
  }
};

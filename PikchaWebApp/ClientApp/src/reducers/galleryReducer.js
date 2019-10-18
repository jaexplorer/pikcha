import {
  GET_PHOTOS,
  PHOTO_SELECTED,
  PHOTO_DESELECTED,
  PHOTOS_LOADING,
  PHOTOS_ERROR
} from "../actions/types";

const initialState = {
  photos: null,
  count: 15,
  start: 1,
  selected: null,
  loading: true,
  error: null,
  hasMore: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case GET_PHOTOS:
      payload.forEach(d => {
        const randomised = Math.random() * (30 - 15) + 15;
        d.height = randomised;
      });
      return {
        ...state,
        photos: state.photos == null ? payload : [...state.photos, ...payload],

        loading: false,
        start: state.start + state.count,
        hasMore: payload.length ? true : false
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

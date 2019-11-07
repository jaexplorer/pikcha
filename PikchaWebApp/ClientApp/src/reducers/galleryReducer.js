import {
  GALLERY_LOADED,
  GALLERY_LOADING,
  GALLERY_ERROR,
  GALLERY_RESET
} from "../actions/types";

const initialState = {
  photos: [],
  count: 15,
  start: 1,
  loading: true,
  error: null,
  hasMore: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case GALLERY_LOADED:
      payload.forEach(d => {
        const randomised = Math.random() * (30 - 15) + 15;
        d.height = randomised;
      });
      return {
        ...state,
        photos: [...state.photos, ...payload],
        loading: false,
        start: state.start + state.count
      };
    case GALLERY_ERROR:
      !payload.data === "You have reached the end." && console.error(payload);
      return {
        ...state,
        error: payload,
        loading: false,
        hasMore: !payload.data === "You have reached the end."
      };
    case GALLERY_LOADING:
      return {
        ...state,
        loading: true
      };
    case GALLERY_RESET:
      return initialState;

    default:
      return state;
  }
};

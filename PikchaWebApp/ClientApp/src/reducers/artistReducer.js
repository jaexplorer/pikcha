import { ARTIST_LOADED, ARTIST_LOADING, ARTIST_ERROR } from "../actions/types";

const initialState = {
  artist: null,
  loading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case ARTIST_LOADED:
      return {
        ...state,
        artist: payload,
        loading: false
      };
    case ARTIST_ERROR:
      console.error(payload);
      return {
        ...state,
        error: payload,
        loading: false
      };
    case ARTIST_LOADING:
      return {
        ...state,
        loading: true
      };

    default:
      return state;
  }
};

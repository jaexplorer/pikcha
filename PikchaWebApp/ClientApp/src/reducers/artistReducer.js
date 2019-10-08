import { GET_ARTIST, ARTIST_LOADING, ARTIST_ERROR } from "../actions/types";

const initialState = {
  artist: null,
  loading: true,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case GET_ARTIST:
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

import {
  ARTIST100_LOADED,
  ARTIST100_LOADING,
  ARTIST100_ERROR,
  ARTIST100_RESET
} from "../actions/types";

const initialState = {
  top100: [],
  count: 15,
  start: 1,
  loading: true,
  error: null,
  hasMore: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case ARTIST100_LOADED:
      return {
        ...state,
        top100: [...state.top100, ...payload],
        loading: false,
        start: state.start + state.count
      };

    case ARTIST100_ERROR:
      !payload.data === "You have reached the end." && console.error(payload);
      return {
        ...state,
        error: payload,
        loading: false,
        hasMore: !payload.data === "You have reached the end."
      };
    case ARTIST100_LOADING:
      return {
        ...state,
        loading: true
      };
    case ARTIST100_RESET:
      return initialState;

    default:
      return state;
  }
};

import {
  PROFILE_LOADED,
  PROFILE_LOADING,
  PROFILE_ERROR,
  PROFILE_RESET,
  ARTIST_PHOTOS_LOADED,
  ARTIST_PHOTOS_LOADING,
  ARTIST_PHOTOS_ERROR,
  ARTIST_PHOTOS_RESET,
  PROFILE_UPDATED
} from "../actions/types";

const initialState = {
  artist: null,
  photos: [],
  start: 1,
  count: 15,
  profileLoading: true,
  photosLoading: true,
  profileError: null,
  photosError: null,
  hasMore: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case PROFILE_UPDATED:
    case PROFILE_LOADED:
      return {
        ...state,
        artist: payload,
        profileLoading: false
      };
    case PROFILE_LOADING:
      return {
        ...state,
        profileLoading: true
      };
    case PROFILE_ERROR:
      console.log(payload);
      return {
        ...state,
        profileError: payload,
        profileLoading: false
      };
    case PROFILE_RESET:
      return {
        ...state,
        artist: null,
        profileLoading: true,
        profileError: null
      };
    case ARTIST_PHOTOS_LOADED:
      payload.forEach(d => {
        const randomised = Math.random() * (30 - 15) + 15;
        d.height = randomised;
      });
      return {
        ...state,
        photos: [...state.photos, ...payload],
        photosLoading: false,
        start: state.start + state.count
      };
    case ARTIST_PHOTOS_LOADING:
      return {
        ...state,
        photosLoading: true
      };
    case ARTIST_PHOTOS_ERROR:
      !payload.data === "You have reached the end." && console.error(payload);
      return {
        ...state,
        photosError: payload,
        loading: false,
        hasMore: !payload.data === "You have reached the end."
      };
    case ARTIST_PHOTOS_RESET:
      return {
        ...state,
        photos: [],
        start: 1,
        count: 15,
        photosLoading: true,
        photosError: null,
        hasMore: true
      };

    default:
      return state;
  }
};

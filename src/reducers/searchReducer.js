import { handleActions } from "redux-actions";

export default handleActions(
    {
        FETCH_SEARCH_SUCCESS: (state, action) => ({
            ...state,
            isFetchingSearch: false,
            search: action.payload.search
        }),
        FETCH_SEARCH_FAILURE: (state, action) => ({
            ...state,
            isFetchingSearch: false,
            error: action.payload.error
        }),
        FETCH_SEARCH_STARTED: (state, action) => ({
            ...state,
            isFetchingSearch: true
        })
    },
    {
        isFetchingSearch: false,
        search: [],
        error: null
    }
);

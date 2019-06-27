import { handleActions } from "redux-actions";

export default handleActions(
    {
        FETCH_PAGE_SUCCESS: (state, action) => ({
            ...state,
            isFetchingPage: false,
            page: action.payload.page
        }),
        FETCH_PAGE_FAILURE: (state, action) => ({
            ...state,
            isFetchingPage: false,
            error: action.payload.error
        }),
        FETCH_PAGE_STARTED: (state, action) => ({
            ...state,
            isFetchingPage: true
        })
    },
    {
        isFetchingPage: false,
        page: { body: [] },
        error: null
    }
);

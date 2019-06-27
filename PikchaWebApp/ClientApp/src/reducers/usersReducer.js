import { handleActions } from "redux-actions";

export default handleActions(
    {
        FETCH_USER_SUCCESS: (state, action) => ({
            ...state,
            isFetchingUser: false,
            user: action.payload.user
        }),
        FETCH_USER_FAILURE: (state, action) => ({
            ...state,
            isFetchingUser: false,
            error: action.payload.error
        }),
        FETCH_USER_STARTED: (state, action) => ({
            ...state,
            isFetchingUser: true
        }),
        GET_IDENTITY_SUCCESS: (state, action) => ({
            ...state,
            isFetchingCurrent: false,
            signedIn: true,
            userId: action.payload.userId,
            currentUser: action.payload.currentUser
        }),
        GET_IDENTITY_FAILURE: (state, action) => ({
            ...state,
            isFetchingCurrent: false,
            error: action.payload.error
        }),
        GET_IDENTITY_STARTED: (state, action) => ({
            ...state,
            isFetchingCurrent: true
        }),
        SIGN_IN_SUCCESS: (state, action) => ({
            ...state,
            isFetchingCurrent: false,
            signedIn: true,
            userId: action.payload.id,
            duration: action.payload.duration
        }),
        SIGN_IN_FAILURE: (state, action) => ({
            ...state,
            error: action.payload.error,
            isFetchingCurrent: false
        }),
        SIGN_IN_STARTED: (state, action) => ({
            ...state,
            isFetchingCurrent: true
        }),
        LOGOUT_STARTED: (state, action) => ({
            ...state,
            signedIn: false,
            userId: null,
            hash: null
        })
    },
    {
        isFetchingUser: false,
        isFetchingCurrent: true,
        signedIn: false,
        userId: null,
        duration: null,
        user: {},
        currentUser: {},
        error: null
    }
);

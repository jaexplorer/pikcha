import { createActions } from "redux-actions";
import { push } from 'connected-react-router'
import clientApi from "../helpers/clientApi";
import elementApi from "../helpers/elementApi";

export const {
    fetchUserSuccess,
    fetchUserFailure,
    fetchUserStarted,
    getIdentitySuccess,
    getIdentityFailure,
    getIdentityStarted,
    signInSuccess,
    signInFailure,
    signInStarted,
    logoutStarted,
    logoutSuccess,
    logoutFailure,
} = createActions(
    {
        FETCH_USER_SUCCESS: user => ({ user }),
        FETCH_USER_FAILURE: error => ({ error }),
        GET_IDENTITY_SUCCESS: (currentUser, userId) => ({
            userId,
            currentUser
        }),
        GET_IDENTITY_FAILURE: error => ({ error }),
        SIGN_IN_SUCCESS: (id, duration) => ({
            id,
            duration
        }),
        SIGN_IN_FAILURE: error => ({ error }),
        LOGOUT_FAILURE: error => ({ error })
    },
    "FETCH_USER_STARTED",
    "GET_IDENTITY_STARTED",
    "SIGN_IN_STARTED",
    "LOGOUT_STARTED",
    "LOGOUT_SUCCESS"
);

export const signIn = values => {
    let data = JSON.stringify({
        username: values.email,
        password: values.password
    });

    return async dispatch => {
        dispatch(signInStarted());

        try {
            const response = await clientApi.post('user/login', data)
            dispatch(
                signInSuccess(response.data.id, response.data.duration)
            );
            dispatch(getIdentity(response.data.userId));
            dispatch(push('/'))
        } catch (error) {
            dispatch(signInFailure(error));
        };
    };
};

export const logout = () => {
    return async dispatch => {
        dispatch(push('/'))
        dispatch(logoutStarted());

        try {
            const response = await clientApi.get('user/logout')
            dispatch(logoutSuccess());
        } catch (error) {
            dispatch(logoutFailure(error));
        };
    };
};

export const saveUser = (values, userId) => {
    let data = {};

    if (userId !== undefined) {
        data["userId"] = userId;
    }

    Object.keys(values).map((key, index) => {
        if (values[key].length > 0) {
            return data[key] = values[key];
        }

        return false;
    });

    data = JSON.stringify(data);

    return async dispatch => {
        dispatch(signInStarted());

        try {
            const response = await clientApi.post('user/save-user', data)
            dispatch(signInSuccess(response.data.userId, response.data.hash));
            dispatch(getIdentity(response.data.userId));
            dispatch(push('/'))
        } catch (error) {
            dispatch(signInFailure(error));
        };
    };
};

export const forgotPassword = values => {
    let data = JSON.stringify({
        loginName: values.email
    });

    return async dispatch => {
        dispatch(signInStarted());

        try {
            const response = await clientApi.post('user/forgot-password', data)
            dispatch(fetchUserSuccess(response.data));
        } catch (error) {
            dispatch(fetchUserFailure(error));
        };
    };
};

export const setPassword = (values, code, id) => {
    let data = JSON.stringify({
        newPassword: values.newPassword,
        code: code,
        id: id
    });

    return async dispatch => {
        dispatch(signInStarted());

        try {
            const response = await clientApi.post('user/set-password', data)
            dispatch(fetchUserSuccess(response.data));
        } catch (error) {
            dispatch(fetchUserFailure(error));
        };
    };
};

export const getIdentity = () => {
    let data = JSON.stringify({});

    return async dispatch => {
        dispatch(getIdentityStarted());

        try {
            const response = await clientApi.post('user/get-identity', data)
            dispatch(getIdentitySuccess(response.data.currentUser));
        } catch (error) {
            dispatch(getIdentityFailure(error));
        };
    };
};

export const fetchUser = id => {
    return async dispatch => {
        dispatch(fetchUserStarted());

        try {
            const response = await elementApi.get(`user/${id}`)
            dispatch(fetchUserSuccess(response.data));
        } catch (error) {
            dispatch(fetchUserFailure(error));
        };
    };
};

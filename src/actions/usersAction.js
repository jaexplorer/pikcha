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

    return dispatch => {
        dispatch(signInStarted());

        clientApi.post('user/login', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(
                        signInSuccess(response.data.id, response.data.duration)
                    );
                    dispatch(getIdentity(response.data.userId));
                    dispatch(push('/'))
                } else {
                    dispatch(signInFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(signInFailure(err));
            });
    };
};

export const logout = () => {
    return dispatch => {
        dispatch(push('/'))
        dispatch(logoutStarted());

        clientApi.get('user/logout')
            .then(response => {
                if (response.data.success) {
                    dispatch(logoutSuccess());
                } else {
                    dispatch(logoutFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(logoutFailure(err));
            });
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

    return dispatch => {
        dispatch(signInStarted());

        clientApi.post('user/save-user', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(signInSuccess(response.data.userId, response.data.hash));
                    dispatch(getIdentity(response.data.userId));
                    dispatch(push('/'))
                } else {
                    dispatch(signInFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(signInFailure(err));
            });
    };
};

export const forgotPassword = values => {
    let data = JSON.stringify({
        loginName: values.email
    });

    return dispatch => {
        dispatch(signInStarted());

        clientApi.post('user/forgot-password', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(fetchUserSuccess(response.data));
                } else {
                    dispatch(fetchUserFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(fetchUserFailure(err));
            });
    };
};

export const setPassword = (values, code, id) => {
    let data = JSON.stringify({
        newPassword: values.newPassword,
        code: code,
        id: id
    });

    return dispatch => {
        dispatch(signInStarted());

        clientApi.post('user/set-password', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(fetchUserSuccess(response.data));
                } else {
                    dispatch(fetchUserFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(fetchUserFailure(err));
            });
    };
};

export const getIdentity = () => {
    let data = JSON.stringify({});

    return dispatch => {
        dispatch(getIdentityStarted());

        clientApi.post('user/get-identity', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(getIdentitySuccess(response.data.currentUser));
                } else {
                    dispatch(getIdentityFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(getIdentityFailure(err));
            });
    };
};

export const fetchUser = id => {
    return dispatch => {
        dispatch(fetchUserStarted());

        elementApi.get(`user/${id}`)
            .then(response => {
                dispatch(fetchUserSuccess(response.data));
            })
            .catch(err => {
                dispatch(fetchUserFailure(err));
            });
    };
};

import { createActions } from "redux-actions";
import elementApi from "../helpers/elementApi";

export const {
    fetchPageSuccess,
    fetchPageFailure,
    fetchPageStarted
} = createActions(
    {
        FETCH_PAGE_SUCCESS: page => ({ page }),
        FETCH_PAGE_FAILURE: error => ({ error })
    },
    "FETCH_PAGE_STARTED"
);

export const fetchPage = slug => {
    return dispatch => {
        dispatch(fetchPageStarted());

        elementApi.get(`pages/${slug}`)
            .then(response => {
                dispatch(fetchPageSuccess(response.data));
            })
            .catch(err => {
                dispatch(fetchPageFailure(err));
            });

    };
};

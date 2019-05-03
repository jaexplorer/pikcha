import { createActions } from "redux-actions";
import elementApi from "../helpers/elementApi";

export const {
    fetchSearchSuccess,
    fetchSearchFailure,
    fetchSearchStarted
} = createActions(
    {
        FETCH_SEARCH_SUCCESS: search => ({ search }),
        FETCH_SEARCH_FAILURE: error => ({ error })
    },
    "FETCH_SEARCH_STARTED"
);

export const fetchSearch = query => {
    return dispatch => {
        dispatch(fetchSearchStarted());

        elementApi.get(`search?q=${query}`)
            .then(response => {
                dispatch(fetchSearchSuccess(response.data.data));
            })
            .catch(err => {
                dispatch(fetchSearchFailure(err));
            });
    };
};

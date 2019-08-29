// import { createActions } from "redux-actions";

// export const {
//     fetchSearchSuccess,
//     fetchSearchFailure,
//     fetchSearchStarted
// } = createActions(
//     {
//         FETCH_SEARCH_SUCCESS: search => ({ search }),
//         FETCH_SEARCH_FAILURE: error => ({ error })
//     },
//     "FETCH_SEARCH_STARTED"
// );

// export const fetchSearch = query => {
//     return async dispatch => {
//         dispatch(fetchSearchStarted());

//         try {
//             const response = await elementApi.get(`search?q=${query}`)
//             dispatch(fetchSearchSuccess(response.data.data));
//         } catch (error) {
//             dispatch(fetchSearchFailure(error));
//         };
//     };
// };

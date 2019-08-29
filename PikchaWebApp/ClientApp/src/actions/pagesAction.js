// import { createActions } from "redux-actions";

// export const {
//     fetchPageSuccess,
//     fetchPageFailure,
//     fetchPageStarted
// } = createActions(
//     {
//         FETCH_PAGE_SUCCESS: page => ({ page }),
//         FETCH_PAGE_FAILURE: error => ({ error })
//     },
//     "FETCH_PAGE_STARTED"
// );

// export const fetchPage = slug => {
//     return async dispatch => {
//         dispatch(fetchPageStarted());

//         try {
//             const response = await elementApi.get(`pages/${slug}`)
//             dispatch(fetchPageSuccess(response.data));
//         } catch (error) {
//             dispatch(fetchPageFailure(error));
//         };
//     };
// };

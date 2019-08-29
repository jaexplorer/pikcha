// import { createActions } from "redux-actions";

// export const {
//     uploadProductSuccess,
//     uploadProductFailure,
//     uploadProductStarted
// } = createActions(
//     {
//         UPLOAD_PRODUCT_SUCCESS: products => ({ products }),
//         UPLOAD_PRODUCT_FAILURE: error => ({ error })
//     },
//     "UPLOAD_PRODUCT_STARTED"
// );

// export const uploadProduct = values => {
//     let data = {
//         typeId: 1,
//         title: values.title,
//         author: 1
//     };

//     data["fields"]["author"] = values.userId;
//     data["variants"]["new1"]["title"] = values.title;
//     data["variants"]["new1"]["sku"] = values.title;
//     data["variants"]["new1"]["price"] = 20;
//     data["variants"]["new1"]["stock"] = 100;

//     data = JSON.stringify(data);

//     return async dispatch => {
//         dispatch(uploadProductStarted());

//         try {
//             const response = await clientApi.post('products/save-product', data)
//             dispatch(uploadProductSuccess(response.data.cart));
//         } catch (error) {
//             dispatch(uploadProductFailure(error));
//         };
// };
// };

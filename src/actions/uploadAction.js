import { createActions } from "redux-actions";
import clientApi from "../helpers/clientApi";

export const {
    uploadProductSuccess,
    uploadProductFailure,
    uploadProductStarted
} = createActions(
    {
        UPLOAD_PRODUCT_SUCCESS: products => ({ products }),
        UPLOAD_PRODUCT_FAILURE: error => ({ error })
    },
    "UPLOAD_PRODUCT_STARTED"
);

export const uploadProduct = values => {
    let data = {
        typeId: 1,
        title: values.title,
        author: 1
    };

    data["fields"]["author"] = values.userId;
    data["variants"]["new1"]["title"] = values.title;
    data["variants"]["new1"]["sku"] = values.title;
    data["variants"]["new1"]["price"] = 20;
    data["variants"]["new1"]["stock"] = 100;

    data = JSON.stringify(data);

    return dispatch => {
        dispatch(uploadProductStarted());

        clientApi.post('products/save-product', data)
            .then(response => {
                if (response.data.success) {
                    dispatch(uploadProductSuccess(response.data.cart));
                } else {
                    dispatch(uploadProductFailure(response.data.error));
                }
            })
            .catch(err => {
                dispatch(uploadProductFailure(err));
            });
    };
};

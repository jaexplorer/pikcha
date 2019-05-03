import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import users from "./usersReducer";
import cart from "./cartReducer";
import products from "./productsReducer";
import search from "./searchReducer";
import pages from "./pagesReducer";
// import checkout from './checkoutReducer';
// import orders from './ordersReducer';

export default history =>
    combineReducers({
        router: connectRouter(history),
        users,
        cart,
        products,
        search,
        pages
        // checkout,
        // orders,
    });

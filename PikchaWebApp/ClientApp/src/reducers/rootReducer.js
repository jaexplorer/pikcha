import { combineReducers } from "redux";
import authReducer from "./authReducer";
import alertReducer from "./alertReducer";
import navReducer from "./navReducer";
import galleryReducer from "./galleryReducer";
import productReducer from "./productReducer";
import cartReducer from "./cartReducer";
import artistReducer from "./artistReducer";
import modalReducer from "./modalReducer";
import accountReducer from "./accountReducer";
import top100Reducer from "./top100Reducer";

export default combineReducers({
  authReducer,
  alertReducer,
  navReducer,
  galleryReducer,
  productReducer,
  cartReducer,
  artistReducer,
  modalReducer,
  accountReducer,
  top100Reducer
});

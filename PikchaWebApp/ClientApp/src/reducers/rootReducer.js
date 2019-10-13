import { combineReducers } from "redux";
import authReducer from "./authReducer";
import alertReducer from "./alertReducer";
import navReducer from "./navReducer";
import galleryReducer from "./galleryReducer";
import productReducer from "./productReducer";
import cartReducer from "./cartReducer";
import artistReducer from "./artistReducer";
import accountReducer from "./accountReducer";

export default combineReducers({
  authReducer,
  alertReducer,
  navReducer,
  galleryReducer,
  productReducer,
  cartReducer,
  artistReducer,
  accountReducer
});

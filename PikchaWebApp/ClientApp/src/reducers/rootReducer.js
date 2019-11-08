import { combineReducers } from "redux";
import navReducer from "./navReducer";
import galleryReducer from "./galleryReducer";
import accountReducer from "./accountReducer";
import authReducer from "./authReducer";
import alertReducer from "./alertReducer";
import modalReducer from "./modalReducer";
import pikcha100Reducer from "./pikcha100Reducer";
import artist100Reducer from "./artist100Reducer";
import profileReducer from "./profileReducer";

export default combineReducers({
  galleryReducer,
  accountReducer,
  navReducer,
  authReducer,
  alertReducer,
  modalReducer,
  pikcha100Reducer,
  artist100Reducer,
  profileReducer
});

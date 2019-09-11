import { combineReducers } from "redux";
import authReducer from "./authReducer";
import alertReducer from "./alertReducer";
import navReducer from "./navReducer";

export default combineReducers({
  authReducer,
  alertReducer,
  navReducer
});

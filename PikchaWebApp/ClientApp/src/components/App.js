import React, { useEffect } from "react";
import "../assets/scss/main/main.css";
import { Provider } from "react-redux";
import store from "../store";
import { BrowserRouter as Router, Route } from "react-router-dom";
import NavBar from "./layout/navbar/Navbar";
import Alerts from "./Alerts";
import Modal from "./layout/modal/Modal";
import SideMenu from "./layout/account/SideMenu";
import Routes from "./routes/Routes";
import SmoothScroll from "./SmoothScroll";
import { authenticate } from "../actions/auth";

const App = () => {
  useEffect(() => {
    setTimeout(() => {
      store.dispatch(authenticate());
    }, 1000);
  }, []);

  return (
    <Provider store={store}>
      <Router>
        <NavBar />
        <Route strict path='/account' component={SideMenu} />
        <Alerts />
        <Modal />
        <SmoothScroll>
          <Routes />
        </SmoothScroll>
      </Router>
    </Provider>
  );
};

export default App;

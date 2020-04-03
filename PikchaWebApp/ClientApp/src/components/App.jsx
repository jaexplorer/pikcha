import React, { useEffect } from "react";
import { connect } from "react-redux";
import { Router, Route } from "react-router-dom";
import history from "../components/routes/history";
import NavBar from "./layout/navbar/Navbar";
import Alerts from "./common/Alerts";
import Modal from "./layout/modal/Modal";
import SideMenu from "./layout/account/SideMenu";
import Routes from "./routes/Routes";
import SmoothScroll from "./common/SmoothScroll";
import { authenticate } from "../actions/auth";
import "../assets/scss/main/main.css";

const App = ({ modal, authenticate }) => {
  useEffect(() => {
    setTimeout(() => {
      authenticate();
    }, 1000);
  }, []);

  return (
    <Router history={history}>
      <Alerts />
      <Modal />
      <div className={`mainview ${modal.type ? "inactive" : "active"}`}>
        <NavBar />
        <Route strict path='/account' component={SideMenu} />
        <SmoothScroll>
          <Routes />
        </SmoothScroll>
      </div>
    </Router>
  );
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps, { authenticate })(App);

import React, { useEffect } from "react";
import "../assets/scss/main/main.css";
import { Provider } from "react-redux";
import store from "../store";
import { BrowserRouter as Router } from "react-router-dom";
import NavBar from "./layout/navbar/Navbar";
import Alerts from "./layout/Alerts";
import { authenticate } from "../actions/auth";
import Routes from "./Routes/Routes";

const App = () => {
  useEffect(() => {
    setTimeout(() => {
      store.dispatch(authenticate());
    }, 1000);
  }, []);

  return (
    <Provider store={store}>
      <Router>
        <div className='frame-container'>
          <NavBar />
          <Alerts />
          <Routes />
        </div>
      </Router>
    </Provider>
  );
};

export default App;

import React, { useEffect } from "react";
import { Provider } from "react-redux";
import store from "../store";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "./pages/Home";
import Register from "./auth/Register";
import Login from "./auth/Login";
import Alerts from "./layout/Alerts";
import { loadUser, logout } from "../actions/auth";

// import LocomotiveScroll from "locomotive-scroll";

const App = () => {
  // useEffect(() => {
  //   // eslint-disable-next-line
  //   const scroll = new LocomotiveScroll({
  //     el: document.querySelector("#js-scroll"),
  //     smooth: true
  //   });
  // });
  useEffect(() => {
    if (localStorage.token) {
      store.dispatch(loadUser());
    } else {
      store.dispatch(logout());
    }
  });

  return (
    <Provider store={store}>
      <Router>
        <div id='js-scroll'>
          <Alerts />
          <Switch>
            <Route exact path='/' component={Home} />
            <Route exact path='/register' component={Register} />
            <Route exact path='/login' component={Login} />
          </Switch>
        </div>
      </Router>
    </Provider>
  );
};

export default App;

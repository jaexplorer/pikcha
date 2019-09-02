import React, { Fragment, useEffect } from "react";
import { Provider } from "react-redux";
import store from "store";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "components/pages/Home";
import Register from "components/auth/Register";
import Login from "components/auth/Login";

import LocomotiveScroll from "locomotive-scroll";

const App = () => {
  useEffect(() => {
    // eslint-disable-next-line
    const scroll = new LocomotiveScroll({
      el: document.querySelector("#js-scroll"),
      smooth: true
    });
  });

  return (
    <Provider store={store}>
      <Router>
        <div id='js-scroll'>
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

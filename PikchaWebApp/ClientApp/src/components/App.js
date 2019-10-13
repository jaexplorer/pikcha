import React, { useEffect } from "react";
import "../assets/scss/main/main.css";
import { Provider } from "react-redux";
import store from "../store";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "./pages/Home";
import Pikcha100 from "./pages/Pikcha100";
import Artist100 from "./pages/Artist100";
import Register from "./auth/Register";
import Login from "./auth/Login";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Profile from "./pages/Profile";
import Product from "./pages/Product";
import PrivateRoute from "./layout/routing/PrivateRoute";
import Customise from "./pages/Customise";
import NotFound from "./pages/NotFound";
import NavBar from "./layout/navbar/Navbar";
import Alerts from "./layout/Alerts";
import { loadUser, logout } from "../actions/auth";

const App = () => {
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
        <div className='frame-container' id='js-scroll'>
          <NavBar />
          <Alerts />
          <Switch>
            <Route exact path='/' component={Home} />
            <Route exact path='/about' component={About} />
            <Route exact path='/contact' component={Contact} />
            <Route exact path='/pikcha100' component={Pikcha100} />
            <Route exact path='/artist100' component={Artist100} />
            <Route exact path='/register' component={Register} />
            <Route exact path='/login' component={Login} /> 
            <Route exact path='/profile/:userid' component={Profile} />
            <Route exact path='/product/:productid' component={Product} />
            <PrivateRoute
              exact
              path='/customise/:productid'
              component={Customise}
            />
            <Route component={NotFound} />
          </Switch>
        </div>
      </Router>
    </Provider>
  );
};

export default App;

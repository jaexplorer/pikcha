import React, { useEffect } from "react";
import "../assets/scss/main/main.css";
import { Provider } from "react-redux";
import store from "../store";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "./pages/Home";
import Pikcha100 from "./pages/Pikcha100";
import Artist100 from "./pages/Artist100";
import About from "./pages/About";
import Contact from "./pages/Contact";
import Profile from "./pages/Profile";
import Product from "./pages/Product";
import PrivateRoute from "./layout/routing/PrivateRoute";
import Customise from "./pages/Customise";
import Account from "./pages/Account";
import NotFound from "./pages/NotFound";
import NavBar from "./layout/navbar/Navbar";
import Alerts from "./layout/Alerts";
import { loaduser } from "../actions/auth";
import { ApplicationPaths } from "./api-authorization/ApiAuthorizationConstants";
import ApiAuthorizationRoutes from "./api-authorization/ApiAuthorizationRoutes";
import AuthorizeRoute from "./api-authorization/AuthorizeRoute";

const App = () => {
  useEffect(() => {
    setTimeout(() => {
      store.dispatch(loaduser());
    }, 1000);
  }, []);

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
            <Route exact path='/profile/:userid' component={Profile} />
            <Route exact path='/product/:productid' component={Product} />
            <Route
              path={ApplicationPaths.ApiAuthorizationPrefix}
              component={ApiAuthorizationRoutes}
            />

            <AuthorizeRoute
              exact
              path='/customise/:productid'
              component={Customise}
            />
            <Route strict path='/account' component={Account} />
            <Route component={NotFound} />
          </Switch>
        </div>
      </Router>
    </Provider>
  );
};

export default App;

import React from "react";
import { Route, Switch } from "react-router-dom";
import Home from "../pages/Home";
import Pikcha100 from "../pages/Pikcha100";
import Artist100 from "../pages/Artist100";
import About from "../pages/About";
import Contact from "../pages/Contact";
import Profile from "../pages/Profile";
import Product from "../pages/Product";
import Customise from "../pages/Customise";
import Account from "../pages/Account";
import NotFound from "../pages/NotFound";
import { ApplicationPaths } from "../auth/ApiAuthorizationConstants";
import ApiAuthorizationRoutes from "../auth/ApiAuthorizationRoutes";
import AuthorizeRoute from "../auth/AuthorizeRoute";

const Routes = () => {
  return (
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
  );
};

export default Routes;
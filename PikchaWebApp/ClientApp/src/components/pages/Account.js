import React, { Fragment } from "react";
import { connect } from "react-redux";
import { Route, Switch, Redirect } from "react-router-dom";
import SideMenu from "../layout/account/SideMenu";
import BackArrow from "../layout/BackArrow";
import MyDetails from "../layout/account/MyDetails";
import MyPhotos from "../layout/account/MyPhotos";
import MyOrders from "../layout/account/MyOrders";
import PaymentDetails from "../layout/account/PaymentDetails";
import Settings from "../layout/account/Settings";
import NotFound from "./NotFound";

const Account = ({ auth, match }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }
  return (
    <Fragment>
      <SideMenu />
      <BackArrow />
      <div className='main-container'>
        <div className='main-content-container'>
          <div className='account-container'>
            <Switch>
              <Route exact path='/account' component={MyDetails} />
              <Route exact path={`${match.path}/photos`} component={MyPhotos} />
              <Route exact path={`${match.path}/orders`} component={MyOrders} />
              <Route
                exact
                path={`${match.path}/payment`}
                component={PaymentDetails}
              />
              <Route
                exact
                path={`${match.path}/settings`}
                component={Settings}
              />
              <Route
                component={() => (
                  <Redirect to='/notFound' component={NotFound} />
                )}
              />
            </Switch>
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Account);

import React, { Fragment } from "react";
import { connect } from "react-redux";
import { Route, Switch, Redirect } from "react-router-dom";
import SideMenu from "../layout/account/SideMenu";
import BackArrow from "../layout/BackArrow";
import MyDetails from "../layout/account/users/MyDetails";
import MyPhotos from "../layout/account/users/MyPhotos";
import MyOrders from "../layout/account/users/MyOrders";
import PaymentDetails from "../layout/account/users/PaymentDetails";
import Settings from "../layout/account/users/Settings";
import BankDetails from "../layout/account/artists/BankDetails";
import Socials from "../layout/account/artists/Socials";
import ArtistsPhotos from "../layout/account/artists/ArtistPhotos";
import Stats from "../layout/account/artists/Stats";
import ArtistSettings from "../layout/account/artists/ArtistSettings";
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
                exact
                path={`${match.path}/bank`}
                component={BankDetails}
              />
              <Route exact path={`${match.path}/socials`} component={Socials} />
              <Route
                exact
                path={`${match.path}/artist-photos`}
                component={ArtistsPhotos}
              />
              <Route exact path={`${match.path}/stats`} component={Stats} />
              <Route
                exact
                path={`${match.path}/artist-settings`}
                component={ArtistSettings}
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

import React, { Fragment } from "react";
import SideMenu from "../layout/account/SideMenu";
import BackArrow from "../layout/BackArrow";
import AccountRoutes from "../Routes/AccountRoutes";
import MainComponent from "../MainComponent";
import { connect } from "react-redux";

const Account = ({ match, account }) => {
  return (
    <Fragment>
      <BackArrow />
      {account.loadingUser ? (
        <h2 className='loading'>Loading...</h2>
      ) : (
        <Fragment>
          <SideMenu />
          <MainComponent container='account-container'>
            <AccountRoutes match={match} />
          </MainComponent>
        </Fragment>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(mapStateToProps)(Account);

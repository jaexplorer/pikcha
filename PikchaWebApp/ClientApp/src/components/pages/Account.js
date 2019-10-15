import React, { Fragment } from "react";
import SideMenu from "../layout/account/SideMenu";
import BackArrow from "../layout/BackArrow";
import AccountRoutes from "../Routes/AccountRoutes";
import MainComponent from "../MainComponent";

const Account = ({ match }) => {
  return (
    <Fragment>
      <SideMenu />
      <BackArrow />
      <MainComponent container='account-container'>
        <AccountRoutes match={match} />
      </MainComponent>
    </Fragment>
  );
};

export default Account;

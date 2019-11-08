import React, { Fragment } from "react";
import AccountRoutes from "../routes/subroutes/AccountRoutes";
import { connect } from "react-redux";
import Loader from "../Loader";

const Account = ({ match, account }) => {
  return (
    <Fragment>
      {account.user ? <AccountRoutes match={match} /> : <Loader />}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(mapStateToProps)(Account);

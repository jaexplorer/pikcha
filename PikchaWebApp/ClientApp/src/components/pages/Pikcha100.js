import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";

const Pikcha100 = ({ auth }) => {
  if (auth.loading) {
    return <h2>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>Pikcha100</div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Pikcha100);

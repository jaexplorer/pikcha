import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";

const About = ({ auth }) => {
  if (auth.loading) {
    return <h2>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>About</div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(About);

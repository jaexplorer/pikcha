import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import PikchaItem from "../layout/pikcha100/PikchaItem";
import Header from "../layout/Header";

const Pikcha100 = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Top 100' title='Pikcha 100' />
      <div className='main-container'>
        <div className='main-content-container'>
          <div id='pikcha100-container'>
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
            <PikchaItem />
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Pikcha100);

import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import CustomiseProduct from "../layout/customise/CustomiseProduct";
import CustomiseSummary from "../layout/customise/CustomiseSummary";
import BackArrowIcon from "../../assets/images/left-arrow-black.png";

const Customise = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }
  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>
        <div className='main-content-container'>
          <div
            className='header-back-arrow'
            onClick={() => window.history.back()}
          >
            <img src={BackArrowIcon} alt='' />
            <span>BACK</span>
          </div>
          <div className='customise-container'>
            <CustomiseProduct />
            <CustomiseSummary />
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Customise);

import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import MainComponent from "../MainComponent";
import CustomiseProduct from "../layout/customise/CustomiseProduct";
import CustomiseSummary from "../layout/customise/CustomiseSummary";
import BackArrow from "../layout/BackArrow";

const Customise = () => {
  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      <MainComponent container='customise-container'>
        <CustomiseProduct />
        <CustomiseSummary />
      </MainComponent>
    </Fragment>
  );
};

export default Customise;

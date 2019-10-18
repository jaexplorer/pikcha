import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import BackArrow from "../layout/BackArrow";
import MainComponent from "../MainComponent";

const NotFound = () => {
  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      <MainComponent container='notFound-container'>
        <h1>Not Found</h1>
        <p>The page you are looking for does not exist</p>
      </MainComponent>
    </Fragment>
  );
};

export default NotFound;

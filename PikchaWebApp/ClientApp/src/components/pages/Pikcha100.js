import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import PikchaItem from "../layout/pikcha100/PikchaItem";
import MainComponent from "../MainComponent";
import Header from "../layout/Header";

const Pikcha100 = () => {
  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Top 100' title='Pikcha 100' />
      <MainComponent container='pikcha100-container'>
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
      </MainComponent>
    </Fragment>
  );
};

export default Pikcha100;

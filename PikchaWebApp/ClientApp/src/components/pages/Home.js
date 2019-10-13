import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import Header from "../layout/Header";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import MainComponent from "../MainComponent";

const Home = () => {
  return (
    <Fragment>
      <SideBar />
      <Header
        subtitle='Todays Popular'
        title='Melbourne Black and White Portraits'
      />
      <MainComponent container='home-container'>
        <MasonryGallery />
      </MainComponent>
    </Fragment>
  );
};

export default Home;

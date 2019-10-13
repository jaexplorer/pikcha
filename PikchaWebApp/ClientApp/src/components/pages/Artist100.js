import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import ArtistItem from "../layout/artist100/ArtistItem";
import Header from "../layout/Header";
import MainComponent from "../MainComponent";

const Artist100 = () => {
  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Top 100' title='Artist 100' />
      <MainComponent container='artist100-container'>
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
        <ArtistItem />
      </MainComponent>
    </Fragment>
  );
};

export default Artist100;

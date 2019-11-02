import React, { useEffect, Fragment } from "react";
import SideBar from "../layout/Sidebar";
import ArtistItem from "../layout/artist100/ArtistItem";
import Header from "../layout/Header";
import MainComponent from "../MainComponent";
import { connect } from "react-redux";
import { getArtist100 } from "../../actions/top100";

const Artist100 = ({ getArtist100, top100 }) => {
  useEffect(() => {
    getArtist100();
  }, []);

  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Top 100' title='Artist 100' />
      {top100.artistloading ? (
        <h2 className='loading'>Loading...</h2>
      ) : (
        <MainComponent container='artist100-container'>
          {top100.artist100.map((artistItem, index) => (
            <ArtistItem artistItem={artistItem} key={index} rank={index + 1} />
          ))}
        </MainComponent>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  top100: state.top100Reducer
});

export default connect(
  mapStateToProps,
  { getArtist100 }
)(Artist100);

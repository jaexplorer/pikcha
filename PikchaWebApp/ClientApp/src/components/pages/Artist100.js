import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import ArtistItem from "../layout/artist100/ArtistItem";
import Header from "../layout/Header";

const Artist100 = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>
        <div className='main-content-container'>
          <Header subtitle='Top 100' title='Artist 100' />
          <div id='artist100-container'>
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
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Artist100);

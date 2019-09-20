import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import Header from "../layout/Header";
import MasonryGallery from "../layout/gallery/MasonryGallery";

const Home = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>
        <div className='main-content-container'>
          <Header subtitle='Todays Popular' title='Top Photos' />
          <MasonryGallery />
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Home);

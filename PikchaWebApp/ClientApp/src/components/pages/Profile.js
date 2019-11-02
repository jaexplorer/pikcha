import React, { Fragment, useEffect, useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import SideBar from "../layout/Sidebar";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import BackArrow from "../layout/BackArrow";
import NotFound from "./NotFound";
import { getArtist } from "../../actions/artist";
import MainComponent from "../MainComponent";
import ProfileSummary from "../layout/profile/ProfileSummary";

const Profile = ({ artist, getArtist }) => {
  const [url, setUrl] = useState(window.location.pathname);

  useEffect(() => {
    const profileId = window.location.pathname.split("/");
    getArtist(profileId[2]);
  }, []);

  useEffect(() => {
    if (window.location.pathname !== url) {
      setUrl(window.location.pathname);
      const profileId = window.location.pathname.split("/");
      getArtist(profileId[2]);
    }
  }, [window.location.pathname]);

  if (artist.error !== null) {
    return <Redirect to='/notFound' component={NotFound} />;
  }

  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      {artist.loading ? (
        <h2 className='loading'>Loading...</h2>
      ) : (
        <Fragment>
          <MainComponent container='profile-container'>
            <ProfileSummary artist={artist} />
            <MasonryGallery />
          </MainComponent>
        </Fragment>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  artist: state.artistReducer
});

export default connect(
  mapStateToProps,
  { getArtist }
)(Profile);

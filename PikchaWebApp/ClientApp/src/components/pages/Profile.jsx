import React, { Fragment, useEffect, useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import {
  getProfile,
  resetProfile,
  getArtistPhotos,
  resetArtistPhotos
} from "../../actions/profile";
import NotFound from "./NotFound";
import Loader from "../common/Loader";
import ProfileSummary from "../layout/profile/ProfileSummary";
import MasonaryGallery from "../layout/gallery/MasonryGallery";

const Profile = ({
  profile,
  getProfile,
  resetProfile,
  getArtistPhotos,
  resetArtistPhotos
}) => {
  const [url, setUrl] = useState(window.location.pathname);

  useEffect(() => {
    const profileId = window.location.pathname.split("/");
    getProfile(profileId[2]);

    return () => {
      resetProfile();
    };
  }, []);

  useEffect(() => {
    if (window.location.pathname !== url) {
      setUrl(window.location.pathname);
      const profileId = window.location.pathname.split("/");
      getProfile(profileId[2]);
    }
  }, [window.location.pathname]);

  if (profile.profileError !== null) {
    return <Redirect to='/notFound' component={NotFound} />;
  }

  return (
    <Fragment>
      {!profile.profileLoading ? (
        <Fragment>
          <ProfileSummary profile={profile.artist} />
          <MasonaryGallery
            gallery={profile}
            getPhotos={(count, start) =>
              getArtistPhotos(profile.artist.id, count, start)
            }
            resetGallery={() => resetArtistPhotos()}
          />
        </Fragment>
      ) : (
        <Loader />
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  profile: state.profileReducer
});

export default connect(
  mapStateToProps,
  { getProfile, resetProfile, getArtistPhotos, resetArtistPhotos }
)(Profile);

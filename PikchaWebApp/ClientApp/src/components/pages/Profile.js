import React, { Fragment, useEffect, useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { getProfile, resetProfile } from "../../actions/profile";
import NotFound from "./NotFound";
import Loader from "../Loader";
import ProfileSummary from "../layout/profile/ProfileSummary";
import MasonaryGallery from "../layout/gallery/MasonryGallery";

const Profile = ({ profile, getProfile, resetProfile }) => {
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
      {profile.artist ? (
        <ProfileSummary profile={profile.artist} />
      ) : (
        <Loader />
      )}
      <MasonaryGallery />
    </Fragment>
  );
};

const mapStateToProps = state => ({
  profile: state.profileReducer
});

export default connect(
  mapStateToProps,
  { getProfile, resetProfile }
)(Profile);

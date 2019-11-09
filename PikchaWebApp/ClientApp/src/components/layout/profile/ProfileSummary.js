import React, { Fragment } from "react";
import { connect } from "react-redux";
import FacebookIcon from "../../../assets/images/facebook-black.png";
import InstagramIcon from "../../../assets/images/instagram-black.png";
import TwitterIcon from "../../../assets/images/twitter-black.png";
import CameraIcon from "../../../assets/images/camera-white.png";

import {
  createUploadImageModal,
  createPromoteModal,
  createDPModal,
  createCoverModal
} from "../../../actions/modal";
import { followArtist, unfollowArtist } from "../../../actions/account";

const ProfileSummary = ({
  profile,
  account,
  createUploadImageModal,
  createPromoteModal,
  createDPModal,
  createCoverModal,
  followArtist,
  unfollowArtist
}) => {
  const {
    bio,
    links,
    performance,
    totSold,
    avgPrice,
    following,
    followers,
    location,
    fName,
    lName,
    aggrImViews,
    avatar,
    cover,
    sign
  } = profile;

  const followAction = () => {
    if (!account.user) {
      return "";
    }
    if (account.user.id === profile.id) {
      if (account.user.roles.includes("Artist")) {
        return (
          <div
            onClick={() => createUploadImageModal()}
            className='artist-action'
          >
            Upload
          </div>
        );
      } else {
        return (
          <div onClick={() => createPromoteModal()} className='artist-action'>
            Become an Artist
          </div>
        );
      }
    } else if (
      account.user.following.some(f => {
        return f.id === profile.id;
      })
    ) {
      return (
        <div
          onClick={() => unfollowArtist(account.user.id, profile.id)}
          className='artist-action'
        >
          Followed
        </div>
      );
    } else {
      return (
        <div
          onClick={() => followArtist(account.user.id, profile.id)}
          className='artist-action'
        >
          Follow
        </div>
      );
    }
  };

  return (
    <div className='profile-summary-container'>
      <div className='cover-photo'>
        <img src={cover} alt='' />
        {account.user && account.user.id === profile.id && (
          <div onClick={() => createCoverModal()} className='edit'>
            <img src={CameraIcon} alt='' />
          </div>
        )}
      </div>
      <div className='summary-content'>
        <div className='first-container'>
          <div className='artist-picture'>
            {account.user && account.user.id === profile.id ? (
              <Fragment>
                <img src={account.user.avatar} alt='' />
                <div onClick={() => createDPModal()} className='edit'>
                  <img src={CameraIcon} alt='' />
                </div>
              </Fragment>
            ) : (
              <img src={avatar} alt='' />
            )}
          </div>
          <div className='artist-info'>
            <div className='artist-details'>
              <div className='artist-name'>
                {fName} {lName}
              </div>
              <div className='artist-location'>{location}</div>
              <div className='socials'>
                <img src={FacebookIcon} alt='' />
                <img src={InstagramIcon} alt='' />
                <img src={TwitterIcon} alt='' />
              </div>
            </div>
            {followAction()}
          </div>
        </div>
        <div className='second-container'>
          <div className='artist-description'>
            <div className='about-me-container'>
              <div className='about-title'>About me</div>
              <div className='about-text'>{bio}</div>
            </div>
            <div className='signature'>
              <img src={sign} alt='' />
            </div>
          </div>
          <div className='artist-stats'>
            <div className='artist-views'>{aggrImViews} Views</div>
            <div className='artist-followers'>{followers.length} Followers</div>
            <div className='photos-sold'>{totSold} Photos Sold</div>
            <div className='average-price'>${avgPrice} Average Price</div>
          </div>
          {/* <div className='following'>
            <div className='following-title'>Following</div>
            <div className='following-container'>
              <div className='following-wrapper'>
                {following.map(followed => (
                  <div className='followed'>
                    <img src={followed.avatar} alt='' />
                  </div>
                ))}
              </div>
            </div>
          </div> */}
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  {
    createUploadImageModal,
    createDPModal,
    followArtist,
    unfollowArtist,
    createPromoteModal,
    createCoverModal
  }
)(ProfileSummary);

import React, { Fragment } from "react";
import { connect } from "react-redux";
import ProfilePic from "../../../assets/images/placeholder.png";
import {
  createUploadImageModal,
  createPromoteModal
} from "../../../actions/modal";
import { followArtist, unfollowArtist } from "../../../actions/account";

const ProfileSummary = ({
  profile,
  account,
  createUploadImageModal,
  createPromoteModal,
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
    avatar
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
      <div className='first-container'>
        <div className='artist-picture'>
          <img src={avatar} alt='' />
        </div>
        <div className='artist-info'>
          <div className='artist-details'>
            <div className='artist-name'>
              {fName} {lName}
            </div>
            <div className='artist-location'>{location}</div>
          </div>
          {followAction()}
        </div>
      </div>
      <div className='second-container'>
        <div className='artist-description'>
          <div className='about-me'>About me</div>
          {bio}
        </div>
        <div className='artist-stats'>
          <div className='artist-views'>{aggrImViews} Views</div>
          <div className='artist-followers'>{followers.length} Followers</div>
          <div className='photos-sold'>{totSold} Photos Sold</div>
          <div className='average-price'>${avgPrice} Average Price</div>
        </div>
        <div className='following'>
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
  { createUploadImageModal, followArtist, unfollowArtist, createPromoteModal }
)(ProfileSummary);

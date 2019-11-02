import React, { Fragment } from "react";
import { connect } from "react-redux";
import ProfilePic from "../../../assets/images/profilePic.png";
import { createModal } from "../../../actions/modal";
import { followArtist, unfollowArtist } from "../../../actions/account";

const ProfileSummary = ({
  artist,
  account,
  createModal,
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
    email,
    phone,
    avatar
  } = artist.artist;

  const determineAction = () => {
    if (account.loadingUser === false && artist.artist.id === account.user.id) {
      if (account.user.roles.includes("Artist")) {
        return (
          <div
            onClick={() => createModal("UploadModal")}
            className='artist-action'
          >
            Upload
          </div>
        );
      } else {
        return (
          <div
            onClick={() => createModal("RoleChangeModal")}
            className='artist-action'
          >
            Upload
          </div>
        );
      }
    } else if (
      account.loadingUser === false &&
      account.user.following.some(f => {
        return f.id === artist.artist.id;
      })
    ) {
      return <div className='artist-action'>Followed</div>;
    } else {
      return (
        <div
          onClick={() => followArtist(account.user.id, artist.artist.id)}
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
        <div className='artist-details'>
          <div className='artist-name'>
            {fName} {lName}
          </div>
          <div className='artist-location'>{location}</div>
        </div>
        {determineAction()}

        <div className='artist-description'>{bio}</div>
      </div>
      <div className='second-container'>
        <div className='artist-description'>{bio}</div>
        <div className='artist-stats'>
          <div className='artist-views'>{aggrImViews} Views</div>
          <div className='artist-followers'>{followers.length} Followers</div>
          <div className='photos-sold'>{totSold} Photos Sold</div>
          <div className='average-price'>${avgPrice} Average Price</div>
        </div>
        {following.length > 0 && (
          <div className='following'>
            <div className='following-title'>Following</div>
            <div className='following-container'>
              <div className='following-wrapper'>
                {following.map(followed => (
                  <div className='followed'>
                    <img src={ProfilePic} alt='' />
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { createModal, followArtist, unfollowArtist }
)(ProfileSummary);

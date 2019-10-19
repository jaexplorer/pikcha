// TODO: Integrate Stats, photo description(short), socials, expand image and price

import React, { useState, Fragment } from "react";
import { Link } from "react-router-dom";
import BackArrow from "../../../assets/images/backArrow-white.png";
import InfoButton from "../../../assets/images/info-white.png";
import InstagramIcon from "../../../assets/images/instagram-white.png";
import TwitterIcon from "../../../assets/images/twitter-white.png";
import DeleteIcon from "../../../assets/images/delete-black.png";
import ArtistPlaceholder from "../../../assets/images/placeholder.png";

import { connect } from "react-redux";
import { deselectPhoto } from "../../../actions/gallery";

const ItemInfo = ({ deselectPhoto, photo }) => {
  const [popup, setPopup] = useState(false);

  return (
    <Fragment>
      <div className='itemInfo-back-arrow'>
        <img onClick={deselectPhoto} src={BackArrow} alt='' />
      </div>
      <div className='itemInfo-info-button'>
        <img
          onClick={() => {
            setPopup(true);
          }}
          src={InfoButton}
          alt=''
        />
      </div>
      {popup && (
        <div className='itemInfo-popup'>
          <div className='popup-artist'>
            <Link to={`/profile/${photo.artistId}`}>
              <img src={photo.artistAvatarfilename} alt='' />
            </Link>
          </div>
          <div className='popup-artist-name'>
            {photo.artistFirstname} {photo.artistLastname}
          </div>
          <div className='popup-content-container'>
            <div className='popup-photo-name'>{photo.title}</div>
            <div className='popup-photo-stats'>
              <span>
                {photo.performance >= 0 ? "Up " : "Down "}
                {photo.performance}%
              </span>
              <span>{photo.totalPhotosSold} Sold</span>
            </div>
            <div className='popup-photo-description'>
              Lorem ipsum dolor sit, amet consectetur adipisicing elit.
              Doloribus alias vero sequi at autem tempora magni quam natus
              incidunt ipsa, odit error, nulla totam facere.
            </div>
            <div className='popup-photo-action'>
              <div className='photo-purchase'>
                <Link to={`/product/${photo.pikchaImageId}`}>View</Link>
              </div>
              <span>${photo.averagePrice}</span>
            </div>
          </div>
          <div className='popup-delete'>
            <img
              onClick={() => {
                setPopup(false);
              }}
              src={DeleteIcon}
              alt=''
            />
          </div>
        </div>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { deselectPhoto }
)(ItemInfo);

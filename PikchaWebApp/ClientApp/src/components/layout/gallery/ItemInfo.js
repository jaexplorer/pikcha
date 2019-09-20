import React, { useState, Fragment } from "react";
import BackArrow from "../../../assets/images/backArrow-white.png";
import InfoButton from "../../../assets/images/info-white.png";
import InstagramIcon from "../../../assets/images/instagram-white.png";
import TwitterIcon from "../../../assets/images/twitter-white.png";
import DeleteIcon from "../../../assets/images/delete-black.png";
import ArtistPlaceholder from "../../../assets/images/placeholder.png";

import { connect } from "react-redux";
import { deselectPhoto } from "../../../actions/gallery";

const ItemInfo = ({ deselectPhoto }) => {
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
            <img src={ArtistPlaceholder} alt='' />
          </div>
          <div className='popup-artist-name'>Adon Higgins</div>
          <div className='popup-socials'>
            <img src={TwitterIcon} alt='' />
            <img src={InstagramIcon} alt='' />
          </div>
          <div className='popup-content-container'>
            <div className='popup-photo-name'>Staring at the light</div>
            <div className='popup-photo-stats'>
              <span>Up 3.42%</span>
              <span>87 Sold</span>
            </div>
            <div className='popup-photo-description'>
              Lorem ipsum dolor sit, amet consectetur adipisicing elit.
              Doloribus alias vero sequi at autem tempora magni quam natus
              incidunt ipsa, odit error, nulla totam facere.
            </div>
            <div className='popup-photo-action'>
              <div className='photo-purchase'>Purchase</div>
              <span>$143</span>
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

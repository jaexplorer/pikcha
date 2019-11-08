import React, { useState, Fragment, useEffect } from "react";
import { Link } from "react-router-dom";
import BackArrow from "../../../assets/images/backArrow-white.png";
import InfoButton from "../../../assets/images/info-white.png";
import DeleteIcon from "../../../assets/images/delete-black.png";
import { addView } from "../../../actions/gallery";
import { connect } from "react-redux";

const ItemInfo = ({ photo, onChange, addView }) => {
  const [popup, setPopup] = useState(false);

  // Decontructing photo
  const {
    caption,
    performance,
    totSold,
    avgPrice,
    productIds,
    id: pictureId,
    title,
    location,
    watermark,
    views,
    artist
  } = photo;

  useEffect(() => {
    addView(pictureId);
  }, []);

  // Decontructing artist
  const { id: artistId, fName, lName, email, phone, avatar } = artist;

  return (
    <Fragment>
      <div className='itemInfo-back-arrow'>
        <img onClick={() => onChange(false)} src={BackArrow} alt='' />
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
      <div className={`itemInfo-popup ${popup ? "active" : "inactive"}`}>
        <div className='popup-artist'>
          <Link to={`/profile/${artistId}`}>
            <img src={avatar} alt='' />
          </Link>
        </div>
        <div className='popup-artist-name'>
          {fName} {lName}
        </div>
        <div className='popup-content-container'>
          <div className='popup-photo-name'>
            {title.slice(0, 30)}
            {title.length > 30 ? "..." : ""}
          </div>
          <div className='popup-photo-stats'>
            <span>
              {performance >= 0 ? "Up " : "Down "}
              {performance}%
            </span>
            <span>{totSold} Sold</span>
          </div>
          <div className='popup-photo-description'>
            {caption.slice(0, 150)}
            {caption.length > 150 ? "..." : ""}
          </div>
          <div className='popup-photo-action'>
            <div className='photo-purchase'>
              <Link to={`/product/${pictureId}`}>View</Link>
            </div>
            <span>${avgPrice}</span>
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
    </Fragment>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { addView }
)(ItemInfo);

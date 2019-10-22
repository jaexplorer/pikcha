import React from "react";
import UserPlaceholder from "../../../assets/images/user-04.png";

const PikchaItem = ({ pikchaItem, rank }) => {
  // Destructuring pikchaItem
  const {
    caption,
    performance,
    totSold,
    avgPrice,
    productIds,
    id,
    title,
    location,
    watermark,
    views,
    artist
  } = pikchaItem;

  return (
    <div className='pikcha-container'>
      <div className='pikcha-rank'>{rank}</div>
      <div className='pikcha-photo'>
        <img src={watermark} alt='' />
      </div>
      <div className='pikcha-photo-information'>
        <div className='pikcha-photo-name'>{title}</div>
        <div className='pikcha-photo-views'>{views} Views</div>
        <div className='pikcha-photo-turnover'>{totSold} Total Sold</div>
        <div className='pikcha-photo-avg-price'>{avgPrice} Average Price</div>
        <div className='pikcha-photo-location'>{location}</div>
      </div>
      <div className='pikcha-photo-graph'></div>
      <div className='divider'></div>
      <div className='artist-picture'>
        <img src={artist.avatar} alt='' />
      </div>
      <div className='artist-information'>
        <div className='artist-name'>
          {artist.fname} {artist.lName}
        </div>
        <div className='artist-location'>Melbourne, Australia</div>
      </div>
    </div>
  );
};

export default PikchaItem;

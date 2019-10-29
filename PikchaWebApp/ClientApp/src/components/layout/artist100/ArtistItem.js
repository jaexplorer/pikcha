import React from "react";
import UserPlaceholder from "../../../assets/images/user-04.png";

const ArtistItem = ({ artistItem, rank }) => {
  // Destructuring artistItem
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
  } = artistItem;

  return (
    <div className='artist-container'>
      <div className='artist-rank'>{rank}</div>
      <div className='artist-picture'>
        <img src={artist.avatar} alt='' />
      </div>
      <div className='artist-information'>
        <div className='artist-name'>
          {artist.fName} {artist.lName}
        </div>
        <div className='artist-location'>Melbourne, Australia</div>
        <div className='artist-stats'>
          <div className='artist-photos-sold'>{totSold} Photos Sold</div>
          <div className='artist-photo-views'>240k Photo Views</div>
        </div>
      </div>

      <div className='divider'></div>
      <div className='top-photo-information'>
        <div className='top-photo-name'>{title}</div>
        <div className='top-photo-views'>1.2m Views</div>
        <div className='top-photo-avg-price'>${avgPrice} Average Price</div>
        <div className='top-photo-location'>{location}</div>
      </div>
      <div className='artist-top-photo'>
        <img src={watermark} alt='' />
      </div>
    </div>
  );
};

export default ArtistItem;

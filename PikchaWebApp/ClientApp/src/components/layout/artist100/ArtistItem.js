import React from "react";
import UserPlaceholder from "../../../assets/images/user-04.png";

const ArtistItem = () => {
  return (
    <div className='artist-container'>
      <div className='artist-rank'>1</div>
      <div className='artist-picture'>
        <img src={UserPlaceholder} alt='' />
      </div>
      <div className='artist-information'>
        <div className='artist-name'>Andrew Davis</div>
        <div className='artist-location'>Melbourne, Australia</div>
      </div>
      <div className='artist-stats'>
        <div className='artist-photos-sold'>15,780 Photos Sold</div>
        <div className='artist-photo-views'>240k Photo Views</div>
      </div>
      <div className='divider'></div>
      <div className='top-photo-information'>
        <div className='top-photo-name'>Wild Charm</div>
        <div className='top-photo-views'>1.2m Views</div>
        <div className='top-photo-avg-price'>$320.30 Average Price</div>
        <div className='top-photo-location'>Queensland, Australia</div>
      </div>
      <div className='artist-top-photo'></div>
    </div>
  );
};

export default ArtistItem;

import React from "react";
import UserPlaceholder from "../../../assets/images/user-04.png";

const PikchaItem = () => {
  return (
    <div className='pikcha-container'>
      <div className='pikcha-rank'>1</div>
      <div className='pikcha-photo'></div>
      <div className='pikcha-photo-information'>
        <div className='pikcha-photo-name'>Wild Charm</div>
        <div className='pikcha-photo-views'>1.2m Views</div>
        <div className='pikcha-photo-turnover'>750 Turnover</div>
        <div className='pikcha-photo-avg-price'>$320.30 Average Price</div>
        <div className='pikcha-photo-location'>Queensland, Australia</div>
      </div>
      <div className='pikcha-photo-graph'></div>
      <div className='divider'></div>
      <div className='artist-picture'>
        <img src={UserPlaceholder} alt='' />
      </div>
      <div className='artist-information'>
        <div className='artist-name'>Andrew Davis</div>
        <div className='artist-location'>Melbourne, Australia</div>
      </div>
    </div>
  );
};

export default PikchaItem;

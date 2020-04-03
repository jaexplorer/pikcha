import React from "react";

const PikchaRow = ({ row, rank }) => {
  const { thumbnail, title, views, totSold, avgPrice, location, artist } = row;
  return (
    <div className='pikcha-row'>
      <div className='pikcha-rank'>{rank}</div>
      <div className='pikcha-photo'>
        <img src={thumbnail} alt='' />
      </div>
      <div className='pikcha-info'>
        <div className='photo-name'>
          {title.slice(0, 30)}
          {title.length > 30 ? "..." : ""}
        </div>
        <div className='photo-info'>
          <div className='photo-views'>
            {views} View{views !== "1" && "s"}
          </div>
          <div className='photo-turnover'>{totSold} Total Sold</div>
          <div className='photo-avg-price'>{avgPrice} Average Price</div>
          <div className='photo-location'>{location}</div>
        </div>
        <div className='artist-info'>
          <div className='artist-picture'>
            <img src={artist.avatar} alt='' />
          </div>
          <div className='artist-details'>
            <div className='artist-name'>
              {artist.fName} {artist.lName}
            </div>
            <div className='artist-location'>{artist.location}</div>
          </div>
        </div>
      </div>
      <div className='photo-statistics'></div>
    </div>
  );
};

export default PikchaRow;

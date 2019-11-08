import React from "react";

const ArtistRow = ({ row, rank }) => {
  const { thumbnail, title, views, totSold, avgPrice, location, artist } = row;

  return (
    <div className='artist-row'>
      <div className='artist-rank'>{rank}</div>
      <div className='artist-picture'>
        <img src={artist.avatar} alt='' />
      </div>
      <div className='artist-information'>
        <div className='artist-name'>
          {artist.fName} {artist.lName}
        </div>
        <div className='artist-location'>{artist.location}</div>
        <div className='artist-stats'>
          <div className='artist-photos-sold'>{totSold} Photos Sold</div>
          <div className='artist-photo-views'>
            {artist.aggrImViews} Photo View{artist.aggrImViews !== "1" && "s"}
          </div>
        </div>
      </div>

      <div className='top-photo-information'>
        <div className='top-photo-name'>
          {title.slice(0, 30)}
          {title.length > 30 ? "..." : ""}
        </div>
        <div className='top-photo-info'>
          <div className='top-photo-views'>
            {views} View{views !== "1" && "s"}
          </div>
          <div className='top-photo-avg-price'>{avgPrice} Average Price</div>
          <div className='top-photo-location'>{location}</div>
        </div>
      </div>
      <div className='artist-top-photo'>
        <img src={thumbnail} alt='' />
      </div>
    </div>
  );
};

export default ArtistRow;

import React from "react";
import Placeholder from "../../../assets/images/placeholder.png";

const MainSeller = () => {
  return (
    <div className='mainSeller-container'>
      <div className='mainSeller-heading'>Purchase from the Artist</div>
      <div className='mainSeller-content'>
        <div className='mainSeller-seller'>
          <div className='mainSeller-artist'>
            <img src={Placeholder} alt='' />
            <div className='mainSeller-info'>
              <div className='mainSeller-artist-name'>John Topica</div>
              <div className='mainSeller-artist-location'>
                Melbourne, Australia
              </div>
            </div>
          </div>
        </div>
        <div className='break'></div>
        <div className='mainSeller-price'>$420</div>
        <div className='break'></div>
        <div className='mainSeller-action'>Customise</div>
      </div>
    </div>
  );
};

export default MainSeller;

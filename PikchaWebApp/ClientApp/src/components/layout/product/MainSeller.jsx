import React from "react";
import { Link } from "react-router-dom";
import Placeholder from "../../../assets/images/placeholder.png";

const MainSeller = ({ photo }) => {
  return (
    <div className='mainSeller-container'>
      <div className='mainSeller-content'>
        <div className='mainSeller-seller'>
          <div className='mainSeller-artist'>
            <img src={Placeholder} alt='' />
            <div className='mainSeller-info'>
              <div className='mainSeller-artist-name'>John Smith</div>
              <div className='mainSeller-artist-location'>
                Melbourne, Australia
              </div>
            </div>
          </div>
        </div>
        <div className='break'></div>
        <div className='mainSeller-price'>$420</div>
        <div className='break'></div>
        <div className='mainSeller-action'>
          <Link to={{ pathname: "/customise", state: { photo } }}>
            Customise
          </Link>
        </div>
      </div>
    </div>
  );
};

export default MainSeller;

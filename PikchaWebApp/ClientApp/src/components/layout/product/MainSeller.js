import React from "react";
import { Link } from "react-router-dom";
import Placeholder from "../../../assets/images/placeholder.png";

const MainSeller = ({ auth }) => {
  return (
    <div className='mainSeller-container'>
      <div className='mainSeller-heading'>
        {auth.isAuthenticated ? "Purchase" : "Available"} from the Artist
      </div>
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
        {auth.isAuthenticated && (
          <Link className='mainSeller-action' to='/Customise/1'>
            Customise
          </Link>
        )}
      </div>
    </div>
  );
};

export default MainSeller;

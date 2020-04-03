import React from "react";
import FullscreenIcon from "../../../assets/images/fullscreen-white.png";

const CustomiseSummary = ({ photo }) => {
  const onSubmit = () => {};

  return (
    <div className='customiseSummary-container'>
      <div className='customiseSummary-content'>
        <div className='customiseSummary-title'>Summary</div>
        <div className='customiseSummary-summary'>
          <div className='summary-photo-details'>
            <div className='photo-name'>Wild Charm</div>
            <div className='photo-number'>#11</div>
          </div>

          <div className='summary-subtitle'>Size</div>
          <div className='summary-subtext'>Big</div>

          <div className='summary-subtitle'>Material</div>
          <div className='summary-subtext'>Wood</div>

          <div className='summary-subtitle'>Frame</div>
          <div className='summary-subtext'>2 Inches</div>

          <div className='summary-subtitle'>Border</div>
          <div className='summary-subtext'>None</div>

          <div className='summary-subtitle'>Finish</div>
          <div className='summary-subtext'>None</div>
        </div>
        <div className='customiseSummary-addToCart'>
          <div className='addToCart-total'>$430</div>
          <div onClick={onSubmit} className='addToCart-button'>
            Add To Cart
          </div>
        </div>
      </div>
      <div className='customiseSummary-photo'>
        <img src={photo} alt='' />
        <div className='fullscreen-icon'>
          <img src={FullscreenIcon} alt='' />
        </div>
      </div>
    </div>
  );
};

export default CustomiseSummary;

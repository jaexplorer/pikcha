import React from "react";
import BackArrowIcon from "../../assets/images/left-arrow-black.png";

const BackArrow = () => {
  return (
    <div className='header-back-arrow' onClick={() => window.history.back()}>
      <img src={BackArrowIcon} alt='' />
      <span>BACK</span>
    </div>
  );
};

export default BackArrow;

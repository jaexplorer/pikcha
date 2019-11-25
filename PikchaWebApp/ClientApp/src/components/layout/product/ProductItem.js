import React from "react";
import Placeholder from "../../../assets/images/placeholder.png";
import FullscreenIcon from "../../../assets/images/fullscreen-white.png";

const ProductItem = () => {
  return (
    <div className='productItem-container'>
      <div className='productItem-content'>
        <div className='productItem-heading'>
          <div className='product-title'>Sunny Mountains</div>
          <div className='product-number'>#11</div>
        </div>
        <div className='productItem-stats'>
          <div>3m Views</div>
          <div>760 Turnover</div>
          <div>$420.30 Average Price</div>
        </div>
        <div className='productItem-description'>
          Lorem ipsum, dolor sit amet consectetur adipisicing elit. Reiciendis
          nihil cum autem, corrupti dolor labore sint culpa! Quae deleniti dolor
          sed nihil. Soluta nobis eligendi aut sint deleniti, a adipisci
          exercitationem minus suscipit cupiditate quo.
        </div>
        <div className='productItem-origins'>
          <div className='productItem-artist-pic'>
            <img src={Placeholder} alt='' />
          </div>
          <div className='product-artist-info'>
            <div className='product-subtitle'>Artist</div>
            <div className='product-subtext'>John Smith</div>
          </div>
          <div className='product-location'>
            <div className='product-subtitle'>Location</div>
            <div className='product-subtext'>Melbourne Australia</div>
          </div>
        </div>
        <div className='productItem-about-artist'>
          <div className='product-subtitle'>About the Artist</div>
          <div className='productItem-artist-description'>
            Lorem ipsum, dolor sit amet consectetur adipisicing elit.
            Reprehenderit vero obcaecati repellendus. Illo fugit iusto
            excepturi. Magni nulla ut at consequatur consequuntur necessitatibus
            sapiente provident.
          </div>
        </div>
      </div>

      <div className='productItem-photo'>
        <img src='' alt='' />
        <div className='fullscreen-icon'>
          <img src={FullscreenIcon} alt='' />
        </div>
      </div>
    </div>
  );
};

export default ProductItem;

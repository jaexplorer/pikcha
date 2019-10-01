import React from "react";
import FullscreenIcon from "../../../assets/images/fullscreen-white.png";
import { connect } from "react-redux";
import { clearCustomisations } from "../../../actions/product";
import { addToCart } from "../../../actions/cart";

const CustomiseSummary = ({ product, clearCustomisations, addToCart }) => {
  const { size, material, frame, border, finish } = product;

  const onSubmit = () => {
    const productToAdd = {
      productName: "Wild Charm",
      productNumber: "#11",
      size,
      material,
      frame,
      border,
      finish,
      productPrice: 430
    };
    addToCart(productToAdd);
    clearCustomisations();
  };

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
          <div className='summary-subtext'>{size}</div>

          <div className='summary-subtitle'>Material</div>
          <div className='summary-subtext'>{material}</div>

          <div className='summary-subtitle'>Frame</div>
          <div className='summary-subtext'>{frame}</div>

          <div className='summary-subtitle'>Border</div>
          <div className='summary-subtext'>{border}</div>

          <div className='summary-subtitle'>Finish</div>
          <div className='summary-subtext'>{finish}</div>
        </div>
        <div className='customiseSummary-addToCart'>
          <div className='addToCart-total'>$430</div>
          <div onClick={onSubmit} className='addToCart-button'>
            Add To Cart
          </div>
        </div>
      </div>
      <div className='customiseSummary-photo'>
        <div className='fullscreen-icon'>
          <img src={FullscreenIcon} alt='' />
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  product: state.productReducer
});

export default connect(
  mapStateToProps,
  { clearCustomisations, addToCart }
)(CustomiseSummary);

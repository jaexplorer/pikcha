import React, { useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeCartDropdown } from "../../../actions/nav";
import { removeFromCart } from "../../../actions/cart";

import RemoveIcon from "../../../assets/images/remove-white.png";

const CartDropdown = ({ cart, removeCartDropdown, removeFromCart }) => {
  // Detect Clicks outside of container
  const dropdownContainer = useRef(null);
  useEffect(() => {
    document.addEventListener("click", e => {
      if (
        dropdownContainer.current &&
        !dropdownContainer.current.contains(e.target)
      ) {
        removeCartDropdown();
      }
    });
  });

  return (
    <div id='cart-dropdown' ref={dropdownContainer}>
      {cart.products.map((product, index) => (
        <div key={index} className='cart-dropdown-item'>
          <div className='cart-product-name'>
            {product.productName} {product.productNumber}
          </div>
          <div className='cart-product-price'>${product.productPrice}</div>
          <div className='cart-product-remove'>
            <img
              onClick={() => {
                removeFromCart(product);
              }}
              src={RemoveIcon}
              alt=''
            />
          </div>
        </div>
      ))}
      {cart.products.length !== 0 ? (
        <div className='cart-dropdown-item'>
          <div className='cart-total'>${cart.total}</div>
          <div className='cart-checkout'>Checkout</div>
        </div>
      ) : (
        <div className='cart-dropdown-item'>
          <div>Your cart is empty!</div>
        </div>
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  cart: state.cartReducer
});

export default connect(
  mapStateToProps,
  { removeCartDropdown, removeFromCart }
)(CartDropdown);

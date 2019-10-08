import React from "react";
import SellerItem from "./SellerItem";

const SellerList = () => {
  return (
    <div className='sellerList-container'>
      <div className='sellerList-heading'>6 People also Selling</div>
      <div className='sellerList-content'>
        <SellerItem />
        <SellerItem />
        <SellerItem />
        <SellerItem />
        <SellerItem />
        <SellerItem />
      </div>
    </div>
  );
};

export default SellerList;

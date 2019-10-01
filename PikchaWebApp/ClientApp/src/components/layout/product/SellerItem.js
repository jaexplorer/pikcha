import React from "react";

const SellerItem = () => {
  return (
    <div className='sellerItem-container'>
      <div className='sellerItem-print-number'>#14</div>
      <div className='break'></div>
      <div className='sellerItem-print-dimensions'>1.6m by 1.2m</div>
      <div className='break'></div>
      <div className='sellerItem-print-materials'>
        <div>Wooden Frame</div>
        <div>Premium Matte Fabric</div>
        <div>Acrylic</div>
      </div>
      <div className='break'></div>
      <div className='sellerItem-price'>$410</div>
      <div className='break'></div>
      <div className='sellerItem-action'>View</div>
    </div>
  );
};

export default SellerItem;

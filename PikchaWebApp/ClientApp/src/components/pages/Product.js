import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import ProductItem from "../layout/product/ProductItem";
import MainSeller from "../layout/product/MainSeller";
import SellerList from "../layout/product/SellerList";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import BackArrow from "../layout/BackArrow";

const Product = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }
  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      <div className='main-container'>
        <div className='main-content-container'>
          <div className='product-container'>
            <ProductItem />
            <MainSeller auth={auth} />
            <SellerList />
            <div className='more-content-heading'>More from this Artist</div>
            <MasonryGallery />
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Product);

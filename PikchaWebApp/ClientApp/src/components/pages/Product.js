// TODO: Fix Error handling, Get a list of sellers, Check main seller

import React, { Fragment, useEffect } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import SideBar from "../layout/Sidebar";
import ProductItem from "../layout/product/ProductItem";
import MainSeller from "../layout/product/MainSeller";
import SellerList from "../layout/product/SellerList";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import BackArrow from "../layout/BackArrow";
import { getProduct } from "../../actions/product";
import NotFound from "./NotFound";

const Product = ({ auth, product, getProduct }) => {
  useEffect(() => {
    const productId = window.location.pathname.split("/");
    getProduct(productId[2]);
    // eslint-disable-next-line
  }, []);

  if (
    product.loading === false &&
    product.product.status === "Error Occurred"
  ) {
    return <Redirect to='/notFound' component={NotFound} />;
  }

  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      <div className='main-container'>
        <div className='main-content-container'>
          {auth.loading || product.loading ? (
            <h2 className='loading'>Loading...</h2>
          ) : (
            <div className='product-container'>
              <ProductItem />
              <MainSeller auth={auth} />
              <SellerList />
              <div className='more-content-heading'>More from this Artist</div>
              <MasonryGallery />
            </div>
          )}
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer,
  product: state.productReducer
});

export default connect(
  mapStateToProps,
  { getProduct }
)(Product);

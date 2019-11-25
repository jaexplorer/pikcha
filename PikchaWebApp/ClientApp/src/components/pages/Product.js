import React, { Fragment, useEffect } from "react";
import { Redirect } from "react-router-dom";
import ProductItem from "../layout/product/ProductItem";
import MainSeller from "../layout/product/MainSeller";
import SellerList from "../layout/product/SellerList";
import { connect } from "react-redux";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import { getPhotos, resetGallery } from "../../actions/gallery";
import NotFound from "./NotFound";

const Product = ({ gallery, getPhotos, resetGallery }) => {
  return (
    <Fragment>
      <ProductItem />
      <MainSeller />
      <SellerList />
      <MasonryGallery
        gallery={gallery}
        getPhotos={(count, start) => getPhotos(count, start)}
        resetGallery={() => resetGallery()}
      />
    </Fragment>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(mapStateToProps, { getPhotos, resetGallery })(Product);

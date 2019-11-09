import React from "react";
import { connect } from "react-redux";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import { getPhotos, resetGallery } from "../../actions/gallery";

const Home = ({ gallery, getPhotos, resetGallery }) => {
  return (
    <MasonryGallery
      gallery={gallery}
      getPhotos={(count, start) => getPhotos(count, start)}
      resetGallery={() => resetGallery()}
    />
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { getPhotos, resetGallery }
)(Home);

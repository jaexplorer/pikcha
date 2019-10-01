import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import { getPhotos } from "../../../actions/gallery";
import MasonryColumn from "./MasonryColumn";

const MasonryGallery = ({ getPhotos, gallery }) => {
  const [columns, setColumns] = useState(4);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth >= 1500) {
        setColumns(4);
      } else if (window.innerWidth >= 1200) {
        setColumns(3);
      } else {
        setColumns(2);
      }
    };
    handleResize();
    getPhotos();
    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  const separate = () => {
    var a = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
    var res = [...Array(columns).keys()].map(c =>
      gallery.photos.data.filter((_, i) => i % columns === c)
    );
    return [...Array(columns)].map((column, index) => (
      <MasonryColumn key={index + 1} photos={res[index]} />
    ));
  };

  return (
    <div className='masonry'>
      {gallery.photos !== null && separate()}
      {/* {[...Array(columns)].map((column, index) => (
        <MasonryColumn key={index + 1} />
      ))} */}
    </div>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { getPhotos }
)(MasonryGallery);

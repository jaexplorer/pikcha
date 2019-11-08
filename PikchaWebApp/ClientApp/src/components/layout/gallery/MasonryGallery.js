import React, { Fragment, useState, useEffect } from "react";
import { connect } from "react-redux";
import { getPhotos, resetGallery } from "../../../actions/gallery";
import MasonryColumn from "./MasonryColumn";
import InfiniteScroll from "react-infinite-scroll-component";
import Loader from "../../Loader";

const MasonryGallery = ({ getPhotos, gallery, resetGallery }) => {
  const [columns, setColumns] = useState(4);

  useEffect(() => {
    getPhotos(gallery.count, gallery.start);
    return () => resetGallery();
  }, []);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth >= 999) {
        setColumns(4);
      } else if (window.innerWidth >= 699) {
        setColumns(3);
      } else {
        setColumns(2);
      }
    };

    handleResize();
    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
    // eslint-disable-next-line
  }, []);

  const separatePhotos = () => {
    var res = [...Array(columns).keys()].map(c =>
      gallery.photos.filter((_, i) => i % columns === c)
    );
    return [...Array(columns)].map((column, index) => (
      <MasonryColumn key={index + 1} photos={res[index]} />
    ));
  };

  return (
    <Fragment>
      <InfiniteScroll
        dataLength={gallery.photos.length}
        next={() => getPhotos(gallery.count, gallery.start)}
        hasMore={gallery.hasMore}
        loader={<Loader />}
        endMessage={<h4 className='end-message'>Yay! You have seen it all</h4>}
      >
        <div className='masonry'>{separatePhotos()}</div>
      </InfiniteScroll>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { getPhotos, resetGallery }
)(MasonryGallery);

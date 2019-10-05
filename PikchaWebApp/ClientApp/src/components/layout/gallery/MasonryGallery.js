import React, { Fragment, useState, useEffect } from "react";
import { connect } from "react-redux";
import { getPhotos } from "../../../actions/gallery";
import MasonryColumn from "./MasonryColumn";
import InfiniteScroll from "react-infinite-scroll-component";

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
    getPhotos(gallery.count, gallery.start);
    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
    // eslint-disable-next-line
  }, []);

  const separate = () => {
    var res = [...Array(columns).keys()].map(c =>
      gallery.photos.data.filter((_, i) => i % columns === c)
    );
    return [...Array(columns)].map((column, index) => (
      <MasonryColumn key={index + 1} photos={res[index]} />
    ));
  };

  return (
    <Fragment>
      {gallery.photos !== null && (
        <InfiniteScroll
          dataLength={gallery.photos.data.length}
          next={() => getPhotos(gallery.count, gallery.start)}
          hasMore={gallery.hasMore}
          loader={<h4 className='loading'>Loading...</h4>}
          endMessage={<h4 className='loading'>Yay! You have seen it all</h4>}
          scrollableTarget={document.querySelector(".main-container")}
        >
          <div className='masonry'>{separate()}</div>
        </InfiniteScroll>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { getPhotos }
)(MasonryGallery);

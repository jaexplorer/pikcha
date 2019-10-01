import React, { useRef, useEffect } from "react";
import { connect } from "react-redux";
import MasonryItem from "./MasonryItem";

const MasonryColumn = ({ gallery, photos }) => {
  const column = useRef(null);

  useEffect(() => {
    gallery.selected !== null &&
    column.current.contains(gallery.selected.current)
      ? column.current.classList.add("selected")
      : column.current.classList.remove("selected");
  });

  return (
    <div className='masonry-col' ref={column}>
      {/* {photos.map((photo, index) => (
        <MasonryItem key={index} photo={photo} />
      ))} */}
      <MasonryItem />
      <MasonryItem />
      <MasonryItem />
      <MasonryItem />
      <MasonryItem />
    </div>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(mapStateToProps)(MasonryColumn);

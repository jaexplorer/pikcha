import React, { useRef, useEffect } from "react";
import { connect } from "react-redux";
import MasonryItem from "./MasonryItem";

const MasonryColumn = ({ gallery }) => {
  const column = useRef(null);

  useEffect(() => {
    gallery.selected !== null &&
    column.current.contains(gallery.selected.current)
      ? (column.current.style.flex = "2")
      : (column.current.style.flex = "1");
  });

  return (
    <div className='masonry-col' ref={column}>
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

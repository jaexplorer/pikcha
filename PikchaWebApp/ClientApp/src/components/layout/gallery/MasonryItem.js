import React, { useRef, useState, useEffect } from "react";
import { connect } from "react-redux";
import { selectPhoto } from "../../../actions/gallery";
import ItemInfo from "./ItemInfo";

const MasonryItem = ({ gallery, selectPhoto }) => {
  const photo = useRef(null);
  const [isSelected, setSelected] = useState(false);

  useEffect(() => {
    setSelected(gallery.selected === photo);
    isSelected
      ? (photo.current.style.height = "50rem")
      : (photo.current.style.height = "30rem");
  }, [gallery.selected, isSelected]);

  return (
    <div
      className='masonry-item'
      ref={photo}
      onClick={e => {
        !isSelected && selectPhoto(photo);
      }}
    >
      {isSelected && <ItemInfo />}
    </div>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { selectPhoto }
)(MasonryItem);

import React, { useRef, useState, useEffect } from "react";
import { connect } from "react-redux";
import { selectPhoto } from "../../../actions/gallery";
import ItemInfo from "./ItemInfo";

const MasonryItem = ({ gallery, selectPhoto }) => {
  const thisPhoto = useRef(null);
  const [isSelected, setSelected] = useState(false);

  useEffect(() => {
    setSelected(gallery.selected === thisPhoto);
    isSelected
      ? (thisPhoto.current.style.height = "50rem")
      : (thisPhoto.current.style.height = "30rem");
  }, [gallery.selected, isSelected]);

  return (
    <div
      className='masonry-item'
      ref={thisPhoto}
      onClick={e => {
        !isSelected && selectPhoto(thisPhoto);
      }}
    >
      {/* <img src={photo.watermarkedFile} alt='' /> */}
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

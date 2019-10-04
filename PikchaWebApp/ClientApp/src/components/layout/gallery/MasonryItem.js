import React, { useRef, useState, useEffect } from "react";
import { connect } from "react-redux";
import { selectPhoto } from "../../../actions/gallery";
import ItemInfo from "./ItemInfo";

const MasonryItem = ({ gallery, selectPhoto, photo }) => {
  const thisPhoto = useRef(null);
  const [isSelected, setSelected] = useState(false);

  useEffect(() => {
    setSelected(gallery.selected === thisPhoto);
    if (isSelected) {
      thisPhoto.current.classList.add("selected");
      thisPhoto.current.style.height = "45rem";
    } else {
      thisPhoto.current.classList.remove("selected");
      thisPhoto.current.style.height = photo.height + "rem";
    }
  }, [gallery.selected, isSelected]);
  return (
    <div
      className='masonry-item'
      ref={thisPhoto}
      onClick={e => {
        !isSelected && selectPhoto(thisPhoto);
      }}
    >
      <img src={"http://localhost:8000/" + photo.watermarkedFile} alt='' />
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

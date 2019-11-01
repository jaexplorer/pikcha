import React, { useRef, useState, useEffect } from "react";
import { connect } from "react-redux";
import { selectPhoto, addView } from "../../../actions/gallery";
import ItemInfo from "./ItemInfo";

const MasonryItem = ({ gallery, selectPhoto, photo, addView }) => {
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
  }, [gallery.selected, isSelected, photo.height]);

  return (
    <div
      className='masonry-item'
      ref={thisPhoto}
      onClick={e => {
        if (!isSelected) {
          selectPhoto(thisPhoto);
          addView(photo.id);
        }
      }}
    >
      <img src={photo.watermark} alt='' />
      {isSelected && <ItemInfo photo={photo} />}
    </div>
  );
};

const mapStateToProps = state => ({
  gallery: state.galleryReducer
});

export default connect(
  mapStateToProps,
  { selectPhoto, addView }
)(MasonryItem);

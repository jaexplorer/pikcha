import React, { useRef, useState, useEffect } from "react";
import ItemInfo from "./ItemInfo";
import FullscreenIcon from "../../../assets/images/fullscreen-white.png";

const MasonryItem = ({ photo }) => {
  const [selected, setSelected] = useState(false);
  const item = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      // console.log(e.path.contains(item.current));
      setSelected(item.current.contains(e.target.parentElement));
    };

    document.addEventListener("click", handleClick);
    item.current.addEventListener("contextmenu", e => {
      e.preventDefault();
    });

    return () => document.removeEventListener("click", handleClick);
  }, []);

  return (
    <div
      className={`masonry-item ${selected && "selected"}`}
      style={{ height: photo.height + "rem" }}
      ref={item}
      onContextMenu={e => e.preventDefault}
    >
      <img src={photo.thumbnail} alt='' />
      {selected && (
        <div className='fullscreen'>
          <img src={FullscreenIcon} alt='' />
        </div>
      )}
      {selected && <ItemInfo photo={photo} onChange={e => setSelected(e)} />}
    </div>
  );
};

export default MasonryItem;

import React, { useState, useEffect, useRef } from "react";
import MasonryItem from "./MasonryItem";

const MasonryColumn = ({ photos }) => {
  const [selected, setSelected] = useState(false);
  const column = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      setSelected(column.current.contains(e.target.parentElement));
    };

    document.addEventListener("click", handleClick);

    return () => document.removeEventListener("click", handleClick);
  }, []);

  return (
    <div className={`masonry-col ${selected && "selected"}`} ref={column}>
      {photos.map((photo, index) => (
        <MasonryItem key={index} photo={photo} />
      ))}
    </div>
  );
};

export default MasonryColumn;

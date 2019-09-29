import React, { useState, useEffect } from "react";
import MasonryColumn from "./MasonryColumn";

const MasonryGallery = () => {
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
      // else {
      //   setColumns(1);
      // }
      // setColumns(
      //   Math.floor(window.innerWidth / 300) - 2 < 1
      //     ? 1
      //     : Math.floor(window.innerWidth / 300) - 2
      // );
    };
    handleResize();

    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  return (
    <div className='masonry'>
      {[...Array(columns)].map((column, index) => (
        <MasonryColumn key={index + 1} />
      ))}
    </div>
  );
};

export default MasonryGallery;

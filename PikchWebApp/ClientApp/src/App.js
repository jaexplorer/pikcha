import React, { useEffect } from "react";
import LocomotiveScroll from "locomotive-scroll";
import ComingSoon from "components/ComingSoon";
import ComingSoonCSS from "assets/scss/comingsoon/comingsoon.css";

const App = () => {
  useEffect(() => {
    // eslint-disable-next-line
    const scroll = new LocomotiveScroll({
      el: document.querySelector("#js-scroll"),
      smooth: true
    });
  });

  return (
    <div id='js-scroll'>
      <ComingSoon />
    </div>
  );
};

export default App;

import React, { Fragment, useEffect, useState, useRef } from "react";
import { TweenLite, Power4 } from "gsap";

const SmoothScroll = ({ children }) => {
  const [height, setHeight] = useState(window.innerHeight);
  const [url, setUrl] = useState(window.location.pathname);
  const [scroll, setScroll] = useState(true);

  const viewport = useRef(null);

  const ro = new ResizeObserver(elements => {
    for (let elem of elements) {
      const crx = elem.contentRect;
      setHeight(crx.height);
    }
  });

  useEffect(() => {
    if (window.location.pathname !== url) {
      setScroll(false);
      window.scrollTo(0, 0);
      TweenLite.to(viewport.current, 0, {
        y: -window.pageYOffset
      });
      setScroll(true);
      setUrl(window.location.pathname);
    }
  }, [window.location.pathname]);

  useEffect(() => {
    TweenLite.set(viewport.current, { y: 0 });
    window.addEventListener("scroll", onScroll);
    ro.observe(viewport.current);

    return () => window.removeEventListener("scroll", onScroll);
  }, []);

  const onScroll = () => {
    scroll &&
      TweenLite.to(viewport.current, 1, {
        y: -window.pageYOffset,
        delay: 0.2,
        ease: Power4.easeOut
      });
  };

  return (
    <Fragment>
      <div id='viewport' ref={viewport}>
        {children}
      </div>
      <div
        style={{
          height: height
        }}
      />
    </Fragment>
  );
};

export default SmoothScroll;

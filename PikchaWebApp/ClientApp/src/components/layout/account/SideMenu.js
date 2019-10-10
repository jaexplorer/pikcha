import React, { useEffect } from "react";
import { Link } from "react-router-dom";

const SideMenu = () => {
  const menu = React.createRef();

  useEffect(() => {
    [...menu.current.children].forEach(a => {
      a.href === window.location.href
        ? a.classList.add("current")
        : a.classList.remove("current");
    });
  });

  return (
    <div id='side-menu'>
      <div className='your-space'>
        <div className='your-space-heading'>Your Space</div>
        <div className='your-space-menu' ref={menu}>
          <Link to='/account'>My Details</Link>
          <Link to='/account/photos'>My Photos</Link>
          <Link to='/account/orders'>My Orders</Link>
          <Link to='/account/payment'>Payment Details</Link>
          <Link to='/account/settings'>Settings</Link>
        </div>
      </div>
    </div>
  );
};

export default SideMenu;

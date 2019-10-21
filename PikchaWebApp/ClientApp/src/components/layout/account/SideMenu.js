import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";

const SideMenu = ({ account }) => {
  const menu = React.createRef();
  const menu2 = React.createRef();

  useEffect(() => {
    const checkCurrent = () => {
      [...menu.current.children].forEach(a => {
        a.href === window.location.href
          ? a.classList.add("current")
          : a.classList.remove("current");
      });
      if (account.user.roles === "artist") {
        [...menu2.current.children].forEach(a => {
          a.href === window.location.href
            ? a.classList.add("current")
            : a.classList.remove("current");
        });
      }
    };
    checkCurrent();
  });

  const setCurrent = theMenu => {
    [...theMenu.current.children].forEach(a => {
      a.href === window.location.href
        ? a.classList.add("current")
        : a.classList.remove("current");
    });
  };

  return (
    <div id='side-menu'>
      <div className='side-menu-container'>
        <div className='side-menu-heading'>Your Space</div>
        <div onClick={() => setCurrent(menu)} className='side-menu' ref={menu}>
          <Link to='/account'>My Details</Link>
          <Link to='/account/photos'>My Photos</Link>
          <Link to='/account/orders'>My Orders</Link>
          <Link to='/account/payment'>Payment Details</Link>
          <Link to='/account/settings'>Settings</Link>
        </div>
      </div>
      {account.user.roles === "artist" && (
        <div className='side-menu-container'>
          <div className='side-menu-heading'>For Artists</div>
          <div
            onClick={() => setCurrent(menu2)}
            className='side-menu'
            ref={menu2}
          >
            <Link to='/account/bank'>Bank Details</Link>
            <Link to='/account/socials'>Socials</Link>
            <Link to='/account/artist-photos'>My Photos</Link>
            <Link to='/account/stats'>Statistics</Link>
            <Link to='/account/artist-settings'>Settings</Link>
          </div>
        </div>
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(mapStateToProps)(SideMenu);

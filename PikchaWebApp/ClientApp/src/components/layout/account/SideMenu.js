import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { createModal } from "../../../actions/modal";
import nextIcon from "../../../assets/images/next-white.png";

const SideMenu = ({ account, createModal }) => {
  const menu = React.createRef();
  const menu2 = React.createRef();

  useEffect(() => {
    const checkCurrent = () => {
      [...menu.current.children].forEach(a => {
        a.href === window.location.href
          ? a.classList.add("current")
          : a.classList.remove("current");
      });
      if (account.user.roles.includes("Photographer")) {
        [...menu2.current.children].forEach(a => {
          a.href === window.location.href
            ? a.classList.add("current")
            : a.classList.remove("current");
        });
      }
    };
    checkCurrent();
  });

  const setCurrent = () => {
    [...menu.current.children].forEach(a => {
      a.href === window.location.href
        ? a.classList.add("current")
        : a.classList.remove("current");
    });
    if (account.user.roles.includes("Photographer")) {
      [...menu2.current.children].forEach(a => {
        a.href === window.location.href
          ? a.classList.add("current")
          : a.classList.remove("current");
      });
    }
  };

  return (
    <div id='side-menu'>
      <div className='side-menu-container'>
        <div className='side-menu-heading'>Your Space</div>
        <div onClick={() => setCurrent()} className='side-menu' ref={menu}>
          <Link to='/account'>My Details</Link>
          <Link to='/account/photos'>My Photos</Link>
          <Link to='/account/orders'>My Orders</Link>
          <Link to='/account/payment'>Payment Details</Link>
          <Link to='/account/settings'>Settings</Link>
        </div>
      </div>
      {account.user.roles.includes("Photographer") ? (
        <div className='side-menu-container'>
          <div className='side-menu-heading'>For Artists</div>
          <div onClick={() => setCurrent()} className='side-menu' ref={menu2}>
            <Link to='/account/bank'>Bank Details</Link>
            <Link to='/account/socials'>Socials</Link>
            <Link to='/account/artist-photos'>My Photos</Link>
            <Link to='/account/stats'>Statistics</Link>
            <Link to='/account/artist-settings'>Settings</Link>
          </div>
        </div>
      ) : (
        <div onClick={() => createModal("RoleChangeModal")} className='prompt'>
          <div className='prompt-title'>Become an Artist!</div>
          <div className='prompt-text'>
            It's a great way to earn money, launch your photography career and
            have a chance to be in the top 100.
          </div>
          <div className='prompt-action'>
            <img src={nextIcon} alt='' />
          </div>
        </div>
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { createModal }
)(SideMenu);

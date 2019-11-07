import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { createPromoteModal } from "../../../actions/modal";
import NextIcon from "../../../assets/images/rightarrow-black.png";
import ProfileIcon from "../../../assets/images/profile-black.png";
import PhotosIcon from "../../../assets/images/photos-black.png";
import OrdersIcon from "../../../assets/images/shopping-list-black.png";
import PaymentIcon from "../../../assets/images/wallet-black.png";
import SettingsIcon from "../../../assets/images/settings-black.png";

const SideMenu = ({ account, createPromoteModal }) => {
  const menu = React.createRef();

  useEffect(() => {
    const checkCurrent = () => {
      [...menu.current.children].forEach(a => {
        a.href === window.location.href
          ? a.classList.add("current")
          : a.classList.remove("current");
      });
    };
    checkCurrent();
  });

  const setCurrent = () => {
    [...menu.current.children].forEach(a => {
      a.href === window.location.href
        ? a.classList.add("current")
        : a.classList.remove("current");
    });
  };

  return (
    <div className={`side-nav ${!account.user && "inactive"}`}>
      <div className='side-menu-container'>
        <div className='side-menu-heading'>Your Space</div>
        <div onClick={() => setCurrent()} className='side-menu' ref={menu}>
          <Link to='/account'>
            <div className='link'>My Details</div>
            <img src={ProfileIcon} alt='' />
          </Link>
          <Link to='/account/photos'>
            <div className='link'>My Photos</div>
            <img src={PhotosIcon} alt='' />
          </Link>
          <Link to='/account/orders'>
            <div className='link'>My Orders</div>
            <img src={OrdersIcon} alt='' />
          </Link>
          <Link to='/account/payment'>
            <div className='link'>Payment Details</div>
            <img src={PaymentIcon} alt='' />
          </Link>
          <Link to='/account/settings'>
            <div className='link'>Settings</div>
            <img src={SettingsIcon} alt='' />
          </Link>
        </div>
      </div>
      {account.user && !account.user.roles.includes("Artist") && (
        <div onClick={() => createPromoteModal()} className='prompt'>
          <div className='prompt-title'>Become an Artist</div>
          <div className='prompt-action'>
            <img src={NextIcon} alt='' />
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
  { createPromoteModal }
)(SideMenu);

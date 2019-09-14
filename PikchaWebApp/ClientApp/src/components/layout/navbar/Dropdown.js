import React, { useRef, useEffect } from "react";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";
import { removeDropdown } from "../../../actions/nav";
import LogoutIcon from "../../../assets/images/logout-white.png";
import ProfileIcon from "../../../assets/images/profile-white.png";

const Dropdown = ({ logout, removeDropdown }) => {
  // Detect Clicks outside of container
  const dropdownContainer = useRef(null);
  useEffect(() => {
    document.addEventListener("click", e => {
      if (
        dropdownContainer.current &&
        !dropdownContainer.current.contains(e.target)
      ) {
        removeDropdown();
      }
    });
  });

  return (
    <div id='nav-dropdown' ref={dropdownContainer}>
      <div className='dropdown-item'>
        <span>Profile</span>
        <img src={ProfileIcon} alt='' />
      </div>
      <div
        onClick={() => {
          logout();
          removeDropdown();
        }}
        className='dropdown-item'
      >
        <span>Logout</span>
        <img src={LogoutIcon} alt='' />
      </div>
    </div>
  );
};

export default connect(
  null,
  { logout, removeDropdown }
)(Dropdown);

import React, { useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";
import { removeDropdown } from "../../../actions/nav";
import LogoutIcon from "../../../assets/images/logout-white.png";
import ProfileIcon from "../../../assets/images/profile-white.png";

const Dropdown = ({ auth, logout, removeDropdown }) => {
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
      <div onClick={removeDropdown} className='dropdown-item'>
        <Link to={`/profile/${auth.user._id}`}>Profile</Link>
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

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(
  mapStateToProps,
  { logout, removeDropdown }
)(Dropdown);

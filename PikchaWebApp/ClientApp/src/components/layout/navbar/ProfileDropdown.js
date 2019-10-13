import React, { useRef, useEffect } from "react";
import { Link, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";
import { removeProfileDropdown } from "../../../actions/nav";
import LogoutIcon from "../../../assets/images/logout-white.png";
import ProfileIcon from "../../../assets/images/profile-white.png";
import AccountIcon from "../../../assets/images/account-white.png";
import { ApplicationPaths } from "../../api-authorization/ApiAuthorizationConstants";

const ProfileDropdown = ({ auth, logout, removeProfileDropdown }) => {
  // Detect Clicks outside of container
  const dropdownContainer = useRef(null);
  useEffect(() => {
    document.addEventListener("click", e => {
      if (
        dropdownContainer.current &&
        !dropdownContainer.current.contains(e.target)
      ) {
        removeProfileDropdown();
      }
    });
  });

  return (
    <div id='profile-dropdown' ref={dropdownContainer}>
      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <Link to={`/profile/${auth.user._id}`}>Profile</Link>
        <img src={ProfileIcon} alt='' />
      </div>
      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <Link to={"/account"}>Account</Link>
        <img src={AccountIcon} alt='' />
      </div>
      <div
        onClick={() => {
          // logout();
          removeProfileDropdown();
        }}
        className='profile-dropdown-item'
      >
        <Link to={ApplicationPaths.LogOut}>Logout</Link>
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
  { logout, removeProfileDropdown }
)(ProfileDropdown);

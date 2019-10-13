import React, { useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";
import { removeProfileDropdown } from "../../../actions/nav";
import LogoutIcon from "../../../assets/images/logout-black.png";
import ProfileIcon from "../../../assets/images/profile-black.png";
import AccountIcon from "../../../assets/images/account-black.png";
import UploadIcon from "../../../assets/images/upload-white.png";
import { ApplicationPaths } from "../../auth/ApiAuthorizationConstants";

const ProfileDropdown = ({ auth, logout, removeProfileDropdown }) => {
  // Detect Clicks outside of container
  const dropdownContainer = useRef(null);
  useEffect(() => {
    document.addEventListener("mousedown", e => {
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
      <div className='upload-container'>
        <img src={UploadIcon} alt='' />
        <div className='upload-button'>Upload</div>
      </div>
      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <img src={ProfileIcon} alt='' />
        <Link to={`/profile/${auth.user._id}`}>Profile</Link>
      </div>
      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <img src={AccountIcon} alt='' />
        <Link to={"/account"}>Account</Link>
      </div>
      <div
        onClick={() => {
          // logout();
          removeProfileDropdown();
        }}
        className='profile-dropdown-item'
      >
        <img src={LogoutIcon} alt='' />
        <Link to={ApplicationPaths.LogOut}>Logout</Link>
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

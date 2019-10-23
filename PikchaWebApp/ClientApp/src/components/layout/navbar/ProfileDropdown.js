import React, { useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { removeProfileDropdown } from "../../../actions/nav";
import LogoutIcon from "../../../assets/images/logout-black.png";
import ProfileIcon from "../../../assets/images/profile-black.png";
import AccountIcon from "../../../assets/images/account-black.png";
import UploadIcon from "../../../assets/images/upload-white.png";
import { createModal } from "../../../actions/modal";
import { ApplicationPaths } from "../../auth/ApiAuthorizationConstants";

const ProfileDropdown = ({ account, removeProfileDropdown, createModal }) => {
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
      <div
        onClick={() => {
          account.user.roles.includes("Photographer")
            ? createModal("UploadModal")
            : createModal("RoleChangeModal");
          removeProfileDropdown();
        }}
        className='upload-container'
      >
        <img src={UploadIcon} alt='' />
        <div className='upload-button'>Upload</div>
      </div>
      {account.user.roles === "artist" && (
        <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
          <img src={ProfileIcon} alt='' />
          <Link to={`/profile/${account.user.id}`}>Profile</Link>
        </div>
      )}

      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <img src={AccountIcon} alt='' />
        <Link to={"/account"}>Account</Link>
      </div>
      <div onClick={removeProfileDropdown} className='profile-dropdown-item'>
        <img src={LogoutIcon} alt='' />
        <Link to={ApplicationPaths.LogOut}>Logout</Link>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { removeProfileDropdown, createModal }
)(ProfileDropdown);

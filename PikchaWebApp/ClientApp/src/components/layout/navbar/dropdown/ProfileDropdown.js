import React, { useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import ProfileIcon from "../../../../assets/images/profile-white.png";
import AccountIcon from "../../../../assets/images/account-white.png";
import LogoutIcon from "../../../../assets/images/logout-white.png";
import PeopleIcon from "../../../../assets/images/people-white.png";
import { closeDropdown, openFollowing } from "../../../../actions/nav";
import { ApplicationPaths } from "../../../../auth/ApiAuthorizationConstants";

const ProfileDropdown = ({ account, closeDropdown, openFollowing }) => {
  const dropdown = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      !dropdown.current.contains(e.target) && closeDropdown();
    };

    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });

  return (
    <div className='profile-dropdown' ref={dropdown}>
      <div onClick={() => closeDropdown()} className='dropdown-item'>
        <img src={ProfileIcon} alt='' />
        <Link to={`/profile/${account.user.id}`}>Profile</Link>
      </div>
      <div onClick={() => closeDropdown()} className='dropdown-item'>
        <img src={AccountIcon} alt='' />
        <Link to={"/account"}>Account</Link>
      </div>
      {window.innerWidth < 699 && (
        <div
          onClick={() => {
            closeDropdown();
            openFollowing();
          }}
          className='dropdown-item'
        >
          <img src={PeopleIcon} alt='' />
          <div className='option'>Following</div>
        </div>
      )}
      <div className='dropdown-item'>
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
  { closeDropdown, openFollowing }
)(ProfileDropdown);

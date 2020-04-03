import React from "react";
import { connect } from "react-redux";

import SearchIcon from "../../../assets/images/search-black.png";
import UploadIcon from "../../../assets/images/upload-white.png";
import PeopleIcon from "../../../assets/images/people-black.png";
import UpArrowIcon from "../../../assets/images/uparrow-black.png";
import {
  createPromoteModal,
  createUploadImageModal
} from "../../../actions/modal";

import { openFollowing } from "../../../actions/nav";
import SearchBar from "../search/SearchBar";

const NavbarBottom = ({
  account,
  nav,
  openFollowing,
  createPromoteModal,
  createUploadImageModal
}) => {
  return (
    <div className='navbar-bottom'>
      <div className={`nav-icon ${!account.user && "inactive"}`}>
        {nav.dropDown === "Following" ? (
          <img src={UpArrowIcon} alt='' />
        ) : (
          <img onMouseDown={() => openFollowing()} src={PeopleIcon} alt='' />
        )}
      </div>

      <div className='nav-icon'>
        <img src={SearchIcon} alt='' />
      </div>
      <SearchBar />

      <div
        onClick={() => {
          account.user && account.user.roles.includes("Artist")
            ? createUploadImageModal()
            : createPromoteModal();
        }}
        className={`upload-button ${!account.user && "inactive"}`}
      >
        <img src={UploadIcon} alt='' />
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  nav: state.navReducer,
  account: state.accountReducer
});

export default connect(mapStateToProps, {
  openFollowing,
  createPromoteModal,
  createUploadImageModal
})(NavbarBottom);

import React, { Fragment } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { createDropdown } from "../../../actions/nav";
import Logo from "../../../assets/images/logo-white.png";
import SearchIcon from "../../../assets/images/search-black.png";
import CartIcon from "../../../assets/images/cart-black.png";
import PeopleIcon from "../../../assets/images/people-black.png";
import PlaceHolder from "../../../assets/images/placeholder.png";
import Dropdown from "./Dropdown";

const Navbar = ({ auth, nav, createDropdown }) => {
  const authLinks = (
    <div className='navbar-right'>
      <div className='navbar-icons'>
        <img src={CartIcon} alt='' /> <img src={PeopleIcon} alt='' />
      </div>
      <div id='navbar-display-pic'>
        <img onClick={createDropdown} src={PlaceHolder} alt='' />
      </div>
      {nav.dropDown ? <Dropdown /> : ""}
    </div>
  );

  const guestLinks = (
    <div className='navbar-right'>
      <div className='navbar-icons'>
        <img src={SearchIcon} alt='' />
      </div>
      <div id='navbar-right-links'>
        <li>
          <Link to='/register'>Signup </Link>
        </li>
        <li>
          <Link to='/login'>Login</Link>
        </li>
      </div>
    </div>
  );

  return (
    <div id='top-navbar'>
      <div id='navbar-logo'>
        <Link to='/'>
          <img src={Logo} alt='' />
        </Link>
      </div>
      <div id='navbar'>
        <div id='navbar-left'>
          <li>
            <Link to='/pikcha100'>Pikcha 100</Link>
          </li>
          <li>
            <Link to='/artist100'>Artist 100</Link>
          </li>
        </div>
        <Fragment>{auth.isAuthenticated ? authLinks : guestLinks}</Fragment>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer,
  nav: state.navReducer
});

export default connect(
  mapStateToProps,
  { createDropdown }
)(Navbar);

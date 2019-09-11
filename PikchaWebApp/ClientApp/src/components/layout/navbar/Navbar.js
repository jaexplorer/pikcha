import React, { Fragment } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";
import Logo from "../../../assets/images/logo-white.png";
import SearchIcon from "../../../assets/images/search.png";
import CartIcon from "../../../assets/images/cart.png";
import PeopleIcon from "../../../assets/images/people.png";
import PlaceHolder from "../../../assets/images/placeholder.png";

const Navbar = ({ logout, auth }) => {
  const authLinks = (
    <div className='navbar-right'>
      <div className='navbar-icons'>
        <img src={CartIcon} alt='' /> <img src={PeopleIcon} alt='' />
      </div>
      <div id='navbar-display-pic'>
        <img onClick={logout} src={PlaceHolder} alt='' />
      </div>
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
  auth: state.authReducer
});

export default connect(
  mapStateToProps,
  { logout }
)(Navbar);

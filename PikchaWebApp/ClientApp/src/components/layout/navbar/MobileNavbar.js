import React, { Fragment, useState } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import {
  createProfileDropdown,
  createCartDropdown
} from "../../../actions/nav";
import Logo from "../../../assets/images/logo-white.png";
import SearchIcon from "../../../assets/images/search-black.png";
import CartIcon from "../../../assets/images/cart-black.png";
import PeopleIcon from "../../../assets/images/people-black.png";
import PlaceHolder from "../../../assets/images/placeholder.png";
import ProfileDropdown from "./ProfileDropdown";
import CartDropdown from "./CartDropdown";
import MenuIcon from "../../../assets/images/menu-black.png";
import CloseIcon from "../../../assets/images/delete-black.png";
import { ApplicationPaths } from "../../auth/ApiAuthorizationConstants";

const MobileNavbar = ({
  auth,
  nav,
  cart,
  createProfileDropdown,
  createCartDropdown
}) => {
  const [menuOpen, setMenuOpen] = useState(false);

  const authIcons = (
    <Fragment>
      <div className='nav-icon'>
        <img onClick={createCartDropdown} src={CartIcon} alt='' />
        {cart.products.length !== 0 && <div className='notication'></div>}
      </div>
      <div className='nav-icon'>
        <img src={PeopleIcon} alt='' />
      </div>
      <div className='nav-icon profile-pic'>
        <img onClick={createProfileDropdown} src={PlaceHolder} alt='' />
      </div>
      {nav.profileDropdown ? <ProfileDropdown /> : ""}
      {nav.cartDropdown ? <CartDropdown /> : ""}
    </Fragment>
  );

  return (
    <div id='mobile-navbar'>
      {menuOpen ? (
        <div className='nav-menu-open'>
          <div class='nav-menu-logo'>
            <Link onClick={() => setMenuOpen(false)} to='/'>
              <img src={Logo} alt='' />
            </Link>
          </div>
          <div
            onClick={e => {
              e.currentTarget.contains(e.target) && setMenuOpen(false);
            }}
            className='nav-menu-list'
          >
            <Link to='/pikcha100'>Pikcha 100</Link>
            <Link to='/artist100'>Artist 100</Link>
            <Link to='/contact'>Contact</Link>
            <Link to='/about'>About </Link>
            {!auth.isAuthenticated && (
              <Fragment>
                <Link to={ApplicationPaths.Register}>Signup </Link>
                <Link to={ApplicationPaths.Login}>Login</Link>
              </Fragment>
            )}
          </div>
          <div className='nav-menu-exit'>
            <img onClick={() => setMenuOpen(false)} src={CloseIcon} alt='' />
          </div>
        </div>
      ) : (
        <Fragment>
          <div className='nav-menu-closed'>
            <img onClick={() => setMenuOpen(true)} src={MenuIcon} alt='' />
          </div>
          <div className='nav-icons'>
            <div className='nav-icon'>
              <img src={SearchIcon} alt='' />
            </div>
            {auth.isAuthenticated && authIcons}
          </div>
        </Fragment>
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer,
  nav: state.navReducer,
  cart: state.cartReducer
});

export default connect(
  mapStateToProps,
  { createProfileDropdown, createCartDropdown }
)(MobileNavbar);

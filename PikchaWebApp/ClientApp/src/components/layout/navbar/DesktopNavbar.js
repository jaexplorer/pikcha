import React, { Fragment } from "react";
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
import { ApplicationPaths } from "../../auth/ApiAuthorizationConstants";

const MobileNavbar = ({
  auth,
  nav,
  cart,
  account,
  createProfileDropdown,
  createCartDropdown
}) => {
  const authLinks = (
    <div className='navbar-right'>
      {account.loading === false && (
        <Fragment>
          <div className='navbar-icons'>
            {/* <div className='navbar-icon'>
              <img src={SearchIcon} alt='' />
            </div> */}

            {/* <div className='navbar-icon'>
              <img onClick={createCartDropdown} src={CartIcon} alt='' />
              {cart.products.length !== 0 && <div className='notication'></div>}
              {nav.cartDropdown && <CartDropdown />}
            </div> */}
            {/* {account.user && account.user.following.length !== 0 && (
              <div className='navbar-icon'>
                <img src={PeopleIcon} alt='' />
              </div>
            )} */}
          </div>

          <div id='navbar-display-pic'>
            <img
              onClick={createProfileDropdown}
              src={account.user.avatar}
              alt=''
            />
            {nav.profileDropdown && <ProfileDropdown />}
          </div>
        </Fragment>
      )}
    </div>
  );

  const guestLinks = (
    <div className='navbar-right'>
      {/* <div className='navbar-icons'>
        <div className='navbar-icon'>
          <img src={SearchIcon} alt='' />
        </div>
      </div> */}

      <div id='navbar-right-links'>
        <li>
          <Link to={ApplicationPaths.Register}>Signup</Link>
        </li>
        <li>
          <Link to={ApplicationPaths.Login}>Login</Link>
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
  nav: state.navReducer,
  cart: state.cartReducer,
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { createProfileDropdown, createCartDropdown }
)(MobileNavbar);

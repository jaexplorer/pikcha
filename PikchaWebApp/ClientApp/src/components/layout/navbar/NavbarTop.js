import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import Logo from "../../../assets/images/logo-coloured.png";
import CartIcon from "../../../assets/images/cart-black.png";
import MenuIcon from "../../../assets/images/menu-black.png";
import { openProfile, openMenu, openCart } from "../../../actions/nav";
import { ApplicationPaths } from "../../../auth/ApiAuthorizationConstants";

const NavbarTop = ({ auth, account, openProfile, openMenu, openCart }) => {
  return (
    <div className='navbar-top'>
      <div className='guest-items'>
        <div className='nav-logo'>
          <Link to='/'>
            <img src={Logo} alt='' />
          </Link>
        </div>
        <Link to='/pikcha100'>Pikcha 100</Link>
        <Link to='/artist100'>Artist 100</Link>
      </div>
      {auth.isAuthenticated ? (
        <div className='auth-items'>
          <div className={`auth-item ${!account.user && "inactive"}`}>
            <img src={CartIcon} alt='' />
          </div>
          <div className={`auth-item ${!account.user && "inactive"}`}>
            <img onMouseDown={() => openMenu()} src={MenuIcon} alt='' />
          </div>
          <div className={`user-item ${!account.user && "inactive"}`}>
            {account.user && (
              <img
                onMouseDown={() => openProfile()}
                className='user-pic'
                src={account.user.avatar}
                alt=''
              />
            )}
          </div>
        </div>
      ) : (
        <div className='auth-links'>
          <Link to={ApplicationPaths.Register}>Signup</Link>
          <Link to={ApplicationPaths.Login}>Login</Link>
        </div>
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer,
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { openProfile, openMenu, openCart }
)(NavbarTop);

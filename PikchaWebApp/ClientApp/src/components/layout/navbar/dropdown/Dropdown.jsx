import React, { Fragment } from "react";
import { connect } from "react-redux";
import ProfileDropdown from "./ProfileDropdown";
import MenuDropdown from "./MenuDropdown";
import CartDropdown from "./CartDropdown";
import FollowingDropdown from "./FollowingDropdown";

const Dropdown = ({ nav }) => {
  return (
    <Fragment>
      {nav.dropDown === "Profile" && <ProfileDropdown />}
      {nav.dropDown === "Menu" && <MenuDropdown />}
      {nav.dropDown === "Cart" && <CartDropdown />}
      {nav.dropDown === "Following" && <FollowingDropdown />}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  nav: state.navReducer
});

export default connect(mapStateToProps)(Dropdown);

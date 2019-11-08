import React, { Fragment } from "react";
import NavbarTop from "./NavbarTop";
import NavbarBottom from "./NavbarBottom";
import Dropdown from "./dropdown/Dropdown";

const Navbar = () => {
  return (
    <div className='nav'>
      <NavbarTop />
      <Dropdown />
      <NavbarBottom />
    </div>
  );
};

export default Navbar;

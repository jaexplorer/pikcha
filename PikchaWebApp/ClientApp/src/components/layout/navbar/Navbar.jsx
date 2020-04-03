import React from 'react';
import NavbarTop from './NavbarTop';
import NavbarBottom from './NavbarBottom';
import Dropdown from './dropdown/Dropdown';

const Navbar = ({ width, height }) => {
  return (
    <div className='nav'>
      <NavbarTop/>
      <Dropdown/>
      <NavbarBottom
        width={width}
        height={height}
      />
    </div>
  );
};

export default Navbar;

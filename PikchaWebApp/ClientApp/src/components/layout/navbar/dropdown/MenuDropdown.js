import React, { Fragment, useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { closeDropdown } from "../../../../actions/nav";

const MenuDropdown = ({ closeDropdown }) => {
  const dropdown = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      !dropdown.current.contains(e.target) && closeDropdown();
    };

    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });

  return (
    <div className='menu-dropdown' ref={dropdown}>
      {window.innerWidth < 699 && (
        <Fragment>
          <div className='dropdown-item'>
            <Link onClick={() => closeDropdown()} to=''>
              Pikcha 100
            </Link>
          </div>
          <div className='dropdown-item'>
            <Link onClick={() => closeDropdown()} to=''>
              Artist 100
            </Link>
          </div>
        </Fragment>
      )}
      <div className='dropdown-item'>
        <Link onClick={() => closeDropdown()} to='/contact'>
          Contact us
        </Link>
      </div>
      <div className='dropdown-item'>
        <Link onClick={() => closeDropdown()} to='/about'>
          About
        </Link>
      </div>
      <div className='dropdown-item'>
        <Link onClick={() => closeDropdown()} to=''>
          FAQ
        </Link>
      </div>
    </div>
  );
};

export default connect(
  null,
  { closeDropdown }
)(MenuDropdown);

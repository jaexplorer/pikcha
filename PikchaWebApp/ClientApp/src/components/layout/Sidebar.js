import React, { Fragment } from "react";
import { Link } from "react-router-dom";

const Sidebar = () => {
  return (
    <div id='sidebar'>
      <Link to='/about'>About</Link>
      <span className='line'></span>
      <Link to='/Contact'>Contact</Link>
    </div>
  );
};

export default Sidebar;

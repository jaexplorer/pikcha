import React from "react";
import PropTypes from "prop-types";

const Header = props => {
  return (
    <div className='title-header'>
      <div className='header-subtitle'>{props.subtitle}</div>
      <div className='header-line'></div>
      <div className='header-title'>{props.title}</div>
    </div>
  );
};

Header.propTypes = {
  subtitle: PropTypes.string.isRequired,
  title: PropTypes.string.isRequired
};

export default Header;

import React from "react";
import { connect } from "react-redux";
import { logout } from "../../../actions/auth";

const Dropdown = ({ logout }) => {
  return (
    <div className='dropdown-item'>
      <span onClick={logout}>Logout</span>
    </div>
  );
};

export default connect(
  null,
  { logout }
)(Dropdown);

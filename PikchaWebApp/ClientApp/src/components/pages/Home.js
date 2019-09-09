import React, { Fragment } from "react";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { logout } from "../../actions/auth";

const Home = ({ auth, logout }) => {
  const authLinks = (
    <Fragment>
      <div>Hello {auth.user && auth.user.name}</div>
      <a onClick={logout} href='#!'>
        Logout
      </a>
    </Fragment>
  );

  const guestLinks = (
    <Fragment>
      <li>
        <Link to='/register'>Register</Link>
      </li>
      <li>
        <Link to='/login'>Login</Link>
      </li>
    </Fragment>
  );

  if (auth.loading) {
    return <h2>Loading...</h2>;
  }

  return (
    <Fragment>
      <div>Home</div>
      <div>{auth.isAuthenticated ? authLinks : guestLinks}</div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(
  mapStateToProps,
  { logout }
)(Home);

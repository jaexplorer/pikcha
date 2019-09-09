import React, { useState, useEffect } from "react";
import { Link, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { login, clearErrors } from "../../actions/auth";
import { setAlert } from "../../actions/alert";

const Login = ({ login, auth, clearErrors, setAlert }) => {
  // Component State
  const [user, setUser] = useState({
    email: "",
    password: ""
  });

  // Destructuring Component State
  const { email, password } = user;

  // Update Component State on change
  const onChange = e => setUser({ ...user, [e.target.name]: e.target.value });

  useEffect(() => {
    if (auth.error === "Invalid Credentials") {
      setAlert(auth.error, "danger");
      clearErrors();
    }
    // eslint-disable-next-line
  }, [auth.error]);

  const onSubmit = e => {
    e.preventDefault();
    if (email === "" || password === "") {
      setAlert("Please fill in all fields", "danger");
    } else {
      login({ email, password });
    }
  };

  if (auth.loading) {
    return <h2>Loading...</h2>;
  }

  if (auth.isAuthenticated) {
    return <Redirect to='/' />;
  }

  return (
    <div>
      <h1>
        Account <span>Login</span>
      </h1>
      <form onSubmit={onSubmit}>
        <div>
          <label htmlFor='email'>Email Address</label>
          <input
            type='email'
            name='email'
            value={email}
            onChange={onChange}
            // required
          />
        </div>
        <div>
          <label htmlFor='password'>Password</label>
          <input
            type='password'
            name='password'
            value={password}
            onChange={onChange}
            // required
          />
        </div>
        <input type='submit' value='Login' />
      </form>
      <p>
        Don't have an account? <Link to='/register'>Register</Link>
      </p>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(
  mapStateToProps,
  { login, clearErrors, setAlert }
)(Login);

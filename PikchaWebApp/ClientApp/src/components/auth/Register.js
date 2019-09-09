import React, { useState, useEffect } from "react";
import { Link, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { register, clearErrors } from "../../actions/auth";
import { setAlert } from "../../actions/alert";

const Register = ({ auth, register, clearErrors, setAlert }) => {
  // Component State
  const [user, setUser] = useState({
    name: "",
    email: "",
    password: "",
    password2: ""
  });

  // Destructuring Component State
  const { name, email, password, password2 } = user;

  // Update Component State on change
  const onChange = e => setUser({ ...user, [e.target.name]: e.target.value });

  useEffect(() => {
    if (auth.error === "User already exists") {
      setAlert(auth.error, "danger");
      clearErrors();
    }
    // eslint-disable-next-line
  }, [auth.error]);

  const onSubmit = e => {
    e.preventDefault();
    if (name === "" || email === "" || password === "") {
      setAlert("Please enter all fields", "danger");
    } else if (password !== password2) {
      setAlert("Passwords do not match", "danger");
    } else {
      register({
        name,
        email,
        password
      });
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
        Account <span>Register</span>
      </h1>
      <form onSubmit={onSubmit}>
        <div>
          <label htmlFor='name'>Name</label>
          <input
            type='text'
            name='name'
            value={name}
            onChange={onChange}
            // required
          />
        </div>
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
            // minLength='6'
          />
        </div>
        <div>
          <label htmlFor='password2'>Confirm Password</label>
          <input
            type='password'
            name='password2'
            value={password2}
            onChange={onChange}
            // required
            // minLength='6'
          />
        </div>
        <input type='submit' value='Register' />
      </form>
      <p>
        Already have an account? <Link to='/login'>Login</Link>
      </p>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(
  mapStateToProps,
  { register, clearErrors, setAlert }
)(Register);

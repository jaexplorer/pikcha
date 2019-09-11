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
    return <h2 className='loading'>Loading...</h2>;
  }

  if (auth.isAuthenticated) {
    return <Redirect to='/' />;
  }

  return (
    <div className='auth-container'>
      <div className='auth-wrapper'>
        <form className='auth-form' onSubmit={onSubmit}>
          <div className='form-title'>Signup</div>
          <div className='input-wrapper'>
            <input
              type='text'
              name='name'
              value={name}
              onChange={onChange}
              placeholder='Name'
            />
          </div>
          <div className='input-wrapper'>
            <input
              type='email'
              name='email'
              value={email}
              onChange={onChange}
              placeholder='Email'
            />
          </div>

          <div className='input-wrapper'>
            <input
              type='password'
              name='password'
              value={password}
              onChange={onChange}
              placeholder='Password'
            />
          </div>
          <div className='input-wrapper'>
            <input
              type='password'
              name='password2'
              value={password2}
              onChange={onChange}
              placeholder='Confirm Password'
            />
          </div>

          <div className='form-btn'>
            <input type='submit' value='Register' />
          </div>

          <div className='text-center'>
            <Link to='/login'>Already have an account?</Link>
          </div>
        </form>
      </div>
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

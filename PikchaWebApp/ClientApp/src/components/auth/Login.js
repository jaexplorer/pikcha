import React, { useState, useEffect } from "react";
import { Link, Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { login, clearErrors } from "../../actions/auth";
import { setAlert } from "../../actions/alert";
import Logo from "../../assets/images/logo-coloured.png";

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
    return <h2 className='loading'>Loading...</h2>;
  }

  if (auth.isAuthenticated) {
    return <Redirect to='/' />;
  }

  return (
    <div className='auth-container'>
      <div className='auth-wrapper'>
        <form className='auth-form' onSubmit={onSubmit}>
          <div class='form-logo'>
            <img src={Logo} alt='' />
          </div>
          <div className='form-title'>Log in</div>
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

          <div className='form-btn'>
            <input type='submit' value='Login' />
          </div>

          <div className='text-center'>
            <Link to='/register'>Don't have an account?</Link>
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
  { login, clearErrors, setAlert }
)(Login);

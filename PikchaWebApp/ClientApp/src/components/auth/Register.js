import React, { useState } from "react";

const Register = () => {
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

  const onSubmit = e => {
    e.preventDefault();
    if (name === "" || email === "" || password === "") {
      console.log("Please enter all fields");
    } else if (password !== password2) {
      console.log("Passwords do not match");
    } else {
      console.log("Registered");
    }
  };

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
            required
          />
        </div>
        <div>
          <label htmlFor='email'>Email Address</label>
          <input
            type='email'
            name='email'
            value={email}
            onChange={onChange}
            required
          />
        </div>
        <div>
          <label htmlFor='password'>Password</label>
          <input
            type='password'
            name='password'
            value={password}
            onChange={onChange}
            required
            minLength='6'
          />
        </div>
        <div>
          <label htmlFor='password2'>Confirm Password</label>
          <input
            type='password'
            name='password2'
            value={password2}
            onChange={onChange}
            required
            minLength='6'
          />
        </div>
        <input type='submit' value='Register' />
      </form>
    </div>
  );
};

export default Register;

import React, { useState } from "react";

const Login = () => {
  // Component State
  const [user, setUser] = useState({
    email: "",
    password: ""
  });

  // Destructuring Component State
  const { email, password } = user;

  // Update Component State on change
  const onChange = e => setUser({ ...user, [e.target.name]: e.target.value });

  const onSubmit = e => {
    e.preventDefault();
    if (email === "" || password === "") {
      console.log("Please fill in all fields");
    } else {
      console.log("Logged in");
    }
  };

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
          />
        </div>
        <input type='submit' value='Login' />
      </form>
    </div>
  );
};

export default Login;

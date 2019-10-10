// TODO: Uploading Profile Picture, Integrate with backend
import React, { useState, Fragment } from "react";
import ProfilePic from "../../../assets/images/profilePic.png";

const MyDetails = () => {
  // Component State
  const [user, setUser] = useState({
    firstName: "",
    lastName: "",
    bio: "",
    email: "",
    phone: "",
    address1: "",
    address2: "",
    country: "",
    state: "",
    city: "",
    zip: ""
  });

  // Destructuring Component State
  const {
    firstName,
    lastName,
    bio,
    email,
    phone,
    address1,
    address2,
    country,
    state,
    city,
    zip
  } = user;

  // Update Component State on change
  const onChange = e => setUser({ ...user, [e.target.name]: e.target.value });

  return (
    <Fragment>
      <div className='account-title'>My Account</div>
      <form className='account-form'>
        <div className='form-section'>
          <div className='section-title'>Personal Information</div>
          <div className='person-details-container'>
            <div className='display-picture'>
              <img src={ProfilePic} alt='' />
            </div>
            <div className='name'>
              <div className='input-wrapper'>
                <div className='input-title'>First Name</div>
                <input
                  className='input-field'
                  type='text'
                  name='firstName'
                  value={firstName}
                  onChange={onChange}
                />
              </div>
              <div className='input-wrapper'>
                <div className='input-title'>Last Name</div>
                <input
                  className='input-field'
                  type='text'
                  name='lastName'
                  value={lastName}
                  onChange={onChange}
                />
              </div>
            </div>
            <div className='bio'>
              <div className='input-wrapper'>
                <div className='input-title'>Bio</div>
                <textarea
                  maxLength='100'
                  className='input-field'
                  type='text'
                  name='bio'
                  value={bio}
                  onChange={onChange}
                />
              </div>
            </div>
          </div>
        </div>

        <div className='form-section'>
          <div className='section-title'>Contact Information</div>
          <div className='contact-details-container'>
            <div className='email'>
              <div className='input-wrapper'>
                <div className='input-title'>Email</div>
                <input
                  className='input-field'
                  type='email'
                  name='email'
                  value={email}
                  onChange={onChange}
                />
              </div>
            </div>
            <div className='phone'>
              <div className='input-wrapper'>
                <div className='input-title'>Phone</div>
                <input
                  className='input-field'
                  type='tel'
                  name='phone'
                  value={phone}
                  onChange={onChange}
                />
              </div>
            </div>
          </div>
        </div>

        <div className='form-section'>
          <div className='section-title'>Shipping Information</div>
          <div className='shipping-details-container'>
            <div className='shipping-wrapper-one'>
              <div className='row-one'>
                <div className='input-wrapper'>
                  <div className='input-title'>Address 1</div>
                  <input
                    className='input-field'
                    type='text'
                    name='address1'
                    value={address1}
                    onChange={onChange}
                  />
                </div>
              </div>
              <div className='row-two'>
                <div className='country'>
                  <div className='input-wrapper'>
                    <div className='input-title'>Country</div>
                    <input
                      className='input-field'
                      type='text'
                      name='country'
                      value={country}
                      onChange={onChange}
                    />
                  </div>
                </div>
                <div className='state'>
                  <div className='input-wrapper'>
                    <div className='input-title'>State/Territory</div>
                    <input
                      className='input-field'
                      type='text'
                      name='state'
                      value={state}
                      onChange={onChange}
                    />
                  </div>
                </div>
              </div>
            </div>
            <div className='shipping-wrapper-two'>
              <div className='row-one'>
                <div className='input-wrapper'>
                  <div className='input-title'>Address 2</div>
                  <input
                    className='input-field'
                    type='text'
                    name='address2'
                    value={address2}
                    onChange={onChange}
                  />
                </div>
              </div>
              <div className='row-two'>
                <div className='city'>
                  <div className='input-wrapper'>
                    <div className='input-title'>City</div>
                    <input
                      className='input-field'
                      type='text'
                      name='city'
                      value={city}
                      onChange={onChange}
                    />
                  </div>
                </div>
                <div className='zip'>
                  <div className='input-wrapper'>
                    <div className='input-title'>Zip Code</div>
                    <input
                      className='input-field'
                      type='text'
                      name='zip'
                      value={zip}
                      onChange={onChange}
                      pattern='[0-9]*'
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className='form-btn'>
          <input type='submit' value='Save' />
        </div>
      </form>
    </Fragment>
  );
};

export default MyDetails;

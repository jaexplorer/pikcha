import React, { useState } from "react";
import { connect } from "react-redux";
import { createDPModal } from "../../../../actions/modal";
import CameraIcon from "../../../../assets/images/camera-white.png";
import { setAlert } from "../../../../actions/alert";
import { updateUserDetails } from "../../../../actions/account";

const MyDetails = ({ account, setAlert, updateUserDetails, createDPModal }) => {
  // Component State
  const [user, setUser] = useState({
    fName: account.user.fName,
    lName: account.user.lName,
    bio: account.user.bio,
    email: account.user.email,
    phone: account.user.phone,
    addr1: account.user.addr1,
    addr2: account.user.addr2,
    country: account.user.country,
    state: account.user.state,
    city: account.user.city,
    postal: account.user.postal
  });

  // Destructuring Component State
  const {
    fName,
    lName,
    bio,
    email,
    phone,
    addr1,
    addr2,
    country,
    state,
    city,
    postal
  } = user;

  // Update Component State on change
  const onChange = e => setUser({ ...user, [e.target.name]: e.target.value });

  const onSubmit = e => {
    e.preventDefault();

    if (fName === "" || lName === "" || email === "") {
      fName === "" && setAlert("Please fill in your first name", "danger");
      lName === "" && setAlert("Please fill in your last name", "danger");
      email === "" && setAlert("Please fill in your last email", "danger");
    } else {
      updateUserDetails(user);
    }
  };

  return (
    <div className='account-container'>
      <div className='account-title'>My Account</div>
      <form className='account-form' onSubmit={onSubmit}>
        <div className='form-section'>
          <div className='section-title'>Personal Information</div>
          <div className='person-details-container'>
            <div className='display-picture'>
              <img src={account.user.avatar} alt='' />
              <div onClick={() => createDPModal()} className='edit'>
                <img src={CameraIcon} alt='' />
              </div>
            </div>
            <div className='name'>
              <div className='input-wrapper'>
                <div className='input-title'>First Name</div>
                <input
                  className='input-field'
                  type='text'
                  name='fName'
                  value={fName}
                  onChange={onChange}
                />
              </div>
              <div className='input-wrapper'>
                <div className='input-title'>Last Name</div>
                <input
                  className='input-field'
                  type='text'
                  name='lName'
                  value={lName}
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
          <div className='contact-information-container'>
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
                    name='addr1'
                    value={addr1}
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
                    name='addr2'
                    value={addr2}
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
                    <div className='input-title'>Postcode</div>
                    <input
                      className='input-field'
                      type='text'
                      name='postal'
                      value={postal}
                      onChange={onChange}
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
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { updateUserDetails, setAlert, createDPModal }
)(MyDetails);

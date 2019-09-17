import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import Header from "../layout/Header";
import ContactDetails from "../layout/contact/ContactDetails";
import ContactForm from "../layout/contact/ContactForm";
import ContactMap from "../layout/contact/ContactMap";

const Contact = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='main-container'>
        <div className='main-content-container'>
          <Header subtitle='Get in touch' title='Contact Us' />
          <div id='contact-container'>
            <ContactDetails />
            <ContactMap />
            <ContactForm />
          </div>
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Contact);

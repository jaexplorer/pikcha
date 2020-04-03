import React, { Fragment } from "react";
import ContactDetails from "../layout/contact/ContactDetails";
import ContactMap from "../layout/contact/ContactMap";

const Contact = () => {
  return (
    <Fragment>
      <div className='page-title'>Contact us</div>
      <ContactDetails />
      <ContactMap />
    </Fragment>
  );
};

export default Contact;

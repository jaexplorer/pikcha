import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import Header from "../layout/Header";
import ContactDetails from "../layout/contact/ContactDetails";
import ContactMap from "../layout/contact/ContactMap";
import MainComponent from "../MainComponent";

const Contact = () => {
  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Get in touch' title='Contact Us' />
      <MainComponent container='contact-container'>
        <ContactDetails />
        <ContactMap />
      </MainComponent>
    </Fragment>
  );
};

export default Contact;

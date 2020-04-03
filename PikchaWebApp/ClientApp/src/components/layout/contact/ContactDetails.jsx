import React from "react";

const ContactDetails = () => {
  return (
    <div className='contact-details-container'>
      <div className='phone-contact'>
        <div className='phone-contact-title'>Phone</div>
        <div className='phone-contact-text'>+61 123 456 789</div>
      </div>
      <div className='fax-contact'>
        <div className='fax-contact-title'>Fax</div>
        <div className='fax-contact-text'>+61 123 456 789</div>
      </div>
      <div className='address-contact'>
        <div className='address-contact-title'>Address</div>
        <div className='address-contact-text'>
          31 Fletcher's Road, Melbourne, Vic, 3000
        </div>
      </div>
      <div className='email-contact'>
        <div className='email-contact-title'>Email</div>
        <div className='email-contact-text'>contactpikcha@gmail.com</div>
      </div>
    </div>
  );
};

export default ContactDetails;

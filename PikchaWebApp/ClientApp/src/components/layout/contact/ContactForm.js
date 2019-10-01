import React from "react";

const ContactForm = () => {
  return (
    <div className='contact-form-container'>
      <div className='contact-form-title'>Send us a Message</div>
      <div className='contact-form-subtext'>
        Feel free to get in touch with us about anything related to Pikcha or
        <br />
        you can just say hi. We will get back to you as soon as we can.
      </div>
      <form className='contact-form'>
        <div className='input-wrapper'>
          <input
            type='text'
            name='name'
            // value={name}
            // onChange={onChange}
            placeholder='Name'
          />
        </div>
        <div className='input-wrapper'>
          <input
            type='email'
            name='email'
            // value={email}
            // onChange={onChange}
            placeholder='Email'
          />
        </div>
        <div className='input-wrapper'>
          <input
            type='text'
            name='subject'
            // value={subject}
            // onChange={onChange}
            placeholder='Subject'
          />
        </div>
        <div className='input-wrapper'>
          <input
            type='textarea'
            name='message'
            // value={message}
            // onChange={onChange}
            placeholder='Message'
          />
        </div>

        <div className='form-btn'>
          <input type='submit' value='Send' />
        </div>
      </form>
    </div>
  );
};

export default ContactForm;

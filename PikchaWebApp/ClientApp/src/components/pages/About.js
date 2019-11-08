import React, { Fragment } from "react";

const About = () => {
  return (
    <Fragment>
      <div className='page-title'>About us</div>
      <div className='about-item'>
        <span className='about-title'>How Pikcha Works</span>
        <p className='about-text'>
          Artist can upload 1 photo per day
          <br />
          <br />
          Each photo is limited to 100 phyical prints
          <br />
          <br />
          Select your image and customize your print size and materials
          <br />
          <br />
          If you love your artwork, hold it!
          <br />
          if you want to sell it, place your print back on the market!
        </p>
      </div>
      <div className='about-item'>
        <span className='about-title'>Photographer's Guide</span>
        <p className='about-text'>
          You can upload up to 1 photo per day.
          <br />
          Select the amount you want to recieve from each sale.
          <br />
          <br />
          All photographers are welcome!
          <br />
          Here are a few tips to help:
          <br />
          <br />
          <strong>- The photo is an original that you own </strong>
          <br />
          - Your photo meets the 5 megapixel minimum
          <br />
          - The photo is not a composite
          <br />
          - The photo is not over-edited
          <br />
          - Photos do not contain explicit nudity/discrimination
          <br />
          - Photos do not have borders/graphics/watermarks or text laid over
          them.
          <br />
          <br />
          <strong>
            Images uploaded to Pikcha are owned by Pikcha.
            <br />
            You must not share this image anywhere.
            <br />
            Respect your customers and followers.
          </strong>
        </p>
      </div>
    </Fragment>
  );
};

export default About;

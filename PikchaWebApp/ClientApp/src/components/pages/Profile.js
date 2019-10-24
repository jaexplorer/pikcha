// TODO: Fix Error handling, Fix CSS, FIX Loading position

import React, { Fragment, useEffect } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import SideBar from "../layout/Sidebar";
import ProfilePic from "../../assets/images/profilePic.png";
import FacebookIcon from "../../assets/images/facebook-black.png";
import InstagramIcon from "../../assets/images/instagram-black.png";
import TwitterIcon from "../../assets/images/twitter-black.png";
import MoreIcon from "../../assets/images/more-white.png";
import MasonryGallery from "../layout/gallery/MasonryGallery";
import BackArrow from "../layout/BackArrow";
import NotFound from "./NotFound";
import { getArtist } from "../../actions/artist";
import MainComponent from "../MainComponent";

const Profile = ({ artist, getArtist }) => {
  useEffect(() => {
    const profileId = window.location.pathname.split("/");
    getArtist(profileId[2]);
    // eslint-disable-next-line
  }, []);

  if (artist.error !== null) {
    return <Redirect to='/notFound' component={NotFound} />;
  }

  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      {artist.loading ? (
        <h2 className='loading'>Loading...</h2>
      ) : (
        <Fragment>
          <MainComponent container='profile-container'>
            <div className='profile-summary-container'>
              <div className='first-container'>
                <div className='artist-picture'>
                  <img src={ProfilePic} alt='' />
                </div>
                <div className='artist-details'>
                  <div className='artist-name'>Anton Mihalcov</div>
                  <div className='artist-location'>Melbourne, Australia</div>
                </div>
                <button className='artist-action'>Follow</button>

                <div className='artist-description'>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit.
                  Quisquam eum nesciunt dolor architecto, amet voluptates quo
                  incidunt atque illo. Esse consequatur accusamus alias
                  distinctio fuga maiores modi ratione asperiores vitae ullam
                  similique, aspernatur iusto maxime debitis itaque sit.
                </div>
              </div>
              <div className='second-container'>
                <div className='artist-description'>
                  Lorem ipsum dolor sit amet consectetur adipisicing elit.
                  Quisquam eum nesciunt dolor architecto, amet voluptates quo
                  incidunt atque illo. Esse consequatur accusamus alias
                  distinctio fuga maiores modi ratione asperiores vitae ullam
                  similique, aspernatur iusto maxime debitis itaque sit.
                </div>
                <div className='artist-stats'>
                  <div className='artist-views'>390k Views</div>
                  <div className='artist-followers'>2.2k Followers</div>
                  <div className='photos-sold'>20,930 Photos Sold</div>
                  <div className='average-price'>$420.30 Average Price</div>
                </div>
                <div className='following'>
                  <div className='following-title'>Following</div>
                  <div className='following-container'>
                    <div className='following-wrapper'>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                      <div className='followed'>
                        <img src={ProfilePic} alt='' />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <MasonryGallery />
          </MainComponent>
        </Fragment>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  artist: state.artistReducer
});

export default connect(
  mapStateToProps,
  { getArtist }
)(Profile);

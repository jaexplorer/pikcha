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

  // API DOESN'T WORK, WAITING ON THANANJI.
  if (artist.loading === false && artist.artist.status === "Error Occured") {
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
            <div id='profile-basic'>
              <div id='profile-picture'>
                <img src={ProfilePic} alt='' />
              </div>
              <div id='profile-name'>Anton Mihalcov</div>
              <div id='profile-location'>Melbourne, Australia</div>
              <div id='profile-socials'>
                <div className='social-button'>
                  <img src={FacebookIcon} alt='' />
                </div>
                <div className='social-button'>
                  <img src={InstagramIcon} alt='' />
                </div>
                <div className='social-button'>
                  <img src={TwitterIcon} alt='' />
                </div>
              </div>
              <div id='profile-button'>
                <button>Follow</button>
              </div>
            </div>
            <div id='profile-background'>
              <div id='profile-bio'>
                Lorem ipsum dolor sit amet consectetur adipisicing elit.
                Reprehenderit adipisci quisquam tenetur consectetur distinctio
                libero error ipsa animi eaque deserunt, incidunt cupiditate?
                Vero quo reiciendis eligendi sapiente quasi facilis, tempore
                dolor.
              </div>
              <div id='profile-overlay'></div>
              <div id='profile-options'>
                <img src={MoreIcon} alt='' />
              </div>
            </div>
            <div id='profile-stats'>
              <div id='profile-views'>390k Views</div>
              <div id='profile-followers'>2.2k Followers</div>
              <div id='profile-photos-sold'>20,930 Photos Sold</div>
              <div id='profile-average-price'>
                $420.30 Average Price <span>(0.42%)</span>
              </div>
              <div id='profile-following'>
                <span>FOLLOWING</span>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
                <div className='following-item'></div>
              </div>
            </div>
          </MainComponent>
          <MasonryGallery />
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

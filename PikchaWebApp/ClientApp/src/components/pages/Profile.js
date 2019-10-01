import React, { Fragment } from "react";
import { connect } from "react-redux";
import SideBar from "../layout/Sidebar";
import ProfilePic from "../../assets/images/profilePic.png";
import FacebookIcon from "../../assets/images/facebook-black.png";
import InstagramIcon from "../../assets/images/instagram-black.png";
import TwitterIcon from "../../assets/images/twitter-black.png";
import MoreIcon from "../../assets/images/more-white.png";
import BackArrowIcon from "../../assets/images/left-arrow-black.png";
import MasonryGallery from "../layout/gallery/MasonryGallery";

const Profile = ({ auth }) => {
  if (auth.loading) {
    return <h2 className='loading'>Loading...</h2>;
  }

  return (
    <Fragment>
      <SideBar />
      <div className='header-back-arrow' onClick={() => window.history.back()}>
        <img src={BackArrowIcon} alt='' />
        <span>BACK</span>
      </div>
      <div className='main-container'>
        <div className='main-content-container'>
          <div id='profile-container'>
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
          </div>
          <MasonryGallery />
        </div>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(Profile);

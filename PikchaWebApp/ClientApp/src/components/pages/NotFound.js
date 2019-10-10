import React, { Fragment } from "react";
import SideBar from "../layout/Sidebar";
import BackArrow from "../layout/BackArrow";

const NotFound = () => {
  return (
    <Fragment>
      <SideBar />
      <BackArrow />
      <div className='main-container'>
        <div className='main-content-container'>
          <div className='notFound-container'>
            <h1>Not Found</h1>
            <p>The page you are looking for does not exist</p>
          </div>
        </div>
      </div>
    </Fragment>
  );
};

export default NotFound;

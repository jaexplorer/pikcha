import React from "react";
import { connect } from "react-redux";

const MainComponent = ({ auth, container, children }) => {
  return (
    <div className='main-container'>
      <div className='main-content-container'>
        {auth.loading ? (
          <h2 className='loading'>Loading...</h2>
        ) : (
          <div id={container}>{children}</div>
        )}
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(MainComponent);

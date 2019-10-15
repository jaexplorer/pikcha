import React, { Fragment } from "react";
import { connect } from "react-redux";
import ModalManager from "./layout/modals/ModalManager";

const MainComponent = ({ auth, container, children }) => {
  return (
    <div className='main-container'>
      <div className='main-content-container'>
        {auth.loading ? (
          <h2 className='loading'>Loading...</h2>
        ) : (
          <Fragment>
            <ModalManager />
            <div id={container}>{children}</div>
          </Fragment>
        )}
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  auth: state.authReducer
});

export default connect(mapStateToProps)(MainComponent);

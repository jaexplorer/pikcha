import React, { Fragment } from "react";
import { connect } from "react-redux";
import DPModal from "./modals/DPModal";
import PromoteModal from "./modals/PromoteModal";
import UploadModal from "./uploadModal/UploadModal";
import CoverModal from "./modals/CoverModal";
import FullscreenModal from "./modals/FullscreenModal";

const Modal = ({ modal }) => {
  return (
    <Fragment>
      {modal.type === "DP" && <DPModal />}
      {modal.type === "Promote" && <PromoteModal />}
      {modal.type === "UploadImage" && <UploadModal />}
      {modal.type === "Cover" && <CoverModal />}
      {modal.type === "Fullscreen" && <FullscreenModal />}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps)(Modal);

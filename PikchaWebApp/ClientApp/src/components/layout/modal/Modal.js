import React, { Fragment } from "react";
import { connect } from "react-redux";
import DPModal from "./modals/DPModal";
import PromoteModal from "./modals/PromoteModal";
import UploadModal from "./modals/UploadModal";

const Modal = ({ modal }) => {
  return (
    <Fragment>
      {modal.type === "DP" && <DPModal />}
      {modal.type === "Promote" && <PromoteModal />}
      {modal.type === "UploadImage" && <UploadModal />}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps)(Modal);

import React, { Fragment } from "react";
import { connect } from "react-redux";
import DPModal from "./DPModal";
import RoleChangeModal from "./RoleChangeModal";
import UploadModal from "./UploadModal";

const ModalManager = ({ modal }) => {
  const createModal = () => {
    switch (modal.type) {
      case "DPModal":
        return <DPModal />;
      case "RoleChangeModal":
        return <RoleChangeModal />;
      case "UploadModal":
        return <UploadModal />;
    }
  };

  return <Fragment>{createModal()}</Fragment>;
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps)(ModalManager);

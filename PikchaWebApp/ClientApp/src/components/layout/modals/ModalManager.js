import React, { Fragment } from "react";
import { connect } from "react-redux";
import DPModal from "./DPModal";
import RoleChangeModal from "./RoleChangeModal";

const ModalManager = ({ modal }) => {
  const createModal = () => {
    switch (modal.type) {
      case "DPModal":
        return <DPModal />;
      case "RoleChangeModal":
        return <RoleChangeModal />;
    }
  };

  return <Fragment>{createModal()}</Fragment>;
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps)(ModalManager);

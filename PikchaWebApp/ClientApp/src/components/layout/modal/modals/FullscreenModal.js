import React, { useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../../actions/modal";
import CloseIcon from "../../../../assets/images/delete-white.png";

const FullscreenModal = ({ modal, removeModal }) => {
  const image = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      !image.current.contains(e.target) && removeModal();
    };

    image.current.addEventListener("contextmenu", e => {
      e.preventDefault();
    });
    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });

  return (
    <div className='modal-container'>
      <div id='fullscreenModal-container'>
        <div onClick={() => removeModal()} className='close-button'>
          <img src={CloseIcon} alt='' />
        </div>
        <img src={modal.data} ref={image} alt='' />
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  modal: state.modalReducer
});

export default connect(mapStateToProps, { removeModal })(FullscreenModal);

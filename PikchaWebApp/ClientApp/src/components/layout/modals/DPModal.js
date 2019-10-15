import React, { useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../actions/modal";
import CloseIcon from "../../../assets/images/delete-black.png";
import DeleteIcon from "../../../assets/images/delete-white.png";
import Cropper from "react-cropper";
import "cropperjs/dist/cropper.css";

const DPModal = ({ removeModal }) => {
  const [error, setError] = useState(false);
  const [preview, setPreview] = useState("");
  const [cropped, setCropped] = useState("");
  const modalContainer = useRef(null);
  const uploadButton = useRef(null);
  const cropper = useRef(null);

  useEffect(() => {
    // Detect Clicks outside of container
    document.addEventListener("mousedown", e => {
      if (
        modalContainer.current &&
        !modalContainer.current.contains(e.target)
      ) {
        removeModal();
      }
    });
    uploadButton.current.addEventListener("change", () => {
      const acceptedImageTypes = ["image/jpeg", "image/png"];
      const file = window.URL.createObjectURL(uploadButton.current.files[0]);
      if (acceptedImageTypes.includes(uploadButton.current.files[0]["type"])) {
        setPreview(file);
      } else {
        setError(true);
        setTimeout(() => {
          setError(false);
        }, 10000);
      }
    });
  });

  const cropImage = () => {
    setCropped(cropper.current.getCroppedCanvas().toDataURL("image/jpeg"));
  };

  return (
    <div className='modal-container'>
      <div id='DPModal-container' ref={modalContainer}>
        <div className='DPModal-header'>
          <div className='upload-title'>Update Profile Picture</div>
          <img onClick={removeModal} src={CloseIcon} alt='' />
        </div>
        {error && (
          <div className='upload-error'>
            <div className='error-message'>
              Error: Wrong file type. Please upload a .jpg or .png.
            </div>
            <img onClick={() => setError(false)} src={DeleteIcon} alt='' />
          </div>
        )}
        <form className='uploadDP-form'>
          <div className='form-buttons'>
            <label className='upload-button'>
              Upload <input type='file' ref={uploadButton} />
            </label>
            <div className='form-btn'>
              <input type='submit' value='Save' />
            </div>
          </div>
          <div className='image-preview'>
            <Cropper
              ref={cropper}
              src={preview}
              style={{ height: "100%", width: "100%" }}
              aspectRatio={1 / 1}
              guides={false}
              viewMode={1}
              dragMode={"move"}
              background={false}
              ready={cropImage}
              cropend={cropImage}
            />
          </div>
        </form>
      </div>
    </div>
  );
};

export default connect(
  null,
  { removeModal }
)(DPModal);

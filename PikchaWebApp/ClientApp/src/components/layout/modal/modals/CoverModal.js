import React, { useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../../actions/modal";
import { updateCoverPicture } from "../../../../actions/profile";
import CloseIcon from "../../../../assets/images/delete-white.png";
import DeleteIcon from "../../../../assets/images/delete-white.png";
import Cropper from "react-cropper";
import "cropperjs/dist/cropper.css";

const CoverModal = ({ removeModal, updateCoverPicture, profile }) => {
  const [error, setError] = useState(false);
  const [preview, setPreview] = useState(profile.artist.cover);
  const [cropped, setCropped] = useState("");
  const modalContainer = useRef(null);
  const uploadButton = useRef(null);
  const cropper = useRef(null);

  const { id } = profile.artist;

  useEffect(() => {
    const handleClick = e => {
      !modalContainer.current.contains(e.target) && removeModal();
    };

    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });

  useEffect(() => {
    uploadButton.current.addEventListener("change", () => {
      const acceptedImageTypes = ["image/jpeg", "image/png"];
      if (uploadButton.current.files && uploadButton.current.files[0]) {
        if (
          acceptedImageTypes.includes(uploadButton.current.files[0]["type"])
        ) {
          const reader = new FileReader();
          reader.readAsDataURL(uploadButton.current.files[0]);
          reader.onload = function(e) {
            setPreview(e.target.result);
          };
        } else {
          setError(true);
          setTimeout(() => {
            setError(false);
          }, 10000);
        }
      }
    });
  });

  const cropImage = () => {
    setCropped(cropper.current.getCroppedCanvas().toDataURL("image/jpeg"));
  };

  const onSubmit = e => {
    e.preventDefault();
    const coverContent = cropped;
    removeModal();
    updateCoverPicture({ id, coverContent });
  };

  return (
    <div className='modal-container'>
      <div id='coverModal-container' ref={modalContainer}>
        <div className='coverModal-header'>
          <div className='upload-title'>Update Cover Picture</div>
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
        <form className='uploadCover-form' onSubmit={onSubmit}>
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
              aspectRatio={4 / 1}
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

const mapStateToProps = state => ({
  profile: state.profileReducer
});

export default connect(mapStateToProps, { removeModal, updateCoverPicture })(
  CoverModal
);

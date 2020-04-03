import React, { Fragment, useEffect, useState, useRef } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { loadSignature, uploadImage } from "../../../../actions/account";
import DeleteIcon from "../../../../assets/images/delete-white.png";
import UploadIcon from "../../../../assets/images/upload-purple.png";
import { removeModal } from "../../../../actions/modal";

const UploadImageForm = ({
  loadSignature,
  account,
  formData,
  handleChange,
  setFormData,
  nextStep,
  uploadImage,
  removeModal
}) => {
  const previewImage = useRef(null);
  const [error, setError] = useState(false);
  const [preview, setPreview] = useState("");
  const [signatureContainer, setSignatureContainer] = useState({
    width: null,
    height: null
  });

  useEffect(() => {
    loadSignature();
  }, []);

  useEffect(() => {
    const handleResize = () => {
      createSignatureContainer(previewImage.current);
    };

    previewImage.current.onload = () => {
      handleResize();
      window.addEventListener("resize", handleResize);
    };

    return () => {
      window.removeEventListener("resize", handleResize);
    };
    // eslint-disable-next-line
  }, [preview]);

  // Load default signature when loadSignature() is done
  useEffect(() => {
    account.signature &&
      setFormData({
        ...formData,
        signature: account.signature.orgSig
      });
  }, [account.signature]);

  // Create signatue container to match the size of the image
  const createSignatureContainer = image => {
    let imageRatio = image.naturalWidth / image.naturalHeight;
    let cRatio = image.width / image.height;
    let targetWidth = 0;
    let targetHeight = 0;

    var test = imageRatio > cRatio;

    if (test) {
      targetWidth = image.width;
      targetHeight = targetWidth / imageRatio;
    } else {
      targetHeight = image.height;
      targetWidth = targetHeight * imageRatio;
    }

    setSignatureContainer({
      ...signatureContainer,
      height: targetHeight,
      width: targetWidth
    });
  };

  const next = () => {
    if (formData.imageFile === null) {
      setError("Please select an image to upload");
    } else {
      const formD = new FormData();
      Object.keys(formData).forEach(key => formD.append(key, formData[key]));
      uploadImage(formD);
      removeModal();
    }
  };

  const onChange = e => {
    const acceptedImageTypes = ["image/jpeg", "image/png"];
    if (e.target.files && e.target.files[0]) {
      if (acceptedImageTypes.includes(e.target.files[0]["type"])) {
        const reader = new FileReader();
        reader.readAsDataURL(e.target.files[0]);
        reader.onload = function(e) {
          setPreview(e.target.result);
        };
        setFormData({
          ...formData,
          imageFile: e.target.files[0]
        });
      } else {
        setError("Error: Wrong file type. Please upload a .jpg or .png");
        setTimeout(() => {
          setError(false);
        }, 10000);
      }
    }
  };

  const reset = () => {
    setFormData({
      ...formData,
      imageFile: null
    });
    setPreview("");
  };

  return (
    <div className='upload-image-container'>
      {error && (
        <div className='upload-error'>
          <div className='error-message'>{error}</div>
          <img onClick={() => setError(false)} src={DeleteIcon} alt='' />
        </div>
      )}
      <div className={`image-preview ${formData.imageFile && "active"}`}>
        <div className='image-container'>
          <img src={preview} alt='' ref={previewImage} />
          {formData.imageFile && (
            <div className='signature-wrapper'>
              <div
                className='signature-container'
                style={{
                  height: signatureContainer.height,
                  width: signatureContainer.width
                }}
              >
                {account.signature && <img src={formData.signature} alt='' />}
              </div>
            </div>
          )}
          {!formData.imageFile && (
            <div className='upload-icon'>
              <label htmlFor='inputFile'>
                <img src={UploadIcon} alt='' />
              </label>
              <input id='inputFile' type='file' onChange={onChange} />
            </div>
          )}
        </div>
      </div>
      {formData.imageFile ? (
        <Fragment>
          <div className='upload-options'>
            <div className='ratio-container'>
              <div className='option-title'>Aspect Ratio</div>
              <div className='supported-ratios-container'>
                We only support the following aspect ratios: 1:1, 4:3, 2:1.
                Please ensure what you have uploaded meets this requirement or
                else we cannot promise your image will fit the canvas properly.
              </div>
            </div>
            <div className='signature-options'>
              <div className='option-title'>Signature Colour</div>
              {account.signature && (
                <div className='signature-toggle'>
                  <input
                    type='radio'
                    id='orgSig'
                    name='signature'
                    value={account.signature.orgSig}
                    onChange={handleChange}
                    defaultChecked
                  />
                  <label htmlFor='orgSig'>
                    <div className='orgSig-button'></div>
                  </label>

                  <input
                    type='radio'
                    id='invSig'
                    name='signature'
                    value={account.signature.invSig}
                    onChange={handleChange}
                  />
                  <label htmlFor='invSig'>
                    <div className='invSig-button'></div>
                  </label>
                </div>
              )}
            </div>
          </div>

          <div className='form-navigation-container'>
            <div className='back-button' onClick={() => reset()}>
              Back
            </div>
            <div className='continue-button' onClick={() => next()}>
              Upload
            </div>
          </div>
        </Fragment>
      ) : (
        <label className='upload-button'>
          Upload <input type='file' onChange={onChange} />
        </label>
      )}
    </div>
  );
};

UploadImageForm.propTypes = {
  account: PropTypes.object.isRequired,
  formData: PropTypes.object.isRequired,
  handleChange: PropTypes.func.isRequired,
  setFormData: PropTypes.func.isRequired,
  nextStep: PropTypes.func.isRequired
};

export default connect(null, { loadSignature, removeModal, uploadImage })(
  UploadImageForm
);

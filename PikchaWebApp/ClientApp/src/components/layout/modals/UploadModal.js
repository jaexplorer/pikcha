import React, { useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../actions/modal";
import CloseIcon from "../../../assets/images/delete-black.png";
import DeleteIcon from "../../../assets/images/delete-white.png";

const UploadModal = ({ removeModal }) => {
  const [error, setError] = useState(false);
  const uploadButton = useRef(null);
  const [preview, setPreview] = useState("");
  const modalContainer = useRef(null);
  const [image, setImage] = useState({
    title: "",
    location: "",
    description: "",
    tags: "",
    price: ""
  });

  const { title, location, description, tags, price } = image;

  // Update Component State on change
  const onChange = e => setImage({ ...image, [e.target.name]: e.target.value });

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

  return (
    <div className='modal-container'>
      <div id='upload-modal-container' ref={modalContainer}>
        <div className='modal-header'>
          <div className='modal-title'>Upload an Image</div>
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
        <form className='upload-form'>
          <div className='form-section'>
            <div className='upload-image-container'>
              <div className='image-preview'>
                <img src={preview} alt='' />
              </div>
              <label className='upload-button'>
                Upload <input type='file' ref={uploadButton} />
              </label>
            </div>
          </div>

          <div className='form-section'>
            <div className='section-title'>Edit Details</div>
            <div className='split-details-container'>
              <div className='title'>
                <div className='input-wrapper'>
                  <div className='input-title'>Title</div>
                  <input
                    className='input-field'
                    type='text'
                    name='title'
                    value={title}
                    onChange={onChange}
                  />
                </div>
              </div>
              <div className='location'>
                <div className='input-wrapper'>
                  <div className='input-title'>Location</div>
                  <input
                    className='input-field'
                    type='text'
                    name='location'
                    value={location}
                    onChange={onChange}
                  />
                </div>
              </div>
            </div>

            <div className='description'>
              <div className='input-wrapper'>
                <div className='input-title'>Description</div>
                <textarea
                  maxLength='100'
                  className='input-field'
                  type='text'
                  name='description'
                  value={description}
                  onChange={onChange}
                />
              </div>
            </div>

            <div className='split-details-container'>
              <div className='tags'>
                <div className='input-wrapper'>
                  <div className='input-title'>Tags</div>
                  <input
                    className='input-field'
                    type='text'
                    name='tags'
                    value={tags}
                    onChange={onChange}
                  />
                </div>
              </div>
              <div className='price'>
                <div className='input-wrapper'>
                  <div className='input-title'>Price</div>
                  <input
                    className='input-field'
                    type='text'
                    name='price'
                    value={price}
                    onChange={onChange}
                  />
                </div>
              </div>
            </div>
          </div>

          <div className='form-btn'>
            <input type='submit' value='Post' />
          </div>
        </form>
      </div>
    </div>
  );
};

export default connect(
  null,
  { removeModal }
)(UploadModal);

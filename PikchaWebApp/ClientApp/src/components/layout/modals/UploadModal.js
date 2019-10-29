import React, { Fragment, useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../actions/modal";
import CloseIcon from "../../../assets/images/delete-white.png";
import DeleteIcon from "../../../assets/images/delete-white.png";
import { loadSignature, uploadImage } from "../../../actions/account";
import { setAlert } from "../../../actions/alert";

const UploadModal = ({
  account,
  removeModal,
  loadSignature,
  setAlert,
  uploadImage
}) => {
  const [error, setError] = useState(false);
  const uploadButton = useRef(null);
  const [preview, setPreview] = useState("");
  const modalContainer = useRef(null);
  const [tagInput, setTagInput] = useState("");
  const [image, setImage] = useState({
    title: "",
    location: "",
    description: "",
    price: "",
    tags: [],
    signature: "",
    imageFile: null
  });

  const {
    title,
    location,
    description,
    price,
    signature,
    tags,
    imageFile
  } = image;

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
      if (uploadButton.current.files && uploadButton.current.files[0]) {
        if (
          acceptedImageTypes.includes(uploadButton.current.files[0]["type"])
        ) {
          const reader = new FileReader();
          reader.readAsDataURL(uploadButton.current.files[0]);
          reader.onload = function(e) {
            setPreview(e.target.result);
          };
          setImage({ ...image, imageFile: uploadButton.current.files[0] });
        } else {
          setError(true);
          setTimeout(() => {
            setError(false);
          }, 10000);
        }
      }
    });
  });

  useEffect(() => {
    loadSignature();
  }, []);

  const onSubmit = e => {
    e.preventDefault();

    if (
      title === "" ||
      location === "" ||
      description === "" ||
      price === "" ||
      /(^[0-9]+$)/.test(price) === false ||
      tags.length === 0 ||
      (imageFile === null || imageFile.size < 5)
    ) {
      title === "" &&
        setAlert("Please supply a title for your image", "danger");
      location === "" &&
        setAlert("Please supply the location of the image", "danger");
      description === ""
        ? setAlert("Please supply a description of the image", "danger")
        : description.length > 380
        ? setAlert(
            "Please describe your image in under 380 characters",
            "danger"
          )
        : description.length < 120 &&
          setAlert(
            "Please describe your image in with atleast 120 characters",
            "danger"
          );
      price === ""
        ? setAlert(
            "Please set the amount you would like from this image",
            "danger"
          )
        : /(^[0-9]+$)/.test(price) === false &&
          setAlert("Please use digits only", "danger");
      tags.length === 0 &&
        setAlert("Please enter atleast one tag for this image", "danger");
      imageFile === null &&
        setAlert("Please select an image to upload", "danger");
      // : imageFile.size < 5000000 &&
      //   setAlert("Please select an image larger than 5MB", "danger");
    } else {
      const caption = description;
      //(Title [string], Caption [text], Location [string], ImageFile [file], Tags [list of strings], Signature [string], Price [Number])
      uploadImage({
        title,
        caption,
        location,
        imageFile,
        tags,
        signature,
        price
      });
      removeModal();
    }
  };

  const onTagBlur = () => {
    let txt = tagInput;
    txt = txt.replace(/[^a-zA-Z0-9\+\-\.\#]/g, "");
    txt !== "" && setImage({ ...image, tags: [...image.tags, txt] });
    setTagInput("");
  };

  const onKeyUp = e => {
    /(188|32)/.test(e.which) && onTagBlur();
  };

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
        <form className='upload-form' onSubmit={onSubmit}>
          <div className='form-section'>
            <div className='upload-image-container'>
              <div className='image-preview'>
                <div className='image-container'>
                  <img src={preview} alt='' />
                </div>
                {account.loadingSignature === false && (
                  <div className='signature-toggle'>
                    <input
                      type='radio'
                      id='orgSig'
                      name='signature'
                      value={account.signature.orgSig}
                      onChange={onChange}
                      defaultChecked
                    />
                    <label htmlFor='orgSig'>
                      <img src={account.signature.orgSig} alt='' />
                    </label>

                    <input
                      type='radio'
                      id='invSig'
                      name='signature'
                      value={account.signature.invSig}
                      onChange={onChange}
                    />
                    <label htmlFor='invSig'>
                      <img src={account.signature.invSig} alt='' />
                    </label>
                  </div>
                )}
              </div>
              <label className='upload-button'>
                Upload <input type='file' ref={uploadButton} />
              </label>
            </div>
          </div>

          <div className='form-section'>
            <div className='section-title'>Edit Image Details</div>
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
                    placeholder='Whats this image called?'
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
                    placeholder='Where was this picture taken?'
                  />
                </div>
              </div>
            </div>

            <div className='description'>
              <div className='input-wrapper'>
                <div className='input-title'>Description</div>
                <div className='input-container'>
                  <div className='info'>i</div>
                  <div className='info-text'>
                    Only the first 150 characters will appear in the gallery
                    popup
                  </div>
                  <textarea
                    className='input-field'
                    type='text'
                    name='description'
                    value={description}
                    onChange={onChange}
                    placeholder='Tell us about your image'
                  />
                </div>
                <div className='textarea-counter'>
                  {380 - description.length}
                </div>
              </div>
            </div>

            <div className='split-details-container'>
              <div className='tags'>
                <div className='input-wrapper'>
                  <div className='input-title'>Tags</div>
                  <div id='tags-container'>
                    {image.tags.map((tag, index) => (
                      <div
                        onClick={() => {
                          setImage({
                            ...image,
                            tags: image.tags.filter(aTag => aTag !== tag)
                          });
                        }}
                        className='tag'
                        key={index}
                      >
                        {tag}
                      </div>
                    ))}
                    <input
                      type='text'
                      id='tag-input'
                      name='tagInput'
                      value={tagInput}
                      onChange={e => setTagInput(e.target.value)}
                      onBlur={onTagBlur}
                      onKeyUp={e => onKeyUp(e)}
                      placeholder='Eg. blue, ocean, beach...'
                    />
                  </div>
                </div>
              </div>
              <div className='price'>
                <div className='input-wrapper'>
                  <div className='input-title'>Set your take</div>
                  <input
                    className='input-field'
                    type='text'
                    name='price'
                    value={price}
                    onChange={onChange}
                    placeholder='Your takings'
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

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(
  mapStateToProps,
  { removeModal, loadSignature, uploadImage, setAlert }
)(UploadModal);

import React, { useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../actions/modal";
import CloseIcon from "../../../assets/images/delete-white.png";
import SignatureCanvas from "react-signature-canvas";

const RoleChangeModal = ({ removeModal }) => {
  const [agreement, setAgreement] = useState("");
  const [signature, setSignature] = useState(null);

  const modalContainer = useRef(null);
  const sigPad = useRef(null);

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
  });

  const clearSigPad = () => {
    sigPad.current.clear();
    setSignature(null);
  };

  const saveSigPad = () => {
    setSignature({
      trimmedDataURL: sigPad.current.getTrimmedCanvas().toDataURL("image/png")
    });
  };

  return (
    <div className='modal-container'>
      <div id='role-change-modal-container' ref={modalContainer}>
        <div className='modal-header'>
          <div className='header'>
            <div className='modal-title'>Want To Be An Artist?</div>
            <img onClick={removeModal} src={CloseIcon} alt='' />
          </div>
          <div className='header-text'>
            It's a great way to earn money, launch your photography career
            <br />
            and have a chance to be in the top 100.
          </div>
        </div>
        {/* 
        <div className='form-subtext'>

        </div> */}

        <form className='role-change-form'>
          {/* <div className='form-section'>
            <div className='section-title'>Getting Paid</div>
            <div className='section-text'>
              We use Paypal to handle payments, please ensure this is setup
              properly.
            </div>
          </div> */}

          <div className='form-section'>
            <div className='section-title'>Your Signature</div>
            <div className='section-text'>
              Create your signature that will appear on your images. Please be
              careful, as this cannot be changed later.
            </div>
            <div className='signature-border'>
              <div className='signature-container'>
                <SignatureCanvas
                  ref={sigPad}
                  penColor='black'
                  minWidth={2}
                  maxWidth={4}
                  minDistance={0}
                  throttle={20}
                  // clearOnResize={false}
                  onEnd={saveSigPad}
                  canvasProps={{
                    className: "sigCanvas"
                  }}
                />
              </div>
            </div>
            <div onClick={clearSigPad} className='signature-clear'>
              CLEAR
            </div>
          </div>

          <div className='form-section'>
            <div className='section-title'>User Agreement</div>
            <div className='section-text'>
              When an image is sold through the website, the sale is between you
              and the customer - Pikcha acts as your agent in this process. In
              order to act as your agent, we need your explicit permission.
            </div>
            <div className='input-wrapper'>
              <input
                id='checkmark'
                type='checkbox'
                name='agreement'
                value={agreement}
                required
                // onChange={onChange}
              />
              <label htmlFor='checkmark' className='check'>
                <svg width='18px' height='18px' viewBox='0 0 18 18'>
                  <path d='M1,9 L1,3.5 C1,2 2,1 3.5,1 L14.5,1 C16,1 17,2 17,3.5 L17,14.5 C17,16 16,17 14.5,17 L3.5,17 C2,17 1,16 1,14.5 L1,9 Z'></path>
                  <polyline points='1 9 7 14 15 4'></polyline>
                </svg>
              </label>
              Yes, I agree to the Pikcha User Agreement
            </div>
          </div>

          <div className='form-btn'>
            <input type='submit' value='Become an Artist' />
          </div>
        </form>
      </div>
    </div>
  );
};

export default connect(
  null,
  { removeModal }
)(RoleChangeModal);

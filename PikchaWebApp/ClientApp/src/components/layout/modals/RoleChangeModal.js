import React, { useState, useRef, useEffect } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../actions/modal";
import CloseIcon from "../../../assets/images/delete-black.png";
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
        <img onClick={removeModal} src={CloseIcon} alt='' />
        <div className='modal-header'>
          <div className='modal-title'>Want To Be An Artist?</div>
          <div className='modal-subtext'>
            It's a great way to earn money, launch your photography career
            <br />
            and have a chance to be in the top 100.
          </div>
        </div>
        <form className='role-change-form'>
          <div className='form-section'>
            <div className='section-title'>Getting Paid</div>
            <div className='section-text'>
              We use Paypal to handle payments, please ensure this is setup
              properly.
            </div>
            {/* <div className='email'>
            <div className='input-wrapper'>
              <div className='input-title'>Email</div>
              <input
                className='input-field'
                type='email'
                name='email'
                value={email}
                onChange={onChange}
              />
            </div>
          </div> */}
          </div>

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
                className='checkbox'
                type='checkbox'
                name='agreement'
                value={agreement}
                required
                // onChange={onChange}
              />
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

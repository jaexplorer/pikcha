import React, { useState, useEffect, useRef } from "react";
import { connect } from "react-redux";
import { removeModal } from "../../../../actions/modal";
import CloseIcon from "../../../../assets/images/delete-white.png";
import Confirm from "./Confirm";
import Success from "./Success";
import UploadImageForm from "./UploadImageForm";
import EditImageForm from "./EditImageForm";

const UploadModal = ({ account, removeModal }) => {
  const modalContainer = useRef(null);
  const [step, setStep] = useState(1);
  const [formData, setFormData] = useState({
    title: "Testing Image",
    location: "Melbourne, Australia",
    caption:
      "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repudiandae aut excepturi eos rem. Mollitia, facere nam veritatis quisquam sunt vero commodi sint perspiciatis itaque aperiam magni cupiditate laudantium, reiciendis veniam praesentium. Aut quidem reiciendis, aliquam asperiores placeat impedit? Similique consectetur est at quos aperiam mollitia illum eum eos, maiores, voluptatem animi explicabo laboriosam alias harum placeat iusto ullam? Laborum iure dicta perspiciatis a animi quaerat voluptate suscipit! Culpa sit temporibus ipsam repellendus corporis nobis impedit veniam, placeat enim. Natus incidunt quisquam quia magnam et odio vero atque earum perferendis at! Delectus dignissimos ullam saepe harum consequuntur fuga amet dolorum, corrupti quae reiciendis hic alias quos sit unde, dolorem suscipit. At, incidunt ducimus sequi sit nihil illo ea non nam doloremque consequuntur repellat sed dolorem obcaecati facere eaque reiciendis aliquid? At explicabo, ut vel pariatur, dignissimos minima debitis rem deserunt numquam repudiandae tenetur quo. Harum reprehenderit et a doloremque beatae voluptatibus repudiandae perspiciatis nihil odio, eligendi iusto cumque perferendis sit molestiae! Temporibus rerum doloremque quo cum. Nulla inventore architecto porro perspiciatis.",
    price: "147",
    tags: ["blue"],
    signature: "",
    imageFile: null
  });

  useEffect(() => {
    const handleClick = e => {
      !modalContainer.current.contains(e.target) && removeModal();
    };

    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });

  const nextStep = () => {
    setStep(step + 1);
  };

  const prevStep = () => {
    setStep(step - 1);
  };

  // Update Component State on change
  const handleChange = e =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const displayStep = () => {
    switch (step) {
      case 1:
        return (
          <UploadImageForm
            account={account}
            nextStep={nextStep}
            setFormData={setFormData}
            handleChange={handleChange}
            formData={formData}
          />
        );
      case 2:
        return (
          <EditImageForm
            account={account}
            nextStep={nextStep}
            prevStep={prevStep}
            handleChange={handleChange}
            formData={formData}
          />
        );

      case 3:
        return (
          <Confirm
            nextStep={nextStep}
            prevStep={prevStep}
            formData={formData}
          />
        );

      case 4:
        return <Success />;
      default:
        return null;
    }
  };

  const onSubmit = e => {
    e.preventDefault();
  };

  return (
    <div className='modal-container'>
      <div id='upload-modal-container' ref={modalContainer}>
        <div className='modal-header'>
          <div className='modal-title'>
            {step === 1 && "Upload Image"}
            {step === 2 && "Edit Image"}
            {step === 3 && "Success"}
            {step === 4 && "Confirm"}
          </div>
          <img onClick={removeModal} src={CloseIcon} alt='' />
        </div>
        <form className='upload-form' onSubmit={onSubmit}>
          {displayStep()}
        </form>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(mapStateToProps, { removeModal })(UploadModal);

import React from "react";
import { connect } from "react-redux";
import Select from "react-select";
import { customDropdownCSS } from "../CustomDropdownCSS";
import {
  setSize,
  setMaterial,
  setFrame,
  setBorder,
  setFinish
} from "../../../actions/product";

const CustomiseProduct = ({
  setSize,
  setMaterial,
  setFrame,
  setBorder,
  setFinish
}) => {
  return (
    <div className='customiseProduct-container'>
      <div className='customiseProduct-header'>
        <div className='customiseProduct-title'>Customise</div>
        <div className='customiseProduct-number'>#11</div>
      </div>
      <div className='customiseProduct-dropdown-containers'>
        <div className='customise-dropdown-item'>
          <Select
            placeholder='Size'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            onChange={e => setSize(e.value)}
          />
        </div>

        <div className='break'></div>

        <div className='customise-dropdown-item'>
          <Select
            placeholder='Material'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            onChange={e => setMaterial(e.value)}
          />
        </div>

        <div className='break'></div>

        <div className='customise-dropdown-item'>
          <Select
            placeholder='Frame'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            onChange={e => setFrame(e.value)}
          />
        </div>

        <div className='break'></div>

        <div className='customise-dropdown-item'>
          <Select
            placeholder='Border'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            onChange={e => setBorder(e.value)}
          />
        </div>

        <div className='break'></div>

        <div className='customise-dropdown-item'>
          <Select
            placeholder='Finish'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            onChange={e => setFinish(e.value)}
          />
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  product: state.productReducer
});

export default connect(
  mapStateToProps,
  { setSize, setMaterial, setFrame, setBorder, setFinish }
)(CustomiseProduct);

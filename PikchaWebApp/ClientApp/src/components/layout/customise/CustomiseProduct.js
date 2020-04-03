import React from "react";
import Select from "react-select";
import { customDropdownCSS } from "../../common/CustomDropdownCSS";

const CustomiseProduct = () => {
  return (
    <div className='filter-container'>
      <div className='filter-header'>
        <div className='filter-title'>Customise</div>
        <div className='filter-number'>#11</div>
      </div>
      <div className='filter-dropdown-containers'>
        <div className='filter-dropdown-item'>
          <Select
            placeholder='Size'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            // onChange={e => setSize(e.value)}
          />
        </div>

        <div className='filter-dropdown-item'>
          <Select
            placeholder='Material'
            value={"big"}
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            // onChange={e => setMaterial(e.value)}
          />
        </div>

        <div className='filter-dropdown-item'>
          <Select
            placeholder='Frame'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            // onChange={e => setFrame(e.value)}
          />
        </div>

        <div className='filter-dropdown-item'>
          <Select
            placeholder='Border'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            // onChange={e => setBorder(e.value)}
          />
        </div>

        <div className='filter-dropdown-item'>
          <Select
            placeholder='Finish'
            styles={customDropdownCSS}
            options={[
              { value: "big", label: "big" },
              { value: "medium", label: "medium" },
              { value: "small", label: "small" }
            ]}
            // onChange={e => setFinish(e.value)}
          />
        </div>
      </div>
    </div>
  );
};

export default CustomiseProduct;

import React, { Fragment } from "react";
import CustomiseProduct from "../layout/customise/CustomiseProduct";
import CustomiseSummary from "../layout/customise/CustomiseSummary";

const Customise = ({ location }) => {
  return (
    <Fragment>
      <CustomiseProduct />
      <CustomiseSummary photo={location.state.photo} />
    </Fragment>
  );
};

export default Customise;

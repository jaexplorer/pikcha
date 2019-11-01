import React, { useEffect, Fragment } from "react";
import SideBar from "../layout/Sidebar";
import PikchaItem from "../layout/pikcha100/PikchaItem";
import MainComponent from "../MainComponent";
import Header from "../layout/Header";
import { connect } from "react-redux";
import { getPikcha100 } from "../../actions/top100";

const Pikcha100 = ({ getPikcha100, top100 }) => {
  useEffect(() => {
    getPikcha100();
  }, []);

  return (
    <Fragment>
      <SideBar />
      <Header subtitle='Top 100' title='Pikcha 100' />
      {top100.pikchaloading ? (
        <h2 className='loading'>Loading...</h2>
      ) : (
        <MainComponent container='pikcha100-container'>
          {top100.pikcha100.map((pikchaItem, index) => (
            <PikchaItem pikchaItem={pikchaItem} key={index} rank={index + 1} />
          ))}
        </MainComponent>
      )}
    </Fragment>
  );
};

const mapStateToProps = state => ({
  top100: state.top100Reducer
});

export default connect(
  mapStateToProps,
  { getPikcha100 }
)(Pikcha100);

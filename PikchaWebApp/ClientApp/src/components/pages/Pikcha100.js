import React, { Fragment, useEffect } from "react";
import { connect } from "react-redux";
import { getPikcha100, resetPikcha100 } from "../../actions/pikcha100";
import InfiniteScroll from "react-infinite-scroll-component";
import PikchaRow from "../layout/pikcha100/PikchaRow";
import Loader from "../Loader";

const Pikcha100 = ({ pikcha100, getPikcha100, resetPikcha100 }) => {
  useEffect(() => {
    getPikcha100(pikcha100.count, pikcha100.start);
    return () => resetPikcha100();
  }, []);

  return (
    <Fragment>
      <div className='page-title'>Pikcha 100</div>
      <div className='pikcha100-container'>
        <InfiniteScroll
          dataLength={pikcha100.top100.length}
          next={() => getPikcha100(pikcha100.count, pikcha100.start)}
          hasMore={pikcha100.hasMore}
          loader={<Loader />}
          endMessage={
            <h4 className='end-message'>Yay! You have seen them all</h4>
          }
        >
          {pikcha100.top100.map((row, index) => (
            <PikchaRow key={index + 1} rank={index + 1} row={row} />
          ))}
        </InfiniteScroll>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  pikcha100: state.pikcha100Reducer
});

export default connect(
  mapStateToProps,
  { getPikcha100, resetPikcha100 }
)(Pikcha100);

import React, { Fragment, useEffect } from "react";
import { connect } from "react-redux";
import Select from "react-select";
import { getArtist100, resetArtist100 } from "../../actions/artist100";
import InfiniteScroll from "react-infinite-scroll-component";
import ArtistRow from "../layout/artist100/ArtistRow";
import Loader from "../common/Loader";
import { customDropdownCSS } from "../common/CustomDropdownCSS";

const Artist100 = ({ artist100, getArtist100, resetArtist100 }) => {
  useEffect(() => {
    getArtist100(artist100.count, artist100.start);
    return () => resetArtist100();
  }, []);

  return (
    <Fragment>
      <div className='page-title'>Artist 100</div>
      <div className='filter-container'>
        <div className='filter-header'>
          <div className='filter-title'>Refine Search</div>
        </div>
        <div className='filter-dropdown-containers'>
          <div className='filter-dropdown-item'>
            <Select
              placeholder='Primary Filter'
              styles={customDropdownCSS}
              options={[
                { value: "Views", label: "Views" },
                { value: "Total Sales", label: "Total Sales" },
                { value: "Total Price", label: "Total Price" }
              ]}
              // onChange={e => setSize(e.value)}
            />
          </div>

          <div className='filter-dropdown-item'>
            <Select
              placeholder='Time'
              value={"big"}
              styles={customDropdownCSS}
              options={[{ value: "All Time", label: "All Time" }]}
              // onChange={e => setMaterial(e.value)}
            />
          </div>

          <div className='filter-dropdown-item'>
            <Select
              placeholder='Location'
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
              placeholder='Nationality'
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
              placeholder='Theme'
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
      <div className='artist100-container'>
        <InfiniteScroll
          dataLength={artist100.top100.length}
          next={() => getArtist100(artist100.count, artist100.start)}
          hasMore={artist100.hasMore}
          loader={<Loader />}
          endMessage={
            <h4 className='end-message'>Yay! You have seen them all</h4>
          }
        >
          {artist100.top100.map((row, index) => (
            <ArtistRow key={index + 1} rank={index + 1} row={row} />
          ))}
        </InfiniteScroll>
      </div>
    </Fragment>
  );
};

const mapStateToProps = state => ({
  artist100: state.artist100Reducer
});

export default connect(mapStateToProps, { getArtist100, resetArtist100 })(
  Artist100
);

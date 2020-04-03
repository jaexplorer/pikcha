import React, { useEffect, useRef } from "react";
import { connect } from "react-redux";
import { closeDropdown } from "../../../../actions/nav";

const FollowingDropdown = ({ account, closeDropdown }) => {
  const dropdown = useRef(null);

  useEffect(() => {
    const handleClick = e => {
      e.stopPropagation();
      if (!dropdown.current.contains(e.target)) {
        dropdown.current.classList.add("inactive");

        setTimeout(() => closeDropdown(), 600);
      }
    };

    document.addEventListener("mousedown", handleClick);

    return () => document.removeEventListener("mousedown", handleClick);
  });
  return (
    <div className='following-dropdown' ref={dropdown}>
      <div className='following-title'>
        {account.user.following.length
          ? "Following"
          : "Followed Artists will appear here"}
      </div>
      <div className='following-container'>
        <div className='following-wrapper'>
          {account.user.following.map(followed => (
            <div className='followed'>
              <img src={followed.avatar} alt='' />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = state => ({
  account: state.accountReducer
});

export default connect(mapStateToProps, { closeDropdown })(FollowingDropdown);

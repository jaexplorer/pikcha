import React from "react";
import PropTypes from "prop-types";

const SuggestionItem = ({ suggestion, handleChange }) => {
  return (
    <div onClick={() => handleChange(suggestion)} className='suggestionItem'>
      {suggestion}
    </div>
  );
};

SuggestionItem.propTypes = {
  suggestion: PropTypes.string.isRequired,
  handleChange: PropTypes.func.isRequired
};

export default SuggestionItem;

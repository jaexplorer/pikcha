import React from "react";
import PropTypes from "prop-types";
import SuggestionItem from "./SuggestionItem";

const SuggestionList = ({ handleChange, suggestions }) => {
  return (
    <div className='suggestionList'>
      {suggestions.map(suggestion => (
        <SuggestionItem
          key={suggestion}
          handleChange={handleChange}
          suggestion={suggestion}
        />
      ))}
    </div>
  );
};

SuggestionList.propTypes = {
  handleChange: PropTypes.func.isRequired,
  suggestions: PropTypes.array.isRequired
};

export default SuggestionList;

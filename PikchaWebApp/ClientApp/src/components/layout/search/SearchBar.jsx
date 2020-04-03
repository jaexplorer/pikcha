import React, { useState, useEffect, useRef } from "react";
import history from "../../routes/history";
import SuggestionList from "./SuggestionList";
import { connect } from "react-redux";
import { getSuggestions, resetSuggestions } from "../../../actions/search";
import { useDebounce } from "../../../hooks/useDebounce";

const SearchBar = ({ search, getSuggestions, resetSuggestions }) => {
  const [searchQuery, setSearchQuery] = useState("");
  const debouncedSearchTerm = useDebounce(searchQuery, 600);
  const form = useRef(null);
  const searchContainer = useRef(null);

  useEffect(() => {
    debouncedSearchTerm
      ? getSuggestions(debouncedSearchTerm)
      : resetSuggestions();
  }, [debouncedSearchTerm]);

  useEffect(() => {
    const handleClick = e => {
      if (!searchContainer.current.contains(e.target)) {
        setSearchQuery("");
        resetSuggestions();
      }
    };
    document.addEventListener("mousedown", handleClick);
    return () => document.removeEventListener("mousedown", handleClick);
  }, []);

  const onSubmit = e => {
    e.preventDefault();
    setSearchQuery("");
    resetSuggestions();
    history.push(`/search/${searchQuery}`);
  };

  return (
    <div className='search-container' ref={searchContainer}>
      <form className='search-bar' onSubmit={onSubmit} ref={form}>
        <input
          type='text'
          value={searchQuery}
          onChange={e => setSearchQuery(e.target.value)}
        />
      </form>
      {search.suggestions && searchQuery && (
        <SuggestionList
          suggestions={search.suggestions}
          handleChange={suggestion => {
            setSearchQuery(suggestion);
            form.current.dispatchEvent(new Event("submit"));
          }}
        />
      )}
    </div>
  );
};

const mapStateToProps = state => ({
  search: state.searchReducer
});

export default connect(mapStateToProps, { getSuggestions, resetSuggestions })(
  SearchBar
);

export const customDropdownCSS = {
  control: (base, state) => ({
    ...base,
    width: "100%",
    height: "5rem",
    background: "#ebebeb",
    borderRadius: "10px",
    border: state.isFocused ? "none" : "none",
    borderColor: state.isFocused ? "none" : "none",
    boxShadow: state.isFocused ? null : null,
    "&:hover": {
      borderColor: state.isFocused ? "none" : "none"
    },
    cursor: "pointer"
  }),
  input: () => ({
    height: "5rem",
    fontSize: "1.2rem",
    display: "flex",
    alignItems: "center",
    paddingLeft: "1.5rem"
  }),
  placeholder: () => ({
    color: "#1a1a1a",
    fontSize: "1.2rem",
    paddingLeft: "1.5rem",
    position: "absolute"
  }),
  indicatorSeparator: base => ({
    ...base,
    display: "none"
  }),
  dropdownIndicator: base => ({
    ...base,
    marginRight: "0.8rem"
  }),
  menu: base => ({
    ...base,
    borderRadius: "10px",
    overflow: "hidden",
    boxShadow: "5px 15px 30px -10px rgba(0, 0, 0, 0.5)",
    marginTop: "1rem",
    wordWrap: "break-word"
  }),
  menuList: base => ({
    ...base,
    padding: 0
  }),
  option: (base, state) => ({
    ...base,
    background: "#f4f7fb",
    color: state.isFocused ? "mediumPurple" : "#1a1a1a",
    fontSize: "1.2rem",
    height: "5rem",
    opacity: 0.6,
    display: "flex",
    alignItems: "center",
    paddingLeft: "1.5rem",
    cursor: "pointer"
  }),
  noOptionsMessage: (base, state) => ({
    ...base,
    background: "#f4f7fb",
    color: state.isFocused ? "#1a1a1a" : "#1a1a1a",
    fontSize: "1.2rem",
    height: "5rem",
    opacity: 0.6,
    display: "flex",
    alignItems: "center",
    paddingLeft: "1.5rem"
  }),
  singleValue: (base, state) => ({
    ...base,
    color: "#1a1a1a",
    height: "5rem",
    fontSize: "1.2rem",
    display: "flex",
    alignItems: "center",
    paddingLeft: "1.5rem",
    paddingLeft: "1.5rem"
  })
};

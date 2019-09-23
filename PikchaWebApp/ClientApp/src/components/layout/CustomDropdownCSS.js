export const customDropdownCSS = {
  control: (base, state) => ({
    ...base,
    width: "9rem",
    height: "3rem",
    background: "#1a1a1a",
    borderRadius: state.isFocused ? "0" : "0",
    borderColor: state.isFocused ? "none" : "none",
    boxShadow: state.isFocused ? null : null,
    "&:hover": {
      borderColor: state.isFocused ? "none" : "none"
    },
    cursor: "pointer"
  }),
  placeholder: (base, state) => ({
    ...base,
    color: "white",
    fontSize: "12pt",
    textTransform: "uppercase",
    paddingLeft: "1rem"
  }),
  indicatorSeparator: (base, state) => ({
    ...base,
    display: "none"
  }),
  dropdownIndicator: (base, state) => ({
    ...base,
    marginRight: "0.8rem",
    transform: state.isFocused ? "rotate(180deg)" : "rotate(0deg)"
  }),

  menu: base => ({
    ...base,
    borderRadius: 0,
    marginTop: 0,
    textAlign: "left",
    wordWrap: "break-word"
  }),
  menuList: base => ({
    ...base,
    padding: 0
  }),
  option: (base, state) => ({
    ...base,
    background: "#1a1a1a",
    color: state.isFocused ? "mediumPurple" : "white",
    fontSize: "12pt",
    textTransform: "uppercase",
    padding: "1rem",
    cursor: "pointer",
    paddingLeft: "1.5rem"
  }),
  singleValue: (base, state) => ({
    ...base,
    color: "white",
    fontSize: "12pt",
    textTransform: "uppercase",
    paddingLeft: "1rem"
  })
};

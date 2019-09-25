export const customDropdownCSS = {
  control: (base, state) => ({
    ...base,
    width: "100%",
    height: "4rem",
    background: "#1a1a1a",
    borderRadius: state.isFocused ? "0" : "0",
    borderColor: state.isFocused ? "none" : "none",
    boxShadow: state.isFocused ? null : null,
    "&:hover": {
      borderColor: state.isFocused ? "none" : "none"
    },
    cursor: "pointer"
  }),
  input: base => ({
    position: "absolute"
  }),
  placeholder: () => ({
    color: "white",
    fontSize: "1.2rem",
    textTransform: "uppercase",
    paddingLeft: "1rem",
    position: "relative"
  }),
  indicatorSeparator: base => ({
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
    fontSize: "1.2rem",
    textTransform: "uppercase",
    padding: "1rem",
    cursor: "pointer",
    paddingLeft: "1.5rem"
  }),
  singleValue: (base, state) => ({
    ...base,
    color: "white",
    fontSize: "1.2rem",
    textTransform: "uppercase",
    paddingLeft: "1rem"
  })
};

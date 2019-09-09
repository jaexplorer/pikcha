import React from "react";
import { connect } from "react-redux";

const Alerts = ({ alerts }) => {
  return (
    alerts.length > 0 &&
    alerts.map(alert => <div key={alert.id}>{alert.msg}</div>)
  );
};

const mapStateToProps = state => ({
  alerts: state.alertReducer
});

export default connect(mapStateToProps)(Alerts);

import React from "react";
import { connect } from "react-redux";

const Alerts = ({ alerts }) => {
  return (
    alerts.length > 0 && (
      <div className='alert-container'>
        {alerts.map(alert => (
          <div className={"alert " + alert.type} key={alert.id}>
            {alert.msg}
          </div>
        ))}
      </div>
    )
  );
};

const mapStateToProps = state => ({
  alerts: state.alertReducer
});

export default connect(mapStateToProps)(Alerts);

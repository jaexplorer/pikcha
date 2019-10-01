import React, { useState, useEffect } from "react";
import Logo from "assets/images/logo.png";

const ComingSoon = () => {
  const second = 1000,
    minute = second * 60,
    hour = minute * 60,
    day = hour * 24;

  const [days, setDays] = useState("");
  const [hours, setHours] = useState("");
  const [minutes, setMinutes] = useState("");
  const [seconds, setSeconds] = useState("");

  useEffect(() => {
    let countDown = new Date("Oct 1, 2019 00:00:00").getTime();
    setInterval(() => {
      let now = new Date().getTime(),
        distance = countDown - now;

      setDays(Math.floor(distance / day));
      setHours(Math.floor((distance % day) / hour));
      setMinutes(Math.floor((distance % hour) / minute));
      setSeconds(Math.floor((distance % minute) / second));
    }, second);
  });

  return (
    <div className='container'>
      <img src={Logo} />
      <h1 id='head'>Coming Soon...</h1>
      <ul>
        <li>
          <span id='days'>{days} </span>Days
        </li>
        <li>
          <span id='hours'>{hours} </span>Hours
        </li>
        <li>
          <span id='minutes'>{minutes} </span>Minutes
        </li>
        <li>
          <span id='seconds'>{seconds} </span>Seconds
        </li>
      </ul>
    </div>
  );
};

export default ComingSoon;

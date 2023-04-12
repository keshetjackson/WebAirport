import React from 'react';
import Logs from '../Logs';
import Legs from '../Legs';
import './Home.css';

const Home = () => {
  return (
    <div className="home-container">
      <div className="logs-container">
        <Logs />
      </div>
      <div className="legs-container">
        <Legs/>
      </div>
    </div>
  );
};

export default Home;

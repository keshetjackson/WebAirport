import React from 'react';
import Logs from './Components/Logs';
import Legs from './Components/Legs';

const Home = () => {
  return (
    <div>
      <h1>Home</h1>
      <div className="logs-legs-container">
        <Logs />
        <Legs />
      </div>
    </div>
  );
};

export default Home;

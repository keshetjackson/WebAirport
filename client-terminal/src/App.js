// src/App.js
import React from 'react';
import './App.css';
import FlightData from './Components/FlightData';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <FlightData />
      </header>
    </div>
  );
}

export default App;
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const FlightData = () => {
  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:7275/api/Flights');
        setData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    // Fetch data initially
    fetchData();

    // Set up a timer to fetch data every 500 milliseconds
    const timer = setInterval(() => {
      fetchData();
    }, 500);

    // Clean up the timer when the component is unmounted
    return () => {
      clearInterval(timer);
    };
  }, []);

  return (
    <div>
      <h1>Flight Data</h1>
      {data ? (
        <pre>{JSON.stringify(data, null, 2)}</pre>
      ) : (
        <p>Loading data...</p>
      )}
    </div>
  );
};

export default FlightData;
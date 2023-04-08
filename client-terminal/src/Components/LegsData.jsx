// FlightData.js

import React, { useState, useEffect } from 'react';
import api from '../Services/api';
import signalRService from '../Services/signalRService';

const LegsData = () => {
  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get('api/Airport/Legs');
        setData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    // Fetch data initially
    fetchData();

    signalRService.on("LegUpdated", fetchData);

    // Clean up the event listener when the component is unmounted
    return () => {
      signalRService.off("LegUpdated", fetchData);
    };
  }, []);

  return (
    <div>
      <h1>Legs Data</h1>
      {data ? (
        <pre>{JSON.stringify(data, null, 2)}</pre>
      ) : (
        <p>Loading real time data...</p>
      )}
    </div>
  );
};

export default LegsData;

import React, { useState, useEffect } from 'react';
import api from '../Services/api';
import signalRService from '../Services/signalRService';
import LegCard from './LegCard/LegCard';

const Legs = () => {
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

    fetchData();

    signalRService.on("LegUpdated", fetchData);

    return () => {
      signalRService.off("LegUpdated", fetchData);
    };
  }, []);

  const expandAll =() => {
    
  }

  return (
    <div>
      <h1>Legs</h1>
      
      {data ? (
        <div className='leg-card-container'>
       { data.map((leg) => ( <LegCard key={leg.id} leg = {leg}/>
       ))}
        </div>
      ) : (
        <p>loading real time data...</p>
      )}
      </div>
    
  );
};

export default Legs;

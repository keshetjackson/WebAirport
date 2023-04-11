import React, { useState, useEffect } from 'react';
import api from '../Services/api';
import signalRService from '../Services/signalRService';
import LogCard from './LogCard/LogCard';

const Logs = () => {

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const formatter = new Intl.DateTimeFormat('en', {
      year: 'numeric',
      month: 'short',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    });
    return formatter.format(date);
  };

  const [data, setData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get('api/Airport/Logs');
        setData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();

    signalRService.on("LogAdded", fetchData);

    return () => {
      signalRService.off("LogAdded", fetchData);
    };
  }, []);

  return (
    <div>
      <h1>Logs</h1>
      {data ? (
        <div className="log-card-container">
          {data.map((log) => (
            <LogCard key={log.id} log={log} formatDate={formatDate} />
          ))}
        </div>
      ) : (
        <p>Loading real-time logs...</p>
      )}
    </div>
  );
};

export default Logs;

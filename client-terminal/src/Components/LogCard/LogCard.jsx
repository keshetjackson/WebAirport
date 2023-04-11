import React, { useState } from 'react';
import './LogCard.css'

const LogCard = ({ log, formatDate }) => {
  const [expanded, setExpanded] = useState(false);

  const toggleExpanded = () => {
    setExpanded(!expanded);
  };

  return (
    <div className={`log-card ${expanded ? 'expanded' : ''}`} onClick={toggleExpanded}>
      <div>{log.flightId ? `Flight ${log.flightId} has entered leg number ${log.legId}` : 'The flight has left the leg'}</div>
      {expanded && (
        <>
          <div>Log ID: {log.id}</div>
          <div>Leg ID: {log.legId}</div>
          <div>At: {formatDate(log.eventTime)}</div>
        </>
      )}
    </div>
  );
};

export default LogCard;

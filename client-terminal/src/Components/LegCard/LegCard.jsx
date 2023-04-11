import React, { useState } from 'react';
import './LegCard.css';

const LegCard = ({ leg }) => {
    const [expanded, setExpanded] = useState(false);
  
    const handleExpandClick = () => {
      setExpanded(!expanded);
    };
  
    return (
      <div className={`leg-card ${leg.flightId ? 'taken' : 'empty'}`} onClick={handleExpandClick}>
        <div className="leg-card__content">
          <h2>Leg number :{leg.number}</h2>        
        </div>
        {expanded && (
          <div className="leg-card__details">
            {/* Add all the details you want to display when the card is expanded */}
            <h3>Flight ID: {leg.flightId || 'empty'}</h3> 
            <h3>Next Leg: {leg.nextLeg || 'empty'}</h3> 
          </div>
        )}
      </div>
    );
  };
  export default LegCard;
  
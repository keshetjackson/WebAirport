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
          <h4>Leg number :{leg.number}</h4>        
        </div>
        {expanded && (
          <div className="leg-card__details">
            {/* Add all the details you want to display when the card is expanded */}
            <h5>Flight ID: {leg.flightId || 'empty'}</h5> 
            <h5>Next Leg: {leg.nextLeg || 'empty'}</h5> 
          </div>
        )}
      </div>
    );
  };
  export default LegCard;
  
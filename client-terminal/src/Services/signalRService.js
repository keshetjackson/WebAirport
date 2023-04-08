import * as signalR from '@microsoft/signalr';
import api from './api';

const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7275/airportHub")
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

  connection.start()
  .then(() => {
    console.log("SignalR connection established");
    console.log(`SignalR connection state is ${connection.state}`);
  })
  .catch((err) => console.error(err));

function registerEventListeners() {
  connection.on("LegUpdated", () => {
    api.get("api/Airport/Legs").then((Response) => {
      console.log(Response.data);
    });  
  })

  connection.on("LogAdded", () => {
    api.get("api/Airport").then((Response) => {
      console.log(Response.data);
    });  
  });
  

}

export default connection;
export { registerEventListeners };
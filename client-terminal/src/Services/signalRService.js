import * as signalR from '@microsoft/signalr';

function App() {
    const connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7275/airportHub')
    .configureLogging(signalR.LogLevel.Information)
    .build()
}

export default connection
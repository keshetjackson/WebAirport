
# WebAirport
Flight Control & Remote Flight Manager

![image](https://user-images.githubusercontent.com/98225513/233122787-3ef21f67-e831-4b8d-aeec-0902e72a3e2b.png)


WebAirport is a real-time flight control system simulator that manages the activity of an airport, including remote control of aircraft.

## Features

The project Features 3 parts : 
- Simulator - generate random flights and send it to the server using http post request.
- Server - 
  - logic - so the logic of the application is basiclly a data structure with its own logic for specific nodes. this data structure is automatic, meaning whenever you pass a value (flight) to the first node (leg), each node will handle all the neccessery things like:
     - checking if the next node is free.
     - wait a certain amount of time.
     - pass the value to the next node.
     - save to database.
     - have a unique logic
     - ![image](https://user-images.githubusercontent.com/98225513/233123075-832f220a-4e44-4bd6-a112-33b3172d5320.png)

     
   - this logic was designed using several design patterns:
     - Singletone - each node is created only once thru the          application life time and each node is threadsafe.
     -  States - each node is checking the state of the next node to      decide if it can pass the value or wait.
     -  factory pattern : nodes (legs) are creating thru a generic      singletone thread safe leg factory to enable global access to the      legs and keeping them unique and thread safe.
     -  Repository - the repository of the application is using new      instance of the datacontext everytime with the datacontext factory      in order to keep the context thread safe and clean.
     - Observer - is used to pass real time updates from the server to the client whenever there is a change in a leg/ log is added.
     - Dependency Injection - Dependency Injection is used throught the application to inject to following Dependencies : 
      - Repository
      - Service 
      - Data Context
    

   - Service - the service of the application is rather small, it is being used only to pass a flight from the constructor to the data structure, it checks if the fligth is arrival or departure and pass it on to the appropiate node, from there the data structure is completly automatic and self managed.

   - Exception handling - i used a global extension handling throught a middleware.

   - Asynchronous thinking for handling flights and events in parallel.
- Gui - implemented using react, shows real time legs state and info on one side of the screen and on the other theres a list of logs for any activity inside the airport.
## Entities
- Flight:
```
 public class Flight
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public bool IsDeparture { get; set; }
        public string Company { get; set; }
        public bool IsCritical { get; set; }

    }
```
- Leg: 
```
public class LegModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int? FlightId { get; set; }
        public virtual Flight? Flight { get; set; }
        public LegType NextLeg { get; set; }
    }
```
- Logger
```
public class Logger
    {
        public int Id { get; set; }
        public int? FlightId { get; set; } 
        public virtual Flight? Flight { get; set; }
        public int? LegId { get; set; } 
        public virtual LegModel? Leg { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public bool IsEntering { get; set; }

    }
```


##  Tests

I added some basic unit test for the server. 

To run tests, open the cmd in the server directory and run the following command

```bash
  dotnet test
```


## Run
   - you might need to connect it to sql server database since im using the secret in produciton.
  
to run the project you will need first run the server and the client.
- server - to run the server you could build the server project and run it in vs community/ open the cmd in the server directory and run the following command: 

```bash
  dotnet run
```

- client - to run the gui you need to open the cmd in the client-terminal directory and use the following command : 

```bash
  npm start
```
after the server is running you could run the simulator the same way you runned the server. then you could see the project in action throught the gui.
## Pictures
![image](https://user-images.githubusercontent.com/98225513/233122136-231cd47e-7bcd-454a-8607-711e82d19258.png)
the cards are expandables : 
![image](https://user-images.githubusercontent.com/98225513/233122498-22805679-0c4d-481b-a653-d27a0eca8b06.png)

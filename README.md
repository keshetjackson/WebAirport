# WebAirport
Flight Control & Remote Flight Manager

WebAirport is a real-time flight control system simulator that manages the activity of an airport, including remote control of aircraft.

## Table of Contents
- [Features](#features)
- [System Architecture](#system-architecture)
- [Entities](#entities)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features
- Simulator that generates incoming flights approaching point 1 (See diagram).
- The system manages flights on the "route":
  - A maximum of 4 aircraft in the airport at a time.
  - Possible landing route: From point 1 to points 2 and 3 in order, then to point 4 for the physical landing.
  - Transfer to parking: Point 5 (transfer) and clearing at points 6 or 7. If a position is occupied, the aircraft will wait at point 5.
- System structure: Simulator, Logic, Entities, Server, GUI, and Log.
- Asynchronous thinking for handling flights, aircraft, and events in parallel.

## System Architecture
- Simulator: Handles the simulation of aircrafts, can work in a plug & play format.
- Logic: The functionality of the airport, including the timing mechanism and the array of flights in the air and on the ground.
- Entities: The minimal entities are Flight and Leg.
- Server: The central part of the system that includes the control tower's "brain" and the data structure of all active flights.
- GUI: Basic GUI that displays updated lists reflecting the status of all "legs" and their active flights.
- Log: Records all activity in a text file, including flight data (can also write to a database).

## Entities
- Flight: {number, passengers count, IsCritical, Brand, CurrentLeg, Landing/Departure}
- Leg: {Number, Capacity, Landing/Departure/Both, CrossingTime (seconds)}

## Installation
1. Clone this repository: `git clone https://github.com/keshetjackson/WebAirport.git`
2. Navigate to the project directory: `cd WebAirport`
3. Install the required packages: `dotnet restore`

## Usage
1. Run the application: `dotnet run`
2. Interact with the GUI to manage the flights and control the airport activities.

## Contributing
1. Fork the repository.
2. Create a new branch: `git checkout -b new-feature`
3. Commit your changes: `git commit -m "Add a new feature"`
4. Push to the branch: `git push origin new-feature`
5. Submit a pull request.

## License
This project is licensed under the MIT License.
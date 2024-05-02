function updateFlightBoard() {
    //console.log("begin departureboard update")
    fetch('/api/departures')
        .then(response => response.json())
        .then(flights => {
            const flightBoard = document.getElementById('flightBoard');
            flightBoard.innerHTML = ''; // Clear existing flight information

            flights.forEach(flight => {
                let row = flightBoard.insertRow(-1); // Append row to the end of the table body

                let cell1 = row.insertCell(0);
                cell1.textContent = flight.flightNumber;

                let cell2 = row.insertCell(1);
                cell2.textContent = `${flight.arrivalAirport} (${flight.arrivalAirportCode})`;

                let cell3 = row.insertCell(2);
                cell3.textContent = flight.departureTime;

                let cell4 = row.insertCell(3);
                cell4.textContent = flight.gate === 0 ? "N/A" : flight.gate;

                let cell5 = row.insertCell(4);
                cell5.textContent = flight.status;
            });
        })
   //console.log("end departureboard update")
}

function updateArrivalsBoard() {
    //console.log("begin arrivalboard update")
    fetch('/api/arrivals')
        .then(response => response.json())
        .then(flights => {
            const flightBoard = document.getElementById('arrivalsBoard');
            flightBoard.innerHTML = ''; // Clear existing flight information

            flights.forEach(flight => {
                let row = flightBoard.insertRow(-1); // Append row to the end of the table body

                let cell1 = row.insertCell(0);
                cell1.textContent = flight.flightNumber;

                let cell2 = row.insertCell(1);
                cell2.textContent = `${flight.departureAirport} (${flight.departureAirportCode})`;

                let cell3 = row.insertCell(2);
                cell3.textContent = flight.arrivalTime;

                let cell4 = row.insertCell(3);
                cell4.textContent = flight.gate === 0 ? "N/A" : flight.gate;

                let cell5 = row.insertCell(4);
                cell5.textContent = flight.status;
            });
        })
    //console.log("end arrivalboard update")
}
function updateOperationsBoard() {
    fetch('/api/operations')
        .then(response => response.json())
        .then(gates => {
            const operationsBoard = document.getElementById('operationsBoard');
            operationsBoard.innerHTML = ''; // Clear existing flight information

            gates.forEach(gate => {
                let row = operationsBoard.insertRow(-1); // Append row to the end of the table body

                let cell1 = row.insertCell(0);
                cell1.textContent = gate.gateNumber;

                let cell2 = row.insertCell(1);
                cell2.textContent = gate.inUse ? "Yes" : "No";

                let cell3 = row.insertCell(2);
                if (gate.flight != null) {
                    if (gate.flight.departureAirport == "Manchester") {
                        cell3.textContent = "Departure";
                    }
                    else {
                        cell3.textContent = "Arrival";
                    }
                }

                let cell4 = row.insertCell(3);
                if (gate.flight != null) {
                    cell4.textContent = gate.flight.flightNumber;
                }

                let cell5 = row.insertCell(4);
                if (gate.flight != null) {
                    if (gate.flight.departureAirport == "Manchester") {
                        cell5.textContent = gate.flight.departureTime;
                    }
                    else {
                        cell5.textContent = gate.flight.arrivalTime;
                    }
                }
            });
        })
}
function updateClock() {
    fetch('/api/clock')
        .then(response => response.json())
        .then(data => {
            const clockDisplay = document.getElementById('clockDisplay');
            clockDisplay.textContent = "Current Time: " + data.time;
        })
}

function updates() {
    updateClock()
    updateFlightBoard()
    updateArrivalsBoard()
    updateOperationsBoard()
}

// Initial update and set time until next update at 1 seconds
updateFlightBoard();
updateArrivalsBoard();
updateOperationsBoard();
updateClock();
setInterval(updates, 1000);
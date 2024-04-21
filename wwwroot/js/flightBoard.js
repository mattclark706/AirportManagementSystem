function updateFlightBoard() {
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

                let cell6 = row.insertCell(5);
                cell6.textContent = `${flight.departureAirport} (${flight.departureAirportCode})`;

                let cell7 = row.insertCell(6);
                cell7.textContent = flight.grounded ? "Yes" : "No";
            });
        })
        .catch(error => console.error('Error fetching updated flights:', error));
}

function updateArrivalsBoard() {
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
                cell3.textContent = flight.departureTime;

                let cell4 = row.insertCell(3);
                cell4.textContent = flight.gate === 0 ? "N/A" : flight.gate;

                let cell5 = row.insertCell(4);
                cell5.textContent = flight.status;

                let cell6 = row.insertCell(5);
                cell6.textContent = `${flight.arrivalAirport} (${flight.arrivalAirportCode})`;

                let cell7 = row.insertCell(6);
                cell7.textContent = flight.grounded ? "Yes" : "No";
            });
        })
        .catch(error => console.error('Error fetching updated flights:', error));
}
function updateClock() {
    fetch('/api/clock')
        .then(response => response.json())
        .then(data => {
            const clockDisplay = document.getElementById('clockDisplay');
            clockDisplay.textContent = "Current Time: " + data.time;
        })
}

// Initial update and set time until next update at 1 seconds
updateFlightBoard();
updateArrivalsBoard();
updateClock();
setInterval(updateFlightBoard, 1000);
setInterval(updateArrivalsBoard, 1000);
setInterval(updateClock, 1000);
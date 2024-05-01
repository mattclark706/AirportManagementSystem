using AirportManagementSystem.Models;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace AirportManagementSystem.Utilities
{
    public static class FlightService
    {
        private static List<Flight> allFlights = new List<Flight>();
        private static List<Flight> departures = new List<Flight>();
        private static List<Flight> arrivals = new List<Flight>();

        public static void LoadFlights(string filePath)
        {
            var json = File.ReadAllText(filePath);
            allFlights = JsonSerializer.Deserialize<List<Flight>> (json) ?? new List<Flight>();
            foreach (Flight flight in allFlights)
            {
                if (flight.DepartureAirport == "Manchester")
                {
                    flight.Grounded = true;
                }
                else
                {
                    flight.Grounded = false;
                }
            }
            departures = LoadDepartures();
            arrivals = LoadArrivals();
        }
        private static List<Flight> LoadDepartures()
        {
            return allFlights
                .Where(flight => flight.DepartureAirport.Equals("Manchester", StringComparison.OrdinalIgnoreCase)).ToList();
        }
        private static List<Flight> LoadArrivals()
        {
            return allFlights
                .Where(flight => flight.ArrivalAirport.Equals("Manchester", StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public static List<Flight> GetAllFlights() 
        {
            return allFlights;
        }

        public static List<Flight> GetDeparturesFlights()
        {
            // Identify flights that have already left to remove them
            var flightsToRemove = new List<Flight>();
            foreach (var flight in departures)
            {
                var flightTime = DateTime.ParseExact(flight.DepartureTime, "HH:mm", CultureInfo.InvariantCulture);
                var flightDate = Clock.CurrentTime.Date + flightTime.TimeOfDay;
                if (flightDate < Clock.CurrentTime)
                {
                    flight.Grounded = false;
                    flightsToRemove.Add(flight);
                }
            }

            // Remove the identified flights from the main list
            foreach (var flight in flightsToRemove)
            {
                departures.Remove(flight);
            }

            // Filter and return flights
            return departures
                .Where(flight =>
                {
                    var flightTime = DateTime.ParseExact(flight.DepartureTime, "HH:mm", CultureInfo.InvariantCulture);
                    var flightDate = Clock.CurrentTime.Date + flightTime.TimeOfDay;

                    return flightDate >= Clock.CurrentTime &&
                           flightDate <= Clock.CurrentTime.AddHours(5); // Time ahead that flights will be shown
                })
                .OrderBy(flight => flight.DepartureTime)
                .Take(10) // Maximum number of flights to be shown
                .ToList();
        }

        public static List<Flight> GetArrivalsFlights()
        {
            // Identify flights that have already arrived to remove them
            var flightsToRemove = new List<Flight>();
            foreach (var flight in arrivals)
            {
                var flightTime = DateTime.ParseExact(flight.ArrivalTime, "HH:mm", CultureInfo.InvariantCulture);
                var flightDate = Clock.CurrentTime.Date + flightTime.TimeOfDay;
                if (flightDate < Clock.CurrentTime)
                {
                    flight.Grounded = true;
                    flightsToRemove.Add(flight);
                }
            }

            // Remove the identified flights from the main list
            foreach (var flight in flightsToRemove)
            {
                arrivals.Remove(flight);
            }

            // Filter and return flights
            return arrivals
                .Where(flight =>
                {
                    var flightTime = DateTime.ParseExact(flight.ArrivalTime, "HH:mm", CultureInfo.InvariantCulture);
                    var flightDate = Clock.CurrentTime.Date + flightTime.TimeOfDay;

                    return flightDate >= Clock.CurrentTime &&
                           flightDate <= Clock.CurrentTime.AddHours(3); // Time ahead that flights will be shown
                })
                .OrderBy(flight => flight.ArrivalTime)
                .Take(10) // Maximum number of flights to be shown
                .ToList();
        }
    }
}

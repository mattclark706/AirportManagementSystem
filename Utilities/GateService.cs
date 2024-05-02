using AirportManagementSystem.Models;
using Microsoft.JSInterop;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AirportManagementSystem.Utilities
{

    public static class GateService
    {
        private static List<Gates> allGates = new List<Gates>();
        public static void fillAllGates()
        {
            // Number of gates at airport(-1)
            for (int i = 1; i < 16; i++)
            {
                allGates.Add(new Gates(i));
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void AssignGates(List<Flight> flights)
        {
            // set gate to empty if flight has left
            foreach (Gates gate in allGates)
            {
                if (gate.Flight != null)
                {
                    if (gate.Flight.DepartureAirport == "Manchester")
                    {
                        if (gate.Flight.Grounded == false)
                        {
                            gate.Flight = null;
                            gate.InUse = false;
                        }
                    }
                    else if (gate.Flight.ArrivalAirport == "Manchester")
                    {
                        if (gate.Flight.Grounded == true)
                        {
                            gate.Flight = null;
                            gate.InUse = false;
                        }
                    }
                }
            }
            // assign relavent flights to empty gates
            foreach (Flight flight in flights)
            {
                if (flight.Gate == null)
                {
                    foreach (Gates gate in allGates)
                    {
                        if (gate.InUse == false)
                        {
                            gate.Flight = flight;
                            gate.InUse = true;
                            flight.Gate = gate.GateNumber;
                            break;
                        }
                    }

                }
            }
            getStatus(flights);
        }
        // set flight status based on gate and time remaining to flight departure
        public static void getStatus(List<Flight> flights)
        {
            foreach (Flight flight in flights)
            {
                if (flight.DepartureAirport == "Manchester")
                {
                    var flightTime = DateTime.ParseExact(flight.DepartureTime, "HH:mm", CultureInfo.InvariantCulture);
                    var flightDate = Clock.CurrentTime.Date + flightTime.TimeOfDay;
                    if (flightDate <= Clock.currentTime.AddMinutes(30))
                    {
                        flight.Status = "Boarding";
                    }
                    if (flightDate <= Clock.currentTime.AddMinutes(10))
                    {
                        flight.Status = "Final Call";
                    }
                }  
            }
        }
        public static List<Gates> getGates() 
        {
            return allGates;
        }
    }
}

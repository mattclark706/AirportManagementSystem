using Microsoft.EntityFrameworkCore;

namespace AirportManagementSystem.Models
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureAirportCode { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string Duration { get; set; }
        public int? Gate { get; set; }
        public string Status { get; set; } = "On Time";
        public bool Grounded { get; set; }
    }
}

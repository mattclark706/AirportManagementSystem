using AirportManagementSystem.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace AirportManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _env;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public List<Models.Flight> Flights { get; set; }
        

        public void OnGet()
        {
            //Flights = FlightService.GetFilteredFlights(Path.Combine(_env.WebRootPath, "Data/flights.json"));
            Flights = FlightService.GetAllFlights();
        }
    }
}

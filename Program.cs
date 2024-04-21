using AirportManagementSystem;
using AirportManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var timeAdvance = Task.Run(async () =>
{
    while (true)
    {
        Clock.AdvanceTime(TimeSpan.FromMinutes(10));
        await Task.Delay(1000);
    }
});

FlightService.LoadFlights(Path.Combine(app.Environment.WebRootPath, "Data/flights.json"));
GateService.fillAllGates();

app.MapGet("/api/departures", () =>
{
    //var flights = FlightService.GetFilteredFlights(Path.Combine(app.Environment.WebRootPath, "Data/flights.json"));
    var flights = FlightService.GetDeparturesFlights();
    GateService.AssignGates(flights);
    return flights;
});

app.MapGet("/api/arrivals", () =>
{
    var flights = FlightService.GetArrivalsFlights();
    //GateService.AssignGates(flights);
    return flights;
});

app.MapGet("/api/clock", () => new { time = Clock.currentTime.ToString("HH:mm") });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

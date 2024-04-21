using Microsoft.AspNetCore.SignalR;

public class ClockHub : Hub
{
    public async Task SendFlightUpdates(string message)
    {
        await Clients.All.SendAsync("ReceiveFlightUpdate", message);
    }
}
using System.Diagnostics;

namespace GymAutomatBackgroundService.Api;

public class GymAutomat : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("GymAutomat");
        
        return Task.CompletedTask;
    }
}
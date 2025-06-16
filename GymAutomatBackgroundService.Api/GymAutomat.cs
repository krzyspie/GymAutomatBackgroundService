using GymAutomatBackgroundService.Api.Services;

namespace GymAutomatBackgroundService.Api;

public class GymAutomat : BackgroundService
{
    private readonly IDelayCalculator _delayCalculator;

    public GymAutomat(IDelayCalculator delayCalculator)
    {
        _delayCalculator = delayCalculator;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            TimeSpan delay = _delayCalculator.CalculateDelay();
            await Task.Delay(delay, stoppingToken);

            LoginToGym();
            RegisterToJogaClass();
        }
    }

    private void RegisterToJogaClass()
    {
        throw new NotImplementedException();
    }

    private void LoginToGym()
    {
        throw new NotImplementedException();
    }
}
using GymAutomatBackgroundService.Api.Services;

namespace GymAutomatBackgroundService.Api;

public class GymAutomat : BackgroundService
{
    private readonly IDelayCalculator _delayCalculator;
    private readonly IGymAccessService _gymAccessService;
    private readonly IGymWorkoutService _gymWorkoutService;

    public GymAutomat(IDelayCalculator delayCalculator, IGymAccessService gymAccessService, IGymWorkoutService gymWorkoutService)
    {
        _delayCalculator = delayCalculator;
        _gymAccessService = gymAccessService;
        _gymWorkoutService = gymWorkoutService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            TimeSpan delay = _delayCalculator.CalculateDelay();
            await Task.Delay(delay, stoppingToken);

            _gymAccessService.Login();
            _gymWorkoutService.RegisterToJogaClass();
        }
    }
}
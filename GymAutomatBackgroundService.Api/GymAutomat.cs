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
            await _gymAccessService.Login();
            
            var yogaWorkout = await _gymWorkoutService.GetJogaWorkouts();
            
            TimeSpan delay = _delayCalculator.CalculateDelay(yogaWorkout.StartDate);
            await Task.Delay(delay, stoppingToken);

            await _gymWorkoutService.RegisterToJogaClass(yogaWorkout.WorkoutId);

            await _gymAccessService.Logout();
        }
    }
}
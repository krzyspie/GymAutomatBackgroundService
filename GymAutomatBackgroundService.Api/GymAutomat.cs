using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Services;

namespace GymAutomatBackgroundService.Api;

public class GymAutomat : BackgroundService
{
    private readonly IDelayCalculator _delayCalculator;
    private readonly IGymAccessService _gymAccessService;
    private readonly IGymWorkoutService _gymWorkoutService;
    private readonly JogaWorkoutService _jogaWorkoutService;

    public GymAutomat(IDelayCalculator delayCalculator, IGymAccessService gymAccessService, IGymWorkoutService gymWorkoutService, JogaWorkoutService jogaWorkoutService)
    {
        _delayCalculator = delayCalculator;
        _gymAccessService = gymAccessService;
        _gymWorkoutService = gymWorkoutService;
        _jogaWorkoutService = jogaWorkoutService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _gymAccessService.Login();
            
            var jogaWorkouts = await _gymWorkoutService.GetJogaWorkouts();
            
            JogaWorkoutModel jogaWorkoutToRegister = _jogaWorkoutService.GetJogaWorkoutToRegister(jogaWorkouts);

            if (jogaWorkoutToRegister != null)
            {
                TimeSpan delay = _delayCalculator.CalculateDelay(jogaWorkoutToRegister.StartDate);
                await Task.Delay(delay, stoppingToken);

                await _gymWorkoutService.RegisterToJogaClass(jogaWorkoutToRegister.WorkoutId);
            }
            else
            {
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
            
            await _gymAccessService.Logout();
        }
    }
}
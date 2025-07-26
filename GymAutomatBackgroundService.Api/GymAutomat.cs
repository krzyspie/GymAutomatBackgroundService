using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Services;

namespace GymAutomatBackgroundService.Api;

public class GymAutomat : BackgroundService
{
    private readonly IDelayCalculator _delayCalculator;
    private readonly IGymAccessService _gymAccessService;
    private readonly IGymWorkoutService _gymWorkoutService;
    private readonly IJogaWorkoutService _jogaWorkoutService;

    public GymAutomat(IDelayCalculator delayCalculator, IGymAccessService gymAccessService, IGymWorkoutService gymWorkoutService, IJogaWorkoutService jogaWorkoutService)
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
            
            WorkoutToRegisterModel jogaWorkoutToRegister = _jogaWorkoutService.GetJogaWorkoutToRegister(jogaWorkouts);

            if (jogaWorkoutToRegister is { CanRegister: true, JogaWorkout: not null })
            {
                TimeSpan delay = _delayCalculator.CalculateDelay(jogaWorkoutToRegister.JogaWorkout.StartDate);
                await Task.Delay(delay, stoppingToken);

                await _gymWorkoutService.RegisterToJogaClass(jogaWorkoutToRegister.JogaWorkout.WorkoutId);
            }
            else
            {
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
            
            await _gymAccessService.Logout();
        }
    }
}
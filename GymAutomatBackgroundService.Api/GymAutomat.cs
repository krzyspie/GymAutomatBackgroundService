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
        // Wait a bit to allow the web server to start first
        Console.WriteLine("Starting GymAutomat");
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Console.WriteLine("Login to gym.");
                await _gymAccessService.Login();
                
                var jogaWorkouts = await _gymWorkoutService.GetJogaWorkouts();
                Console.WriteLine($"Joga workouts: {jogaWorkouts.Count}");
                
                WorkoutToRegisterModel jogaWorkoutToRegister = _jogaWorkoutService.GetJogaWorkoutToRegister(jogaWorkouts);
                Console.WriteLine($"Can register to workout: {jogaWorkoutToRegister.CanRegister}");

                if (jogaWorkoutToRegister is { CanRegister: true, JogaWorkout: not null })
                {
                    TimeSpan delay = _delayCalculator.CalculateDelay(jogaWorkoutToRegister.JogaWorkout.StartDate);
                    await Task.Delay(delay, stoppingToken);

                    await _gymWorkoutService.RegisterToJogaClass(jogaWorkoutToRegister.JogaWorkout.WorkoutId);
                }
                
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GymAutomat: {ex.Message}");
                // Wait before retrying
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
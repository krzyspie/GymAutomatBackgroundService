namespace GymAutomatBackgroundService.Api.Services;

public interface IGymWorkoutService
{
    Task RegisterToJogaClass();
    Task<List<int>> GetJogaWorkouts(DateTime startDate);
}
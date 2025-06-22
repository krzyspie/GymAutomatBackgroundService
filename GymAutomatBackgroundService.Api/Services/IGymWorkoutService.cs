namespace GymAutomatBackgroundService.Api.Services;

public interface IGymWorkoutService
{
    void RegisterToJogaClass();
    List<int> GetJogaWorkouts();
}
using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.Services;

public interface IGymWorkoutService
{
    Task RegisterToJogaClass();
    Task<List<JogaWorkoutModel>> GetJogaWorkouts(DateTime startDate);
}
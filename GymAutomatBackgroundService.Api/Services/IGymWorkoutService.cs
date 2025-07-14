using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.Services;

public interface IGymWorkoutService
{
    Task RegisterToJogaClass(int jogaId);
    Task<List<JogaWorkoutModel>> GetJogaWorkouts();
}
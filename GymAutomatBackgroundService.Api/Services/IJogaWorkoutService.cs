using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.Services;

public interface IJogaWorkoutService
{
    JogaWorkoutModel GetJogaWorkoutToRegister(List<JogaWorkoutModel> jogaWorkouts);
    bool IsRegistrationNotOpenYet(JogaWorkoutModel jogaWorkout);
}
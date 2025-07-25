namespace GymAutomatBackgroundService.Api.Models;

public class WorkoutToRegisterModel
{
    public bool CanRegister { get; set; }

    public JogaWorkoutModel? JogaWorkout { get; set; }
}
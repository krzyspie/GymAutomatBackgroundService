namespace GymAutomatBackgroundService.Api.Models;

public class GymWorkoutsResponse
{
    public ICollection<WorkoutResult> Result { get; set; } = new List<WorkoutResult>();
}
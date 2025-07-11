namespace GymAutomatBackgroundService.Api.Models;

public class GymWorkoutsResponse
{
    public ICollection<WorkoutResult> Results { get; set; } = new List<WorkoutResult>();
}
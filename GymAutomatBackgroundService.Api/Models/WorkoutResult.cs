namespace GymAutomatBackgroundService.Api.Models;

public class WorkoutResult
{
    public int ClassId { get; set; }
    public required string Name { get; set; }
    public required string StartDate { get; set; }
}
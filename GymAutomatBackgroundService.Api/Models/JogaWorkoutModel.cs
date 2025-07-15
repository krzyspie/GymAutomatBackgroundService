namespace GymAutomatBackgroundService.Api.Models;

public class JogaWorkoutModel
{
    public int WorkoutId { get; set; }
    public DateTime StartDate { get; set; }
    
    public int ParticipantsLimit { get; set; }
    
    public int ParticipantsNumber { get; set; }
}
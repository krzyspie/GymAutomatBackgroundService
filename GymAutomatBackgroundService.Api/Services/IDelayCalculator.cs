namespace GymAutomatBackgroundService.Api.Services;

public interface IDelayCalculator
{
    TimeSpan CalculateDelay(DateTime workoutDate);
}
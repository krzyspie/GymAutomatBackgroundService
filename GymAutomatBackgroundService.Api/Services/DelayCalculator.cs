using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class DelayCalculator : IDelayCalculator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public DelayCalculator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public TimeSpan CalculateDelay(DateTime workoutDate)
    {
        DateTime registrationDate = workoutDate.AddDays(-3);
        DateTime registrationDateTime = new DateTime(registrationDate.Year, registrationDate.Month, registrationDate.Day, 12, 20, 0, DateTimeKind.Local);
        DateTime now = _dateTimeProvider.Now;

        var delay = registrationDateTime > now
            ? registrationDateTime - now
            : TimeSpan.FromDays(1);
        
        return delay;
    }
}
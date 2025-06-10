using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class DelayCalculator : IDelayCalculator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public DelayCalculator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public int CalculateDelay()
    {
        DateTime now = _dateTimeProvider.Now;

        if ((now.DayOfWeek == DayOfWeek.Wednesday || now.DayOfWeek == DayOfWeek.Friday) && now.Hour == 12)
        {
            return 1000;
        }
        
        DateTime yogaRegistrationDay = now.DayOfWeek switch
        {
            DayOfWeek.Friday => now.AddDays(5),
            DayOfWeek.Saturday => now.AddDays(4),
            DayOfWeek.Sunday => now.AddDays(3),
            DayOfWeek.Monday => now.AddDays(2),
            DayOfWeek.Tuesday => now.AddDays(2),
            DayOfWeek.Wednesday => now.AddDays(2),
            DayOfWeek.Thursday => now.AddDays(1),
            _ => now
        };

        DateTime referenceDate = new DateTime(yogaRegistrationDay.Year, yogaRegistrationDay.Month, yogaRegistrationDay.Day, 12, 15, 0, DateTimeKind.Local);

        var delay = referenceDate - now;
        
        return delay.Milliseconds;
    }
}
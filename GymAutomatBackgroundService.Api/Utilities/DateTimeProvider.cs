namespace GymAutomatBackgroundService.Api.Utilities;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DayOfWeek GetDayOfWeek(DateTime dateTime)
    {
        return dateTime.DayOfWeek;
    }

    public int GetHour(DateTime dateTime)
    {
        return dateTime.Hour;
    }

    public DateTime AddDays(DateTime dateTime, int days)
    {
        return dateTime.AddDays(days);
    }
}
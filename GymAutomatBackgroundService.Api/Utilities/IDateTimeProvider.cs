namespace GymAutomatBackgroundService.Api.Utilities;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DayOfWeek GetDayOfWeek(DateTime dateTime);
    int GetHour(DateTime dateTime);
    DateTime AddDays(DateTime dateTime, int days);
}
namespace GymAutomatBackgroundService.Api.Services;

public interface IRequestDataFactory
{
    FormUrlEncodedContent GetClassesRequest(string dateFrom, string dateTo);
    FormUrlEncodedContent RegisterWorkoutRequest(string classId);
    FormUrlEncodedContent LoginToGymRequest(string login, string password);
}
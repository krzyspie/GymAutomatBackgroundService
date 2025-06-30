namespace GymAutomatBackgroundService.Api.Services;

public interface IRequestDataFactory
{
    FormUrlEncodedContent GetJogaWorkoutsRequest(string dateFrom, string dateTo);
    FormUrlEncodedContent RegisterWorkoutRequest(string classId);
    FormUrlEncodedContent LoginToGymRequest(string login, string password);
}
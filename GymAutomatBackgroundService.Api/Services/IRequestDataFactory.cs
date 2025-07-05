namespace GymAutomatBackgroundService.Api.Services;

public interface IRequestDataFactory
{
    FormUrlEncodedContent GetJogaWorkoutsRequest(DateTime dateFrom, DateTime dateTo);
    FormUrlEncodedContent RegisterWorkoutRequest(int classId);
    FormUrlEncodedContent LoginToGymRequest(string login, string password);
}
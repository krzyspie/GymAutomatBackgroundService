namespace GymAutomatBackgroundService.Api.HttpClients;

public interface IGymClient
{
    Task LoginToGym(FormUrlEncodedContent data);
    Task RegisterToJogaWorkout(FormUrlEncodedContent data);
}
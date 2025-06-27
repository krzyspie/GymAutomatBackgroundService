namespace GymAutomatBackgroundService.Api.HttpClients;

public interface IGymClient
{
    Task LoginToGym();
    Task RegisterToJogaWorkout();
}
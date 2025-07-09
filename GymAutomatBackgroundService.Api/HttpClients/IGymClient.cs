using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.HttpClients;

public interface IGymClient
{
    Task<GymResponse?> LoginToGym(FormUrlEncodedContent data);
    Task Logout();
    Task<GymResponse?> RegisterToJogaWorkout(FormUrlEncodedContent data);
    Task<GymWorkoutsResponse> GetWorkouts(FormUrlEncodedContent data);
}
using GymAutomatBackgroundService.Api.HttpClients;

namespace GymAutomatBackgroundService.Api.Services;

public class GymWorkoutService : IGymWorkoutService
{
    private readonly IGymClient _gymClient;
    private const string JogaWorkoutId = "33146527";

    public GymWorkoutService(IGymClient gymClient)
    {
        _gymClient = gymClient;
    }
    public void RegisterToJogaClass()
    {
        throw new NotImplementedException();
    }

    public List<int> GetJogaWorkouts()
    {
        throw new NotImplementedException();
    }
}
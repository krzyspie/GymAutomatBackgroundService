namespace GymAutomatBackgroundService.Api.HttpClients;

public class GymClient : IGymClient
{
    private readonly HttpClient client;

    public GymClient(HttpClient client)
    {
        this.client = client;
    }

    public Task LoginToGym(FormUrlEncodedContent data)
    {
        throw new NotImplementedException();
    }

    public Task RegisterToJogaWorkout(FormUrlEncodedContent data)
    {
        throw new NotImplementedException();
    }
}
using GymAutomatBackgroundService.Api.HttpClients;

namespace GymAutomatBackgroundService.Api.Services;

public class GymAccessService : IGymAccessService
{
    private readonly IGymClient _gymClient;
    private readonly IRequestDataFactory _requestDataFactory;

    public GymAccessService(IGymClient gymClient, IRequestDataFactory requestDataFactory)
    {
        _gymClient = gymClient;
        _requestDataFactory = requestDataFactory;
    }
    
    public async Task Login()
    {
        FormUrlEncodedContent requestData = _requestDataFactory.LoginToGymRequest("aaa", "bbb");
        await _gymClient.LoginToGym(requestData);
    }

    public void Logout()
    {
        
    }
}
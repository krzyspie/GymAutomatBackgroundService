using GymAutomatBackgroundService.Api.HttpClients;

namespace GymAutomatBackgroundService.Api.Services;

public class GymAccessService : IGymAccessService
{
    private readonly IGymClient _gymClient;
    private readonly IRequestDataFactory _requestDataFactory;
    private readonly ISecretsManagerService _secretsManagerService;

    public GymAccessService(IGymClient gymClient, IRequestDataFactory requestDataFactory, ISecretsManagerService secretsManagerService)
    {
        _gymClient = gymClient;
        _requestDataFactory = requestDataFactory;
        _secretsManagerService = secretsManagerService;
    }
    
    public async Task Login()
    {
        string login = _secretsManagerService.GetLogin();
        string password = _secretsManagerService.GetPassword();
        
        FormUrlEncodedContent requestData = _requestDataFactory.LoginToGymRequest(login, password);
        await _gymClient.LoginToGym(requestData);
    }

    public async Task Logout()
    {
        await _gymClient.Logout();
    }
}
using Google.Cloud.SecretManager.V1;

namespace GymAutomatBackgroundService.Api.Services;

public class SecretsManagerService : ISecretsManagerService
{
    private const string ProjectId = "gymautomatproject";
    
    public string GetLogin()
    {
        var client = SecretManagerServiceClient.Create();
        var name = new SecretVersionName(ProjectId, "GymLogin", "latest");
        var result = client.AccessSecretVersion(name);
        return result.Payload.Data.ToStringUtf8();
    }

    public string GetPassword()
    {
        var client = SecretManagerServiceClient.Create();
        var name = new SecretVersionName(ProjectId, "GymPass", "latest");
        var result = client.AccessSecretVersion(name);
        return result.Payload.Data.ToStringUtf8();
    }
}
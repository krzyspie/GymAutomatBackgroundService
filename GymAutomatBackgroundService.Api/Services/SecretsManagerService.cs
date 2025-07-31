using Google.Cloud.SecretManager.V1;
using Google.Apis.Auth.OAuth2;

namespace GymAutomatBackgroundService.Api.Services;

public class SecretsManagerService : ISecretsManagerService
{
    private const string ProjectId = "gymautomatproject";
    
    public string GetLogin()
    {
        try
        {
            var credential = GetCredential();
            var client = new SecretManagerServiceClientBuilder
            {
                Credential = credential
            }.Build();
            
            var name = new SecretVersionName(ProjectId, "GymLogin", "latest");
            var result = client.AccessSecretVersion(name);
            return result.Payload.Data.ToStringUtf8();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to retrieve login secret: {ex.Message}", ex);
        }
    }

    public string GetPassword()
    {
        try
        {
            var credential = GetCredential();
            var client = new SecretManagerServiceClientBuilder
            {
                Credential = credential
            }.Build();
            
            var name = new SecretVersionName(ProjectId, "GymPass", "latest");
            var result = client.AccessSecretVersion(name);
            return result.Payload.Data.ToStringUtf8();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to retrieve password secret: {ex.Message}", ex);
        }
    }
    
    private GoogleCredential GetCredential()
    {
        try
        {
            // First try to get application default credentials
            return GoogleCredential.GetApplicationDefault();
        }
        catch
        {
            try
            {
                // If that fails, try to use ComputeCredential directly for Cloud Run
                return GoogleCredential.FromComputeCredential();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get credentials: {ex.Message}");
                throw new InvalidOperationException("Unable to obtain Google Cloud credentials", ex);
            }
        }
    }
}

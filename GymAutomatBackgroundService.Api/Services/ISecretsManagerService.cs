namespace GymAutomatBackgroundService.Api.Services;

public interface ISecretsManagerService
{
    string GetLogin();
    string GetPassword();
}
namespace GymAutomatBackgroundService.Api.Services;

public interface IGymAccessService
{
    Task Login();
    void Logout();
}
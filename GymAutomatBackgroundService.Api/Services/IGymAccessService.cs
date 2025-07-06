namespace GymAutomatBackgroundService.Api.Services;

public interface IGymAccessService
{
    Task Login();
    Task Logout();
}
namespace GymAutomatBackgroundService.Api.Models;

public class Error
{
    public string ResponseCode { get; set; }
    public string Message { get; set; }
    public string? Path { get; set; }
}
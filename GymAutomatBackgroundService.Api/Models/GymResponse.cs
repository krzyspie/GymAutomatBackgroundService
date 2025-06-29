using System.Text.Json.Serialization;

namespace GymAutomatBackgroundService.Api.Models;

public class GymResponse
{
    public string Status { get; set; }
    public string Message { get; set; }
    [JsonPropertyName("api_check")]
    public ApiCheck ApiCheck { get; set; }
}
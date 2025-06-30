using System.Text.Json;
using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.HttpClients;

public class GymClient : IGymClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    
    private readonly HttpClient _client;

    public GymClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<GymResponse?> LoginToGym(FormUrlEncodedContent data)
    {
        HttpResponseMessage response = await _client.PostAsync("/wp-admin/admin-ajax.php", data);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GymResponse>(responseString, _jsonSerializerOptions);

        return result;
    }

    public async Task<GymResponse?> RegisterToJogaWorkout(FormUrlEncodedContent data)
    {
        HttpResponseMessage response = await _client.PostAsync("/wp-admin/admin-ajax.php", data);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GymResponse>(responseString, _jsonSerializerOptions);

        return result;
    }
}
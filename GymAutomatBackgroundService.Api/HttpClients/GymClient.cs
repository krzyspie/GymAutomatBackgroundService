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

    public async Task Logout()
    {
        HttpResponseMessage response = await _client.PostAsync("/admin-panel/?action=logout&redirect_to=https%3A%2F%2Fjustgym.pl%2Fklient%3Flogout%3Dsuccess&_wpnonce=c1e868a7b7", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<GymResponse?> RegisterToJogaWorkout(FormUrlEncodedContent data)
    {
        HttpResponseMessage response = await _client.PostAsync("/wp-admin/admin-ajax.php", data);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GymResponse>(responseString, _jsonSerializerOptions);

        return result;
    }

    public async Task<GymWorkoutsResponse> GetWorkouts(FormUrlEncodedContent data)
    {
        HttpResponseMessage response = await _client.PostAsync("/wp-admin/admin-ajax.php", data);
        var responseString = await response.Content.ReadAsStringAsync(); 

        var innerJson = JsonSerializer.Deserialize<string>(responseString);
        var result = JsonSerializer.Deserialize<GymWorkoutsResponse>(innerJson, _jsonSerializerOptions);

        return result ?? new GymWorkoutsResponse();
    }
}
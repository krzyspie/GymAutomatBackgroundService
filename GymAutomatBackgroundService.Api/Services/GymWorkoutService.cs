using System.Globalization;
using GymAutomatBackgroundService.Api.HttpClients;
using GymAutomatBackgroundService.Api.Models;

namespace GymAutomatBackgroundService.Api.Services;

public class GymWorkoutService : IGymWorkoutService
{
    private readonly IGymClient _gymClient;
    private readonly IRequestDataFactory _requestDataFactory;
    public GymWorkoutService(IGymClient gymClient, IRequestDataFactory requestDataFactory)
    {
        _gymClient = gymClient;
        _requestDataFactory = requestDataFactory;
    }
    public async Task RegisterToJogaClass()
    {
        await _gymClient.RegisterToJogaWorkout(null);
        
        return;
    }

    public async Task<List<int>> GetJogaWorkouts(DateTime startDate)
    {
        string endDate = startDate.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss");
        string date = startDate.ToString("yyyy-MM-dd HH:mm:ss");
        
        FormUrlEncodedContent workoutsRequest = _requestDataFactory.GetJogaWorkoutsRequest(date, endDate);
        GymWorkoutsResponse gymWorkoutsResponse = await _gymClient.GetWorkouts(workoutsRequest);

        return new List<int>();
    }
}
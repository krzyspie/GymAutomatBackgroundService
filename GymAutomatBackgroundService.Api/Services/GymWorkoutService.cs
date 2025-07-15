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
    public async Task RegisterToJogaClass(int jogaId)
    {
        FormUrlEncodedContent request = _requestDataFactory.RegisterWorkoutRequest(jogaId);
        GymResponse? response = await _gymClient.RegisterToJogaWorkout(request);
    }

    public async Task<List<JogaWorkoutModel>> GetJogaWorkouts()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(5);
        
        FormUrlEncodedContent workoutsRequest = _requestDataFactory.GetJogaWorkoutsRequest(startDate, endDate);
        GymWorkoutsResponse gymWorkoutsResponse = await _gymClient.GetWorkouts(workoutsRequest);
        List<JogaWorkoutModel> jogaWorkouts = gymWorkoutsResponse.Results
            .Where(w => w.Name.Contains("joga", StringComparison.InvariantCultureIgnoreCase))
            .Select(wi => 
                new JogaWorkoutModel
                {
                    WorkoutId = wi.ClassId, 
                    StartDate = DateTime.Parse(wi.StartDate, CultureInfo.InvariantCulture),
                    ParticipantsLimit = wi.ParticipantsLimit,
                    ParticipantsNumber = wi.ParticipantsNumber
                })
            .OrderBy(jw => jw.StartDate)
            .ToList();

        return jogaWorkouts;
    }
}
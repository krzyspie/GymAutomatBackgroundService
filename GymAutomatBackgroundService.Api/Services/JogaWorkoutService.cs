using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class JogaWorkoutService
{
    private readonly DateTimeProvider _dateTimeProvider;

    public JogaWorkoutService(DateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public JogaWorkoutModel GetJogaWorkoutToRegister(List<JogaWorkoutModel> jogaWorkouts)
    {
        if (jogaWorkouts == null || !jogaWorkouts.Any())
            return null;

        var workouts = jogaWorkouts
            .Where(w => w.StartDate.DayOfWeek != DayOfWeek.Sunday)
            .OrderBy(j => j.StartDate)
            .ToList();

        foreach (var jogaWorkout in workouts)
        {
            if (_dateTimeProvider.Now <= jogaWorkout.StartDate.AddDays(-4))
            {
                return jogaWorkout;
            }
            if (jogaWorkout.ParticipantsNumber < jogaWorkout.ParticipantsLimit)
            {
                return jogaWorkout;
            }
        }

        return null;
    }
}
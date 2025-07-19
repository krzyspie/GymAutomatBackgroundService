using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class JogaWorkoutService
{
    private const int DaysBeforeStartForRegistration = 4;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JogaWorkoutService(IDateTimeProvider dateTimeProvider)
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
            if (IsRegistrationNotOpenYet(jogaWorkout))
            {
                return jogaWorkout;
            }
            if (HasFreeSlots(jogaWorkout))
            {
                return jogaWorkout;
            }
        }

        return null;
    }

    private static bool HasFreeSlots(JogaWorkoutModel jogaWorkout) => 
        jogaWorkout.ParticipantsNumber < jogaWorkout.ParticipantsLimit;

    private bool IsRegistrationNotOpenYet(JogaWorkoutModel jogaWorkout) => 
        _dateTimeProvider.Now <= jogaWorkout.StartDate.AddDays(-DaysBeforeStartForRegistration);
}
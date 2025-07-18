using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class JogaWorkoutService
{
    private const int MinDaysRegistrationIsOpen = 4;
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
            if (IsBeforeRegistrationOpen(jogaWorkout))
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

    private static bool HasFreeSlots(JogaWorkoutModel jogaWorkout)
    {
        return jogaWorkout.ParticipantsNumber < jogaWorkout.ParticipantsLimit;
    }

    private bool IsBeforeRegistrationOpen(JogaWorkoutModel jogaWorkout)
    {
        return _dateTimeProvider.Now <= jogaWorkout.StartDate.AddDays(-MinDaysRegistrationIsOpen);
    }
}
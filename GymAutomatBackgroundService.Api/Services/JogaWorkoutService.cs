using GymAutomatBackgroundService.Api.Models;
using GymAutomatBackgroundService.Api.Utilities;

namespace GymAutomatBackgroundService.Api.Services;

public class JogaWorkoutService : IJogaWorkoutService
{
    private const int DaysBeforeStartForRegistration = 4;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JogaWorkoutService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public WorkoutToRegisterModel GetJogaWorkoutToRegister(List<JogaWorkoutModel> jogaWorkouts)
    {
        if (jogaWorkouts == null || !jogaWorkouts.Any())
            return new WorkoutToRegisterModel { CanRegister = false };

        var workouts = jogaWorkouts
            .Where(w => w.StartDate.DayOfWeek != DayOfWeek.Sunday)
            .OrderBy(j => j.StartDate)
            .ToList();

        foreach (var jogaWorkout in workouts)
        {
            if (IsRegistrationNotOpenYet(jogaWorkout))
            {
                return new WorkoutToRegisterModel() { CanRegister = true, JogaWorkout = jogaWorkout };
            }
            if (HasFreeSlots(jogaWorkout))
            {
                return new WorkoutToRegisterModel() { CanRegister = true, JogaWorkout = jogaWorkout };
            }
        }

        return new WorkoutToRegisterModel { CanRegister = false };
    }

    private static bool HasFreeSlots(JogaWorkoutModel jogaWorkout) => 
        jogaWorkout.ParticipantsNumber < jogaWorkout.ParticipantsLimit;

    public bool IsRegistrationNotOpenYet(JogaWorkoutModel jogaWorkout) => 
        _dateTimeProvider.Now <= jogaWorkout.StartDate.AddDays(-DaysBeforeStartForRegistration);
}
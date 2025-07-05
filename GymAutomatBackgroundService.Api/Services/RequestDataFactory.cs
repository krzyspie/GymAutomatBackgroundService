namespace GymAutomatBackgroundService.Api.Services;

public class RequestDataFactory : IRequestDataFactory
{
    private const string ClubId = "1511";
    private const string PostAction = "mda_post_class";
    private const string LoginAction = "mda_user_login";
    
    public FormUrlEncodedContent GetJogaWorkoutsRequest(DateTime dateFrom, DateTime dateTo)
    {
        var registerData = new Dictionary<string, string>
        {
            { "action", "ef_get_classes" },
            { "club_id", ClubId },
            { "date_from", dateFrom.ToString("yyyy-MM-dd HH:mm:ss") },
            { "date_to", dateTo.ToString("yyyy-MM-dd HH:mm:ss") }
        };
        
        return new FormUrlEncodedContent(registerData);
    }
    
    public FormUrlEncodedContent RegisterWorkoutRequest(int classId)
    {
        var registerData = new Dictionary<string, string>
        {
            { "action", PostAction },
            { "class_id", classId.ToString() },
            { "club_id", ClubId }
        };
        
        return new FormUrlEncodedContent(registerData);
    }
    
    public FormUrlEncodedContent LoginToGymRequest(string login, string password)
    {
        var logData = new Dictionary<string, string>
        {
            { "action", LoginAction },
            { "log", login },
            { "pwd", password },
            { "return_url", "https://justgym.pl/klient/" }
        };

        return new FormUrlEncodedContent(logData);
    }
}
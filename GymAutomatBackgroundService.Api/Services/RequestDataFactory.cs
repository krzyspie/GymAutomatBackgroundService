namespace GymAutomatBackgroundService.Api.Services;

public class RequestDataFactory : IRequestDataFactory
{
    private const string ClubId = "1511";
    private const string PostAction = "mda_post_class";
    private const string LoginAction = "mda_user_login";
    
    public FormUrlEncodedContent GetClassesRequest(string dateFrom, string dateTo)
    {
        var registerData = new Dictionary<string, string>
        {
            { "action", PostAction },
            { "club_id", ClubId },
            { "date_from", dateFrom },
            { "date_to", dateTo }
        };
        
        return new FormUrlEncodedContent(registerData);
    }
    
    public FormUrlEncodedContent RegisterWorkoutRequest(string classId)
    {
        var registerData = new Dictionary<string, string>
        {
            { "action", PostAction },
            { "class_id", classId },
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
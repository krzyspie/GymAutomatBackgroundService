namespace GymAutomatBackgroundService.Api.Services;

public class RequestFactory
{
    public FormUrlEncodedContent GetGetClassesRequest()
    {
        Dictionary<string, string> registerData = new Dictionary<string, string>();
        registerData.Add("action", "mda_post_class");
        registerData.Add("club_id", "1511");
        registerData.Add("date_from", "1511");
        registerData.Add("date_to", "1511");
        return new FormUrlEncodedContent(registerData);
    }
}
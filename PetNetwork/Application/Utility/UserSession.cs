using PetNetwork.Domain.Models;

namespace PetNetwork.Application.Utility;

public class UserSession
{
    public static UserSession? Session { get; private set; }

    public UserAccount Account { get; private set; }

    public Person PersonalInfo { get; private set; }

    private UserSession(UserAccount account, Person personalInfo)
    {
        Account = account;
        PersonalInfo = personalInfo;
    }

    public static void Start(UserAccount account, Person personalInfo)
    {
        if (Session != null)
            throw new InvalidOperationException("Session already initialized.");
        Session = new UserSession(account, personalInfo);
    }

    public static void Stop() => Session = null;
}
using ClassLibrary;

namespace WebFront.Services;

public class LoginService : ILoginService
{
    private Users? LoggedinnUser;

    public LoginService()
    {
        LoggedinnUser = null;
    }

    public void Login(Users user)
    {
        LoggedinnUser = user;
        //TODO store with valkey
    }

    public void Logout()
    {
        LoggedinnUser = null;
        //TODO Remove from valkey
    }

    public bool IsLoggedIn()
    {
        //TODO check if there is a user logged inn via Valkey
        return LoggedinnUser != null;
    }
    
    public Users GetLoggedinnUser()
    {
        return LoggedinnUser;
    }
}
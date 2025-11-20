using ClassLibrary;

namespace WebFront.Services;

public interface ILoginService
{
    void Login(Users user);
    void Logout();
    bool IsLoggedIn();
    Users GetLoggedinnUser();
}
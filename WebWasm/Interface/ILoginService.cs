using WebWasm.Models;

namespace WebWasm.Services;

public interface ILoginService
{
    Task Login(Users user, string sessionToken);
    Task Logout();
    Task<bool> IsLoggedIn(string? sessionToken);
    Task<Users?> GetLoggedinnUser(string sessionToken);
}

using ClassLibrary;

namespace WebFront.Services;

public interface IUserService
{
    Task<IEnumerable<Users>?> GetUsers();
    Task<Users?> GetUser(int id);
    Task CreateUser(Users user);
    Task DeleteUser(int id);
}
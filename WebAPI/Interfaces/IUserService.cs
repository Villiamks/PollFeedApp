using ClassLibrary;

namespace WebAPI.Services;

public interface IUserService
{
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users?> GetUserById(int id);
    Task CreateUser(Users user);
    Task DeleteUser(int id);
}
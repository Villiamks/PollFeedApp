using ClassLibrary;

namespace WebAPI.Interfaces;

public interface IUserService
{
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users?> GetUserById(int id);
    Task<Users> CreateUser(Users user);
    Task<Users?> DeleteUserById(int id);
}
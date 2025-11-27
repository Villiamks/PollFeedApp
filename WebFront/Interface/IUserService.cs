using ClassLibrary;
using ClassLibrary.DTOs;

namespace WebFront.Services;

public interface IUserService
{
    Task<bool> RegisterUser(RegisterDTO registerDTO);
    Task<string?> LoginUser(LoginDTO loginDTO);
    Task<IEnumerable<Users>?> GetUsers();
    Task<Users?> GetUser(int id);
    Task CreateUser(string userName, string password, string email);
    Task DeleteUser(int id);
}
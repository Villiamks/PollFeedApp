using WebWasm.Models;

namespace WebWasm.Services;

public interface IUserService
{
    Task<IEnumerable<Users>?> GetUsers();
    Task<Users?> GetUser(int id);
    Task CreateUser(string userName, string password,  string email);
    Task DeleteUser(int id);
}

using ClassLibrary;

namespace WebAPI.Interfaces;

public interface IUserService
{
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users> GetUserById(int id);
    
    //TODO Create and Delete
}
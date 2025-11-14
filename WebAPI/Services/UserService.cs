using ClassLibrary;
using WebAPI.Data.Repos;

namespace WebAPI.Services;

public class UserService : IUserService
{
    private UserContext _dbContext;

    public UserService(UserContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        return _dbContext.GetUsers();
    }

    public async Task<Users?> GetUserById(int id)
    {
        return await _dbContext.GetUserById(id);
    }

    public async Task CreateUser(Users user)
    {
        _dbContext.AddUser(user);
    }

    public async Task DeleteUser(int id)
    {
        var user  = await GetUserById(id);
        if (user != null)
            _dbContext.DeleteUser(user);
    }
}
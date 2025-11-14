using ClassLibrary;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class UserService : IUserService
{
    private IRepository<Users>  _userRepository;

    public UserService(IRepository<Users> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return users;
    }

    public async Task<Users> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }
}
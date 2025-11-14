using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using DbContext = WebAPI.Data.DbContext;

namespace WebAPI.Services;

public class UserService : IUserService
{
    
    private DbContext _context;
    
    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        var users  = _context.Users
            .Include(u => u.Polls)
            .Include(u => u.Votes);
        return users;
    }

    public async Task<Users?> GetUserById(int id)
    {
        return GetAllUsers().Result.FirstOrDefault(u => u.UserId == id);
    }
    
    public async Task CreateUser(Users newUser)
    {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await GetUserById(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

}